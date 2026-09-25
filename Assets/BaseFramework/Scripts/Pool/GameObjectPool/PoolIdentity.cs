using UnityEngine;

namespace BaseFramework.Runtime
{
    public sealed class PoolIdentity : MonoBehaviour
    {
        public Pool SelfPool { get; private set; }
        public bool IsBound => SelfPool != null;

        public IPoolable Poolable { get; private set; }

        private void Awake()
        {
            Poolable = GetComponent<IPoolable>();
        }

        internal void BindPool(Pool pool)
        {
            SelfPool = pool;
        }

        /// <summary>回收到池子中</summary>
        /// <returns></returns>
        public bool ReturnToPool()
        {
            if (SelfPool == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"{gameObject.name}没有绑定对象池，无法回收");
                return false;
            }

            return SelfPool.Return(gameObject);
        }

        /// <summary>从对象池中彻底销毁</summary>
        /// <returns></returns>
        public bool DestoryFromPool()
        {
            if (SelfPool == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"{gameObject.name}没有绑定对象池，无法销毁");
                return false;
            }
            return SelfPool.Destroy(gameObject);
        }

        internal void CallSpawn()
        {
            Poolable?.OnSpawn();
        }
        internal void CallDespawn()
        {
            Poolable?.OnDespawn();
        }
        /// <summary>解除绑定</summary>
        internal void UnbindPool()
        {
            SelfPool = null;
        }
    }
}
