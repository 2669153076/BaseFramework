using System;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>加了锁的单例基类</summary>
    /// <summary>继承该类的子类需要实现私有构造函数</summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonNormalLocked<T> where T : class
    {
        private static T _instance;

        private static readonly object lockObj = new object();


        public static T GetInstance()
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        try
                        {
                            //与GetConstructor相比
                            //无法做 “构造是否存在” 的提前判断，找不到直接抛异常
                            _instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                        catch (Exception e)
                        {
                            //TODO:打印错误信息
                            Debug.LogError($"实例化单例{typeof(T).FullName}错误。");
                            Debug.LogException(e);
                        }
                    }
                }
            }
            return _instance;
        }

    }
}