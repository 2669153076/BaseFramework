
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseFramework.Runtime
{
    /// <summary>全局Mono管理类</summary>
    /// <summary>没有继承Mono behaviour的类却需要使用生命周期时，使用该类</summary>
    public class MonoMgr : SingletonAutoMono<MonoMgr>
    {
        private event UnityAction _updateEvent;
        private event UnityAction _lateUpdateEvent;
        private event UnityAction _fixedUpdateEvent;

        private HashSet<Coroutine> _coroutineHashSet = new();

        /// <summary>全局是否暂停更新</summary>
        public bool IsPause { get; set; }

        /// <summary>添加Update事件</summary>
        /// <param name="updateFun"></param>
        public void AddUpdateListener(UnityAction updateFun)
        {
            _updateEvent += updateFun;
        }
        /// <summary>移除Update事件</summary>
        /// <param name="updateFun"></param>
        public void RemoveUpdateListener(UnityAction updateFun)
        {
            _updateEvent -= updateFun;
        }
        /// <summary>添加FixedUpdate事件</summary>
        /// <param name="fixedUpdateFun"></param>
        public void AddFixedUpdateListener(UnityAction fixedUpdateFun)
        {
            _fixedUpdateEvent += fixedUpdateFun;
        }
        /// <summary>移除FixedUpdate事件</summary>
        /// <param name="fixedUpdateFun"></param>
        public void RemoveFixedUpdateListener(UnityAction fixedUpdateFun)
        {
            _fixedUpdateEvent -= fixedUpdateFun;
        }
        /// <summary>添加LateUpdate事件</summary>
        /// <param name="lateUpdateFun"></param>
        public void AddLateUpdateListener(UnityAction lateUpdateFun)
        {
            _lateUpdateEvent += lateUpdateFun;
        }
        /// <summary>移除LateUpdate事件</summary>
        /// <param name="lateUpdateFun"></param>
        public void RemoveLateUpdateListener(UnityAction lateUpdateFun)
        {
            _lateUpdateEvent -= lateUpdateFun;
        }

        public void ClearAllListener()
        {
            _updateEvent = null;
            _fixedUpdateEvent = null;
            _lateUpdateEvent = null;
        }


        public Coroutine StartMyCoroutine(IEnumerator routine)
        {
            Coroutine co = null;

            //包装一层，使协程执行完毕后自动清理，避免脏数据
            IEnumerator Wrapper()
            {
                try
                {
                    yield return routine;
                }
                finally
                {
                    if (co != null)
                    {
                        _coroutineHashSet.Remove(co);
                    }
                }
            }

            co = StartCoroutine(Wrapper());
            _coroutineHashSet.Add(co);
            return co;
        }

        public void StopMyCoroutine(Coroutine coroutine)
        {
            if (coroutine == null)
            {
                //TODO:打印错误日志
                Debug.LogError("传入的协程为空");
                return;
            }
            if (_coroutineHashSet.Remove(coroutine))
            {
                StopCoroutine(coroutine);
            }
        }
        public void StopAllMyCoroutine()
        {
            foreach (Coroutine coroutine in _coroutineHashSet)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
            _coroutineHashSet.Clear();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ClearAllListener();
            StopAllMyCoroutine();
        }


        private void Update()
        {
            if (IsPause)
                return;
            Invoke(_updateEvent);
        }

        private void FixedUpdate()
        {
            if (IsPause)
                return;
            Invoke(_fixedUpdateEvent);
        }

        private void LateUpdate()
        {
            if (IsPause)
                return;
            Invoke(_lateUpdateEvent);
        }


        private void Invoke(UnityAction evt)
        {
            if (evt == null)
                return;

#if UNITY_EDITOR
            Delegate[] delegates = evt.GetInvocationList();
            foreach (Delegate del in delegates)
            {
                if (del is UnityAction action)
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {

                        //TODO:打印错误日志
                        Debug.LogError($"事件监听错误:{e}");
                    }
                }
                else
                {
                    //TODO:打印错误日志
                    Debug.LogError($"事件{del.Target?.GetType().Name}.{del.Method.Name}不是UnityAction类型");
                }
            }

#else
            evt?.Invoke();
#endif
        }
    }
}
