using System.Collections.Generic;
using UnityEngine;
namespace BaseFramework.Runtime
{
    /// <summary>
    /// 本地化资源缓存
    /// 缓存已加载的多语言文本表与本地化精灵，避免重复加载
    /// </summary>
    public sealed class LocalizationCache
    {
        /// <summary>
        /// 多语言文本表缓存：Key=语言枚举，Value=该语言下的键值文本字典
        /// </summary>
        private readonly Dictionary<E_Language, Dictionary<string, LocalizationEntry>> _tables = new();

        /// <summary>
        /// 本地化精灵缓存：Key=资源路径，Value=Sprite资源
        /// </summary>
        private readonly Dictionary<string, Sprite> _spriteCache = new();

        /// <summary>
        /// 尝试获取指定语言的本地化文本表
        /// </summary>
        /// <param name="language">目标语言</param>
        /// <param name="table">输出：语言文本字典，不存在则为null</param>
        /// <returns>true=缓存中存在该语言表</returns>
        public bool TryGetTable(E_Language language, out Dictionary<string, LocalizationEntry> table)
        {
            return _tables.TryGetValue(language, out table);
        }

        /// <summary>
        /// 判断缓存中是否已存在指定语言表
        /// </summary>
        /// <param name="language">目标语言</param>
        /// <returns>true=存在</returns>
        public bool ContainsTable(E_Language language)
        {
            return _tables.ContainsKey(language);
        }

        /// <summary>
        /// 将语言文本表存入缓存，覆盖已有数据
        /// </summary>
        /// <param name="language">目标语言</param>
        /// <param name="table">语言文本字典</param>
        public void SetTable(E_Language language, Dictionary<string, LocalizationEntry> table)
        {
            _tables[language] = table;
        }

        /// <summary>
        /// 尝试从缓存获取本地化精灵
        /// </summary>
        /// <param name="assetPath">精灵资源路径</param>
        /// <param name="sprite">输出：精灵资源，不存在则为null</param>
        /// <returns>true=缓存命中</returns>
        public bool TryGetSprite(string assetPath, out Sprite sprite)
        {
            return _spriteCache.TryGetValue(assetPath, out sprite);
        }

        /// <summary>
        /// 将精灵资源存入缓存
        /// </summary>
        /// <param name="assetPath">精灵资源路径</param>
        /// <param name="sprite">精灵资源</param>
        public void SetSprite(string assetPath, Sprite sprite)
        {
            _spriteCache[assetPath] = sprite;
        }

        /// <summary>
        /// 清空全部缓存（文本表+精灵缓存）
        /// </summary>
        public void Clear()
        {
            _tables.Clear();
            _spriteCache.Clear();
        }

        /// <summary>
        /// 仅清空所有语言文本表缓存
        /// </summary>
        public void ClearTables()
        {
            _tables.Clear();
        }

        /// <summary>
        /// 仅清空精灵资源缓存
        /// </summary>
        public void ClearSprites()
        {
            _spriteCache.Clear();
        }
    }
}
