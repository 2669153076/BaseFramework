using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>自动挂载MonoBehaviour的单例基类</summary>
    /// <typeparam name="T">泛型T</typeparam>
    public abstract class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T GetInstance()
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject(typeof(T).FullName);
                _instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }

        protected virtual void OnDestroy() { }
    }
}