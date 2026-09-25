
using System;

namespace BaseFramework.Runtime
{
    public sealed partial class EventCenter : SingletonNormal<EventCenter>
    {
        /// <summary>
        /// 无参事件表
        /// </summary>
        private sealed class EventTable : IEventTable
        {
            public EventId EventId { get; }

            public Type EventType => null;

            public bool IsEmpty => _listeners == null;

            private Action _listeners;

            public EventTable(EventId id)
            {
                EventId = id;
            }

            public void Add(Action listener)
            {
                _listeners += listener;
            }

            public void Remove(Action listener)
            {
                _listeners -= listener;
            }

            public void Invoke()
            {
                _listeners?.Invoke();
            }

            public void Clear()
            {
                _listeners = null;
            }
        }

        public sealed class EventTable<T> : IEventTable
        {
            public EventId EventId { get; }

            public Type EventType => typeof(T);

            public bool IsEmpty => _listeners == null;

            private Action<T> _listeners;

            public EventTable(EventId id)
            {
                EventId = id;
            }

            public void Add(Action<T> listener)
            {
                _listeners += listener;
            }

            public void Remove(Action<T> listener)
            {
                _listeners -= listener;
            }

            public void Invoke(T data)
            {
                _listeners?.Invoke(data);
            }


            public void Clear()
            {
                _listeners = null;
            }
        }
    }
}