
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// Resources资源管理器
    /// </summary>
    public sealed partial class ResMgr : SingletonNormalLocked<ResMgr>
    {
        /// <summary>
        /// 资源状态
        /// </summary>
        private enum EResState
        {
            /// <summary>
            /// 加载中
            /// </summary>
            Loading,
            /// <summary>
            /// 加载完毕
            /// </summary>
            Done,
            /// <summary>
            /// 加载失败
            /// </summary>
            Failed,
            /// <summary>
            /// 待删除
            /// </summary>
            PendDel
        }
    }
}
