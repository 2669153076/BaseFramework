

using System;
using System.Reflection;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>普通的单例基类</summary>
    /// <summary>继承该类的子类需要实现私有构造函数</summary>
    /// <typeparam name="T">泛型T</typeparam>
    public abstract class SingletonNormal<T> where T : class
    {
        private static T _instance;

        public static T GetInstance()
        {
            if (_instance == null)
            {
                Type type = typeof(T);
                ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                if (info != null)
                {
                    _instance = info.Invoke(null) as T;
                }
                else
                {
                    //TODO:打印错误信息
                    Debug.LogError($"单例{typeof(T).FullName}为空");
                }
            }

            return _instance;
        }
    }
}
