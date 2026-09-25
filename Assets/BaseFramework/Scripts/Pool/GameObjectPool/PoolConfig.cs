using UnityEngine;


namespace BaseFramework.Runtime
{
    /// <summary>挂载在需要使用对象池对象上，设置对象池中该对象的最大数量</summary>
    public class PoolConfig : MonoBehaviour
    {
        [Header("初始容量")]
        public int InitialCapacity = 10;

        [Header("最大容量")]
        public int MaxCapacity = 100;

        [Header("每次扩容数量")]
        public int ExpandAmount = 10;

        [Header("允许自动扩容")]
        public bool AutoExpand = true;

    }
}
