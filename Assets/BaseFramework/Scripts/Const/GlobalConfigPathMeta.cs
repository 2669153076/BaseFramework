using System;

namespace BaseFramework.Runtime
{
    /// <summary>路径总表白数据模型，对应Excel表</summary>
    [Serializable]
    public class GlobalConfigPathMeta
    {
        public int id;
        public string path;
        public string description;
    }
}