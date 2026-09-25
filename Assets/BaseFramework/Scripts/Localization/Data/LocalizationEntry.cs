using System;

namespace BaseFramework.Runtime
{
    [Serializable]
    public class LocalizationEntry
    {
        public string Key;

        public E_LocalizationType Type;

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Value;

        /// <summary>
        /// 资源路径：Sprite 类型使用
        /// 例如：
        /// UI/Localization/Chinese/Title
        /// </summary>
        public string AssetPath;
    }
}