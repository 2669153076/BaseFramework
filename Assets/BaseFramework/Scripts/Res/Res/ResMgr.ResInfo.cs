
using UnityEngine;
using UnityEngine.Events;
namespace BaseFramework.Runtime
{
    /// <summary>
    /// Resources资源管理器
    /// </summary>
    public sealed partial class ResMgr : SingletonNormalLocked<ResMgr>
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        private class ResInfo<T> :ResInfoBase where T : UnityEngine.Object
        {
            /// <summary>
            /// 资源引用
            /// </summary>
            public T asset;

            /// <summary>
            /// 回调函数
            /// </summary>
            public System.Action<T> onComplete;


        }
    }
}
