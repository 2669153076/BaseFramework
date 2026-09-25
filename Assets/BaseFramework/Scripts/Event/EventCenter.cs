using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>事件中心</summary>
    public sealed partial class EventCenter : SingletonNormal<EventCenter>
    {
        private EventCenter() { }

        private readonly Dictionary<EventId, IEventTable> _eventDict = new(); //事件列表

        /// <summary>触发事件</summary>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        public void EventTrigger(EventId id)
        {

            if (!_eventDict.TryGetValue(id, out var iet))
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"执行事件监听失败：事件未注册，EventId={id}");
                return;
            }
            if (iet is not EventTable et)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"执行事件监听失败：EventId={id} 参数类型不匹配");
                return;
            }

            et.Invoke();
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        /// <param name="data">参数</param>
        public void EventTrigger<T>(EventId id, T data)
        {
            if (!_eventDict.TryGetValue(id, out var iet))
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"执行事件监听失败：事件未注册，EventId={id}");
                return;
            }
            if (iet is not EventTable<T> et)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"执行事件监听失败：EventId={id} 参数类型不匹配");
                return;
            }

            et.Invoke(data);
        }

        /// <summary>添加事件监听</summary>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        /// <param name="listener">监听函数</param>
        public void AddEventListener(EventId id, Action listener)
        {
            if (listener == null)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"添加事件监听失败：listener为空，EventId={id}");
                return;
            }

            if (_eventDict.TryGetValue(id, out var iet))
            {
                if (iet is not EventTable et)
                {
                    //检索到了事件id，但类型冲突
                    //TODO:打印错误日志
                    Debug.LogError(
                        $"事件参数类型冲突：EventId={id} 已经注册为 {iet.EventType}");
                    return;
                }

                //检索到了事件id，类型不冲突
                et.Add(listener);
                return;
            }

            //没有对应事件id，就新增
            var newTable = new EventTable(id);
            newTable.Add(listener);
            _eventDict.Add(id, newTable);
        }
        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <typeparam name="T">监听函数参数类型</typeparam>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        /// <param name="listener">监听函数</param>
        public void AddEventListener<T>(EventId id, Action<T> listener)
        {
            if (listener == null)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"添加事件监听失败：listener为空，EventId={id}");
                return;
            }

            if (_eventDict.TryGetValue(id, out var iet))
            {
                if (iet is not EventTable<T> et)
                {
                    //检索到了事件id，但类型冲突
                    //TODO:打印错误日志
                    Debug.LogError(
                        $"事件参数类型冲突：EventId={id} 已经注册为 {iet.EventType}");
                    return;
                }

                et.Add(listener);
                return;
            }

            EventTable<T> newTable = new(id);
            newTable.Add(listener);
            _eventDict.Add(id, newTable);
        }

        /// <summary>移除事件监听</summary>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        /// <param name="listener">监听函数</param>
        public void RemoveEventListener(EventId id, Action listener)
        {
            if (listener == null)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：listener为空，EventId={id}");
                return;
            }

            if (!_eventDict.TryGetValue(id, out var iet))
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：事件未注册，EventId={id}");
                return;
            }
            if (iet is not EventTable et)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：EventId={id} 参数类型不匹配");
                return;
            }

            et.Remove(listener);

            if (et.IsEmpty)
            {
                _eventDict.Remove(id);
            }
        }
        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <typeparam name="T">监听函数的参数类型</typeparam>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        /// <param name="listener">监听函数</param>
        public void RemoveEventListener<T>(EventId id, Action<T> listener)
        {
            if (listener == null)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：listener为空，EventId={id}");
                return;
            }

            if (!_eventDict.TryGetValue(id, out var iet))
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：事件未注册，EventId={id}");
                return;
            }
            if (iet is not EventTable<T> et)
            {
                //TODO:打印错误日志
                Debug.LogError(
                    $"移除事件监听失败：EventId={id} 参数类型不匹配");
                return;
            }

            et.Remove(listener);
            if (et.IsEmpty)
            {
                _eventDict.Remove(id);
            }
        }

        /// <summary>清理指定事件名的监听</summary>
        /// <param name="id">事件Id，需要在EventConfig中添加配置</param>
        public void Clear(EventId id)
        {
            if (_eventDict.TryGetValue(id, out var iet))
            {
                iet.Clear();
                _eventDict.Remove(id);
            }
        }
        /// <summary>清理所有事件监听</summary>
        public void ClearAll()
        {
            foreach (var item in _eventDict.Values)
            {
                item.Clear();
            }

            _eventDict.Clear();
        }
    }
}