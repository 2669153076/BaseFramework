using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace BaseFramework.Runtime
{
    /// <summary>
    /// Resources模式本地化资源加载器
    /// 实现ILocalizationProvider接口，使用Resources同步加载文本表与精灵资源
    /// 注意：Resources.Load是同步阻塞加载，包装为Task仅适配接口，并非真正异步
    /// </summary>
    public sealed class ResourcesLocalizationProvider : ILocalizationProvider
    {
        /// <summary>
        /// 语言表在Resources下的根目录
        /// </summary>
        private readonly string _tableRoot;

        /// <summary>
        /// 构造Resources本地化加载器
        /// </summary>
        /// <param name="tableRoot">语言表Resources根路径，默认 Localization</param>
        public ResourcesLocalizationProvider(string tableRoot = "Localization")
        {
            _tableRoot = tableRoot;
        }

        public Task<LocalizationTable> LoadTableAsync(E_Language language, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            string path = GetLanguageTablePath(language);
            //TODO:资源加载
            TextAsset asset = Resources.Load<TextAsset>(path);

            if (asset == null)
            {
                Log.Error("找不到本地化语言表: {0}", path);
                return Task.FromResult<LocalizationTable>(null);
            }

            LocalizationTable table = JsonFileMgr.GetInstance().FromJson<LocalizationTable>(asset.text, JsonFileMgr.EJsonType.LitJson);

            return Task.FromResult(table);
        }

        
        public Task<Sprite> LoadSpriteAsync(string assetPath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(assetPath))
            {
                return Task.FromResult<Sprite>(null);
            }
            //TODO:资源加载
            Sprite sprite = Resources.Load<Sprite>(assetPath);
            return Task.FromResult(sprite);
        }
        /// <summary>
        /// 释放精灵资源<br/>
        /// Resources模式资源由引擎自动管理，此处无需手动释放
        /// </summary>
        /// <param name="assetPath"></param>
        public void ReleaseSprite(string assetPath)
        {
            // Resources 不需要在这里释放。
        }

        /// <summary>
        /// 清理Provider，Resources模式无额外资源需要释放
        /// </summary>
        public void Clear()
        {
        }

        /// <summary>
        /// 根据语言枚举拼接Resources内语言表完整路径
        /// </summary>
        /// <param name="language">目标语言</param>
        /// <returns>Resources加载路径，未知语言默认返回简体中文路径</returns>
        private string GetLanguageTablePath(E_Language language)
        {
            return language switch
            {
                E_Language.SChinese => $"{_tableRoot}/SChinese",
                E_Language.TChinese => $"{_tableRoot}/TChinese",
                E_Language.English => $"{_tableRoot}/English",
                E_Language.Japanese => $"{_tableRoot}/Japanese",
                _ => $"{_tableRoot}/SChinese"
            };
        }
    }
}
