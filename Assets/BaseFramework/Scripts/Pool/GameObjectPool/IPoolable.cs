namespace BaseFramework.Runtime
{
    /// <summary>对象池对象生命周期接口</summary>
    public interface IPoolable
    {
        /// <summary>从对象池取出时调用</summary>
        void OnSpawn();

        /// <summary>回收到对象池时调用</summary>
        void OnDespawn();
    }
}
