
using System;
using UnityEngine;
namespace BaseFramework.Runtime
{
    /// <summary>
    /// Resources资源管理器
    /// </summary>
    public sealed partial class ResMgr : SingletonNormalLocked<ResMgr>
    {
        /// <summary>
        /// 资源信息基类
        /// </summary>
        private class ResInfoBase
        {
            /// <summary>
            /// 引用计数
            /// </summary>
            public int refCount;
            /// <summary>
            /// 资源加载状态
            /// </summary>
            public EResState resState;
            /// <summary>
            /// 加载资源协程
            /// </summary>
            public Coroutine loadCoroutine;

            public void AddRefCount() 
            {
                ++refCount;
            }
            public void SubRefCount() {
                --refCount;
                if (refCount < 0)
                {
                    Debug.LogError("引用计数小于0，请检查加载-卸载是否一一对应");
                }
            }
        }
    }
}
