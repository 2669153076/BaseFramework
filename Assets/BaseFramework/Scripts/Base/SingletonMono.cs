using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>继承MonoBehaviour的单例基类</summary>
    /// <typeparam name="T">泛型T</typeparam>
    [DisallowMultipleComponent]
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T GetInstance()
        {
            return _instance;
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
