using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace BaseFramework.Runtime{
        /// <summary>
        /// 游戏框架异常，用于区分普通C#异常
        /// </summary>
        [Serializable]
        public class FrameworkException : System.Exception
        {
            public FrameworkException() : base() { }
            public FrameworkException(string message) : base(message) { }
            public FrameworkException(string message, System.Exception innerException) : base(message, innerException) { }
            public FrameworkException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        }
    }
