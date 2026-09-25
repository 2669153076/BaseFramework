using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class LogBase
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public enum E_LogLevel : byte
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug = 0,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 错误
            /// </summary>
            Error,
            /// <summary>
            /// 严重错误
            /// </summary>
            Fatal
        }
    }
}

