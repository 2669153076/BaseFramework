
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 引用池
    /// </summary>
    public partial class ReferencePool : SingletonNormalLocked<ReferencePool>
    {
        private readonly Dictionary<Type, ReferenceCollection> _referenceDict = new();
        private bool _enableStrictCheck = false;    //是否启用严格检查
        /// <summary>
        /// 是否启用严格检查
        /// </summary>
        public bool EnableStrictCheck
        {
            get { return _enableStrictCheck; }
            set { _enableStrictCheck = value; }
        }
        /// <summary>
        /// 获取引用池数量
        /// </summary>
        public int Count => _referenceDict.Count;

        /// <summary>
        /// 获取所有引用池的信息
        /// </summary>
        /// <returns></returns>
        public ReferencePoolInfo[] GetAllReferencePoolInfos()
        {
            int index = 0;
            ReferencePoolInfo[] infos = null;

            lock (_referenceDict)
            {
                infos = new ReferencePoolInfo[_referenceDict.Count];
                foreach (var item in _referenceDict)
                {
                    infos[index++] = new ReferencePoolInfo(item.Key, item.Value.UnusedReferenceCount, item.Value.UsingReferenceCount, item.Value.AcquireReferenceCount, item.Value.ReleaseReferenceCount, item.Value.AddReferenceCount, item.Value.RemoveReferenceCount);
                }
            }

            return infos;
        }

        /// <summary>
        /// 清理所有引用对象
        /// </summary>
        public void ClearAll()
        {
            lock (_referenceDict)
            {
                foreach (var item in _referenceDict)
                {
                    item.Value.RemoveAll();
                }
                _referenceDict.Clear();
            }
        }

        /// <summary>
        /// 从池子中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Acquire<T>()where T : class, IReference, new()
        {
            return GetReferenceCollection(typeof(T)).Acquire<T>();
        }
        /// <summary>
        /// 从池子中获取对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IReference Acquire(Type type)
        {
            CheckType(type);
            return GetReferenceCollection(type).Acquire(type);
        }
        /// <summary>
        /// 向池子中归还对象
        /// </summary>
        /// <param name="pool">引用对象</param>
        public void Release(IReference pool)
        {
            if (pool == null)
            {
                Log.Error("传入参数为空");
            }
            
            Type type = pool.GetType();
            CheckType(type);
            GetReferenceCollection(type).Release(pool);
        }
        /// <summary>
        /// 向对象池中添加一定数量的引用对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">添加数量</param>
        public void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Add(count);
        }
        /// <summary>
        /// 向对象池中添加一定数量的引用对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count">添加数量</param>
        public void Add(Type type,int count)
        {
            GetReferenceCollection(type).Add(count);
        }
        /// <summary>
        /// 从池子中彻底移除引用对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">要移除的数量</param>
        public void Remove<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Remove(count);
        }
        /// <summary>
        /// 从池子中彻底移除引用对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count">要移除的数量</param>
        public void Remove(Type type, int count)
        {
            CheckType(type);
            GetReferenceCollection(type).Remove(count);
        }
        /// <summary>
        /// 从池子中彻底移除所有引用对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveAll<T>() where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).RemoveAll();
        }
        /// <summary>
        /// 从池子中彻底移除指定引用对象
        /// </summary>
        /// <param name="type"></param>
        public void RemoveAll(Type type)
        {
            CheckType(type);
            GetReferenceCollection(type).RemoveAll();
        }

        /// <summary>
        /// 获取引用对象集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ReferenceCollection GetReferenceCollection(Type type)
        {
            if(type == null)
            {
                Log.Error("传入类型为空");
            }

            ReferenceCollection result = null;
            lock (_referenceDict)
            {
                if(!_referenceDict.TryGetValue(type, out result))
                {
                    result = new ReferenceCollection(type,_enableStrictCheck);
                    _referenceDict.Add(type, result);
                }
            }
            return result;
        }
        /// <summary>
        /// 检查对象类型
        /// </summary>
        /// <param name="type"></param>
        private void CheckType(Type type)
        {
            //释放启用了严格检查
            if (!_enableStrictCheck)
            {
                return;
            }
            if (type == null)
            {
                Log.Error("传入类型为空");
            }
            //判断类型是不是class，或者是不是抽象类
            if(!type.IsClass || type.IsAbstract)
            {
                Log.Error("传入类型应该是一个非抽象类");
            }
            if(!typeof(IReference).IsAssignableFrom(type))
            {
                Log.Error("不是一个继承了IReference接口的类:{0}", type.FullName);
            }
        }

    }
}