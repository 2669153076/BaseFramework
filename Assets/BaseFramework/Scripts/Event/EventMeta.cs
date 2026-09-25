using System;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>事件元数据</summary>
    [Serializable]
    public class EventMeta
    {
        [Tooltip("事件Id")]
        public EventId eventId;
        [Tooltip("事件名")]
        public string eventName;
        [Tooltip("事件描述")]
        public string eventDescription;

        public int paramCount;  //参数个数，仅用于编辑器调试
    }
}