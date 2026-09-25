
using System;

namespace BaseFramework.Runtime
{
    public sealed partial class EventCenter : SingletonNormal<EventCenter>
    {
        /// <summary>
        /// 非泛型事件表接口
        /// </summary>
        private interface IEventTable
        {
            EventId EventId { get; }
            Type EventType { get; }
            bool IsEmpty { get; }
            void Clear();
        }
    }
}