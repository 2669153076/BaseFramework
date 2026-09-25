using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public interface ILocalizationProvider
    {
        /// <summary>
        /// 加载指定语言的本地化Json表
        /// </summary>
        /// <param name="language">目标语言枚举</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>解析后的本地化表对象；找不到文件/解析失败返回null</returns>
        Task<LocalizationTable> LoadTableAsync(E_Language language,CancellationToken cancellationToken);

        /// <summary>
        /// 通过Resources加载本地化精灵
        /// </summary>
        /// <param name="assetPath">Resources下Sprite资源路径</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Sprite资源；路径为空或资源不存在返回null</returns>
        Task<Sprite> LoadSpriteAsync(string assetPath,CancellationToken cancellationToken);

        /// <summary>
        /// 释放精灵资源
        /// </summary>
        /// <param name="assetPath">精灵资源路径</param>
        void ReleaseSprite(
            string assetPath);

        /// <summary>
        /// 清理 Provider
        /// </summary>
        void Clear();
    }
}