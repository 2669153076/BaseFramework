using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace BaseFramework.Runtime
{
    /// <summary>
    /// 本地化多语言管理器
    /// 负责语言表加载、语言切换、文本/精灵读取，内置缓存与默认语言降级Fallback
    /// </summary>
    public sealed class LocalizationMgr : SingletonNormal<LocalizationMgr>
    {
        /// <summary>
        /// 本地化资源加载提供者，对接不同资源加载方案（Resources/AB等）
        /// </summary>
        private ILocalizationProvider _provider;
        /// <summary>
        /// 本地化缓存实例，缓存已加载语言表与Sprite
        /// </summary>
        private LocalizationCache _cache;
        /// <summary>
        /// 语言切换异步任务取消令牌源，用于中断上一次未完成的语言加载
        /// </summary>
        private CancellationTokenSource _languageCts;
        /// <summary>
        /// 当前使用语言
        /// </summary>
        private E_Language _currentLanguage;
        /// <summary>
        /// 兜底默认语言，当前语言找不到Key时自动回退到此语言
        /// </summary>
        private E_Language _defaultLanguage;
        /// <summary>
        /// 管理器是否完成初始化
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// 当前语言
        /// </summary>
        public E_Language CurrentLanguage => _currentLanguage;
        /// <summary>
        /// 默认兜底语言
        /// </summary>
        public E_Language DefaultLanguage => _defaultLanguage;
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        public bool IsInitialized => _initialized;
        /// <summary>
        /// 当前本地化资源加载提供者
        /// </summary>
        public ILocalizationProvider Provider => _provider;
        /// <summary>
        /// 语言切换完成回调，参数：新语言枚举
        /// </summary>
        public event Action<E_Language> OnLanguageChanged;

        /// <summary>
        /// 私有构造，单例模式
        /// </summary>
        private LocalizationMgr()
        {
        }

        #region Initialize
        /// <summary>
        /// 初始化本地化管理器，加载默认语言表
        /// </summary>
        /// <param name="defaultLanguage">默认兜底语言</param>
        /// <param name="provider">资源加载实现提供者</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>true初始化成功；false失败/取消</returns>
        public async Task<bool> InitializeAsync(E_Language defaultLanguage, ILocalizationProvider provider, CancellationToken cancellationToken = default)
        {
            if (_initialized)
            {
                Log.Warning("LocalizationMgr 已经初始化");
                return true;
            }
            if (provider == null)
            {
                Log.Error("LocalizationProvider 为空");
                return false;
            }
            _provider = provider;
            _cache = new LocalizationCache();
            _defaultLanguage = defaultLanguage;
            _currentLanguage = defaultLanguage;

            try
            {
                LocalizationTable table = await _provider.LoadTableAsync(defaultLanguage, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (table == null)
                {
                    Log.Error("默认语言加载失败: {0}", defaultLanguage);
                    return false;
                }
                AddTable(defaultLanguage, table);
                _initialized = true;
                return true;
            }
            catch (OperationCanceledException)
            {
                Log.Warning("Localization 初始化被取消");
                return false;
            }
        }
        #endregion

        #region Language
        /// <summary>
        /// 切换语言。如果语言表未缓存，则自动加载；加载成功后触发OnLanguageChanged事件
        /// </summary>
        /// <param name="language">目标语言</param>
        /// <returns>true切换成功；false加载失败/任务被取消</returns>
        public async Task<bool> SetLanguageAsync(E_Language language)
        {
            if (!_initialized)
            {
                Log.Error("LocalizationMgr 尚未初始化");
                return false;
            }
            if (_currentLanguage == language)
            {
                return true;
            }
            // 取消上一次语言加载任务，防止多次快速切换引发冲突
            _languageCts?.Cancel();
            _languageCts?.Dispose();
            _languageCts = new CancellationTokenSource();
            CancellationToken token = _languageCts.Token;

            try
            {
                // 如果缓存不存在该语言表，则调用Provider加载
                if (!_cache.ContainsTable(language))
                {
                    LocalizationTable table = await _provider.LoadTableAsync(language, token);
                    token.ThrowIfCancellationRequested();
                    if (table == null)
                    {
                        Log.Error("语言加载失败: {0}", language);
                        return false;
                    }
                    AddTable(language, table);
                }
                token.ThrowIfCancellationRequested();
                _currentLanguage = language;
                OnLanguageChanged?.Invoke(language);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        /// <summary>
        /// 预加载指定语言表到缓存，不切换当前语言
        /// </summary>
        /// <param name="language">需要预加载的语言</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>true预加载成功；false失败/取消</returns>
        public async Task<bool> PreloadLanguageAsync(E_Language language, CancellationToken cancellationToken = default)
        {
            if (!_initialized)
            {
                return false;
            }
            if (_cache.ContainsTable(language))
            {
                return true;
            }
            try
            {
                LocalizationTable table = await _provider.LoadTableAsync(language, cancellationToken);
                if (table == null)
                {
                    return false;
                }
                AddTable(language, table);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
        #endregion

        #region Text
        /// <summary>
        /// 根据Key获取本地化文本
        /// </summary>
        /// <param name="key">本地化键名</param>
        /// <returns>本地化文本；找不到Key则直接返回Key字符串</returns>
        public string GetText(string key)
        {
            LocalizationEntry entry = GetEntry(key);
            if (entry == null)
            {
                return key;
            }
            if (entry.Type != E_LocalizationType.Text)
            {
                Log.Error("Key 不是 Text 类型: {0}", key);
                return key;
            }
            return entry.Value ?? string.Empty;
        }

        /// <summary>
        /// 获取本地化文本并进行string.Format参数填充
        /// </summary>
        /// <param name="key">本地化键名</param>
        /// <param name="args">格式化参数列表</param>
        /// <returns>填充后的文本，格式化异常时返回原始文本</returns>
        public string Format(string key, params object[] args)
        {
            string text = GetText(key);
            if (args == null || args.Length == 0)
            {
                return text;
            }
            try
            {
                return string.Format(text, args);
            }
            catch (FormatException e)
            {
                Log.Error("本地化格式化失败\nKey: {0}\nText: {1}\nError: {2}", key, text, e.Message);
                return text;
            }
        }
        #endregion

        #region Sprite
        /// <summary>
        /// 根据Key异步获取本地化Sprite，优先读取缓存，无缓存则通过Provider加载
        /// </summary>
        /// <param name="key">本地化键名</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>Sprite实例；Key不存在/类型错误/加载失败返回null</returns>
        public async Task<Sprite> GetSpriteAsync(string key, CancellationToken cancellationToken = default)
        {
            LocalizationEntry entry = GetEntry(key);
            if (entry == null)
            {
                return null;
            }
            if (entry.Type != E_LocalizationType.Sprite)
            {
                Log.Error("Key 不是 Sprite 类型: {0}", key);
                return null;
            }
            if (string.IsNullOrEmpty(entry.AssetPath))
            {
                Log.Error("Sprite AssetPath 为空: {0}", key);
                return null;
            }
            string assetPath = entry.AssetPath;
            // 优先读取缓存
            if (_cache.TryGetSprite(assetPath, out Sprite cachedSprite))
            {
                return cachedSprite;
            }
            try
            {
                Sprite sprite = await _provider.LoadSpriteAsync(assetPath, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (sprite == null)
                {
                    return null;
                }
                _cache.SetSprite(assetPath, sprite);
                return sprite;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }
        #endregion

        #region Entry
        /// <summary>
        /// 查找本地化条目，优先当前语言，找不到自动回退到默认语言
        /// </summary>
        /// <param name="key">本地化键名</param>
        /// <returns>本地化条目，找不到返回null</returns>
        private LocalizationEntry GetEntry(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            // 先查找当前语言表
            if (_cache.TryGetTable(_currentLanguage, out Dictionary<string, LocalizationEntry> currentTable))
            {
                if (currentTable.TryGetValue(key, out LocalizationEntry entry))
                {
                    return entry;
                }
            }
            // 当前语言未找到，使用默认语言兜底
            if (_currentLanguage != _defaultLanguage)
            {
                if (_cache.TryGetTable(_defaultLanguage, out Dictionary<string, LocalizationEntry> defaultTable))
                {
                    if (defaultTable.TryGetValue(key, out LocalizationEntry entry))
                    {
                        return entry;
                    }
                }
            }
            Log.Error("找不到本地化 Key: {0}", key);
            return null;
        }
        #endregion

        #region Table
        /// <summary>
        /// 将原始LocalizationTable转为字典结构并存入缓存，自动过滤空条目、空Key、重复Key
        /// </summary>
        /// <param name="language">对应语言</param>
        /// <param name="table">原始解析出来的本地化数据表</param>
        private void AddTable(E_Language language, LocalizationTable table)
        {
            Dictionary<string, LocalizationEntry> dictionary = new();
            if (table.entries != null)
            {
                for (int i = 0; i < table.entries.Count; i++)
                {
                    LocalizationEntry entry = table.entries[i];
                    if (entry == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(entry.Key))
                    {
                        Log.Warning("发现空的本地化 Key");
                        continue;
                    }
                    if (dictionary.ContainsKey(entry.Key))
                    {
                        Log.Warning("重复的本地化 Key: {0}", entry.Key);
                        continue;
                    }
                    dictionary.Add(entry.Key, entry);
                }
            }
            _cache.SetTable(language, dictionary);
        }
        #endregion

        #region Check
        /// <summary>
        /// 判断是否存在指定Key（含Fallback默认语言查找）
        /// </summary>
        /// <param name="key">本地化键名</param>
        /// <returns>true存在</returns>
        public bool HasKey(string key)
        {
            return GetEntry(key) != null;
        }

        /// <summary>
        /// 获取目标语言对比默认语言缺失的Key列表，用于本地化查漏
        /// </summary>
        /// <param name="language">待检查语言</param>
        /// <returns>缺失的Key集合</returns>
        public List<string> GetMissingKeys(E_Language language)
        {
            List<string> result = new();
            if (!_cache.TryGetTable(_defaultLanguage, out var defaultTable))
            {
                return result;
            }
            if (!_cache.TryGetTable(language, out var targetTable))
            {
                result.AddRange(defaultTable.Keys);
                return result;
            }
            foreach (string key in defaultTable.Keys)
            {
                if (!targetTable.ContainsKey(key))
                {
                    result.Add(key);
                }
            }
            return result;
        }
        #endregion

        #region Clear
        /// <summary>
        /// 销毁管理器，取消异步任务、清理Provider与缓存，重置状态
        /// </summary>
        public void Clear()
        {
            _languageCts?.Cancel();
            _languageCts?.Dispose();
            _languageCts = null;
            _provider?.Clear();
            _cache?.Clear();
            _provider = null;
            _cache = null;
            _initialized = false;
        }
        #endregion
    }
}
