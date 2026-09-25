using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseFramework.Runtime
{
    public sealed partial class ReferencePool : SingletonNormalLocked<ReferencePool>
    {
        /// <summary>
        /// 引用对象集合
        /// </summary>
        private sealed class ReferenceCollection
        {
            private readonly Queue<IReference> _references;   //引用对象集合
            private readonly Type _type;
            private  int _usingReferenceCount;  //使用中的引用数量
            private  int _acquireReferenceCount;    //获取的数量
            private  int _releaseReferenceCount;    //归还的数量
            private  int _addReferenceCount;    //增加的数量
            private  int _removeReferenceCount; //移除的数量
            private bool _enableStrictCheck = false;    //释放启动类型检查

            public ReferenceCollection(Type type,bool enableStrictCheck)
            {
                this._references = new Queue<IReference>();
                this._type = type;
                this._usingReferenceCount = 0;
                this._acquireReferenceCount = 0;
                this._releaseReferenceCount = 0;
                this._addReferenceCount = 0;
                this._removeReferenceCount = 0;
                this._enableStrictCheck = enableStrictCheck;
            }
            #region 属性

            /// <summary>
            /// 类型
            /// </summary>
            public Type ReferenceType
            {
                get
                {
                    return _type;
                }
            }
            /// <summary>
            /// 没有使用的数量
            /// </summary>
            public int UnusedReferenceCount
            {
                get
                {
                    return _references.Count;
                }
            }
            /// <summary>
            /// 使用中的数量
            /// </summary>
            public int UsingReferenceCount
            {
                get
                {
                    return _usingReferenceCount;
                }
            }
            /// <summary>
            /// 请求的数量
            /// </summary>
            public int AcquireReferenceCount
            {
                get
                {
                    return _acquireReferenceCount;
                }
            }
            /// <summary>
            /// 归还的数量
            /// </summary>
            public int ReleaseReferenceCount
            {
                get
                {
                    return _releaseReferenceCount;
                }
            }
            /// <summary>
            /// 创建的数量
            /// </summary>
            public int AddReferenceCount
            {
                get
                {
                    return _addReferenceCount;
                }
            }
            /// <summary>
            /// 移除的数量
            /// </summary>
            public int RemoveReferenceCount
            {
                get
                {
                    return _removeReferenceCount;
                }
            }

            #endregion

            /// <summary>
            /// 请求引用对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T Acquire<T>() where T : class, IReference, new()
            {
                if (typeof(T) != _type)
                {
                    Log.Error("类型错误");
                    return null;
                }

                _usingReferenceCount++;
                _acquireReferenceCount++;

                lock (_references)
                {
                    if (_references.Count > 0)
                    {
                        return (T)_references.Dequeue();
                    }
                }

                _addReferenceCount++;
                return new T();

            }
            /// <summary>
            /// 请求引用对象
            /// </summary>
            /// <returns></returns>
            public IReference Acquire(Type type)
            {
                if (type != this._type)
                {
                    Log.Error("类型错误");
                    return null;
                }

                _usingReferenceCount++;
                _acquireReferenceCount++;
                lock(_references)
                {
                    if( _references.Count > 0)
                    {
                        return _references.Dequeue();
                    }
                }

                _addReferenceCount++;
                return (IReference)Activator.CreateInstance(type);
            }
            /// <summary>
            /// 释放指定引用对象
            /// </summary>
            /// <param name="reference"></param>
            public void Release(IReference reference)
            {
                if (reference == null)
                {
                    Log.Error("Release reference is null");
                    return;
                }

                reference.Clear();
                lock (_references)
                {
                    if (_enableStrictCheck&&_references.Contains(reference)){
                        Log.Warning("对象已经被归还");
                        return;
                    }
                    _references.Enqueue(reference);
                }
                _releaseReferenceCount++;
                _usingReferenceCount--;
            }
            /// <summary>
            /// 生成指定数量
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="count">生成数量</param>
            public void Add<T>(int count) where T : class, IReference, new()
            {
                if (typeof(T) != _type)
                {
                    Log.Error("类型错误");
                }

                lock (_references)
                {
                    _addReferenceCount += count;
                    while (count-- > 0)
                    {
                        _references.Enqueue(new T());
                    }
                }
            }
            /// <summary>
            /// 生成指定数量的引用对象
            /// </summary>
            /// <param name="count">生成数量</param>
            public void Add(int count)
            {
                lock(_references)
                {
                    _addReferenceCount += count;
                    while(count-- > 0)
                    {
                        _references.Enqueue((IReference)Activator.CreateInstance(_type));
                    }
                }
            }
            /// <summary>
            /// 移除指定数量的引用对象
            /// </summary>
            /// <param name="count">移除数量</param>
            public void Remove(int count)
            {
                lock (_references)
                {
                    if (count > _references.Count)
                    {
                        count = _references.Count;
                    }
                    _removeReferenceCount+=count;
                    while (count-- > 0)
                    {
                        _references.Dequeue();
                    }
                }
            }
            /// <summary>
            /// 移除所有引用对象
            /// </summary>
            public void RemoveAll()
            {
                lock ( _references)
                {
                    _removeReferenceCount+= _references.Count;
                    _references.Clear();
                }
            }

        }
    }
}