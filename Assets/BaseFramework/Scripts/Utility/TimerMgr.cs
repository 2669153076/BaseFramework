using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 计时器管理器
    /// </summary>
    public partial class TimerMgr : SingletonNormal<TimerMgr>
    {
        private int TIMER_KEY = 0;  //记录当前要创建的唯一ID
        private Dictionary<int, TimerItem> _timerDict = new();   //记录会受到TimeScale影响的计时器
        private Dictionary<int, TimerItem> _realTimerDict = new();   //记录不会受到TimeScale影响的计时器
        private List<TimerItem> _cacheTimerList = new();
        public const float _intervalTime = 0.1f;    //执行计时器的间隔时间

        private Coroutine _startCoroutine = null;
        private Coroutine _startRealCoroutine = null;

        private WaitForSecondsRealtime _startWaitForSecondsRealtime = new(_intervalTime);
        private WaitForSeconds _startWaitForSeconds = new(_intervalTime);

        private TimerMgr() { }

        //启动计时器管理器
        public void Start()
        {
            _startCoroutine = MonoMgr.GetInstance().StartMyCoroutine(StartCoroutine(false,_timerDict));
            _startRealCoroutine = MonoMgr.GetInstance().StartCoroutine(StartCoroutine(true,_realTimerDict));
        }
        /// <summary>
        /// 协程
        /// </summary>
        /// <param name="isRealTime">是否会受到TimeScale的影响</param>
        /// <param name="timerDict">计时器字典</param>
        /// <returns></returns>
        private IEnumerator StartCoroutine(bool isRealTime,Dictionary<int,TimerItem> timerDict)
        {
            while(true)
            {
                if(isRealTime){
                    yield return _startWaitForSecondsRealtime;
                }
                else
                {
                    //每100毫秒进行一次计时
                    yield return _startWaitForSeconds;

                }

                foreach (var item in _timerDict.Values)
                {
                    if (!item._isRun)
                    {
                        continue;
                    }

                    //判断计时器是否有间隔执行的需求
                    if (item._intervalCallback != null)
                    {
                        item._intervalTime-=(int)(_intervalTime*1000f);
                        if (item._intervalTime <= 0)
                        {
                            item._intervalCallback.Invoke();
                            item._intervalTime = item._initIntervalTime;
                        }
                    }

                    item._totalTime -= (int)(_intervalTime * 1000f);
                    if (item._totalTime <= 0)
                    {
                        item._callback.Invoke();
                        _cacheTimerList.Add(item);
                    }
                }

                foreach (var item in _cacheTimerList)
                {
                    _timerDict.Remove(item._id);
                    ReferencePool.GetInstance().Release(item);  //向引用池中归还
                }
            }
        }


        //关闭计时器管理器
        public void Close()
        {
            MonoMgr.GetInstance().StopMyCoroutine(_startCoroutine);
            MonoMgr.GetInstance().StopMyCoroutine(_startRealCoroutine);
        }


        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="totalTime">计时器总时间（毫秒）</param>
        /// <param name="isReal">是否会受到TimeScale的影响<br/>true:不会<br/>false:会</param>
        /// <param name="callback">计时器结束回调</param>
        /// <param name="intervalTime">在一次计时中需要间隔的时间（毫秒）</param>
        /// <param name="intervalCallback">在一次计时中需要间隔的委托回调</param>
        /// <returns></returns>
        public int CreatTimer(uint totalTime,bool isReal, Action callback, uint intervalTime = 0, Action intervalCallback = null)
        {
            int keyId =  TIMER_KEY;

            TimerItem item = ReferencePool.GetInstance().Acquire<TimerItem>();
            item.InitInfo(keyId + TIMER_KEY, totalTime, callback, intervalTime, intervalCallback);

            if (isReal)
            {
                _realTimerDict.Add(keyId, item);
            }
            else
            {
                _timerDict.Add(keyId, item);
            }

            return keyId;
        }
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="totalTime">计时器总时间（毫秒）</param>
        /// <param name="isReal">是否会受到TimeScale的影响<br/>true:不会<br/>false:会</param>
        /// <param name="callback">计时器结束回调</param>
        /// <param name="intervalTime">在一次计时中需要间隔的时间（毫秒）</param>
        /// <param name="intervalCallback">在一次计时中需要间隔的委托回调</param>
        /// <returns></returns>
        public int CreatTimer(ulong totalTime,bool isReal, Action callback, ulong intervalTime = 0, Action intervalCallback = null)
        {
            int keyId = TIMER_KEY;

            TimerItem item = ReferencePool.GetInstance().Acquire<TimerItem>();
            item.InitInfo(keyId + TIMER_KEY, totalTime, callback, intervalTime, intervalCallback);

            if (isReal)
            {
                _realTimerDict.Add(keyId, item);
            }
            else
            {
                _timerDict.Add(keyId, item);
            }

            return keyId;
        }

        /// <summary>
        /// 移除计时器
        /// </summary>
        /// <param name="id">计时器id</param>
        /// <param name="isReal">是否会收到TimeScale的影响<br/>true:不会<br/>false:会</param>
        public void RemoveTimer(int id)
        {
            if (_timerDict.ContainsKey(id))
            {
                ReferencePool.GetInstance().Release(_timerDict[id]);
                _timerDict.Remove(id);
            }
            else if (_realTimerDict.ContainsKey(id))
            {

                ReferencePool.GetInstance().Release(_realTimerDict[id]);
                _realTimerDict.Remove(id);
            }
            else
            {
                Log.Error("找不到计时器：{0}", id);
            }
        }
        
        /// <summary>
        /// 重置计时器
        /// </summary>
        /// <param name="id"></param>
        public void ResetTimer(int id)
        {
            if (_timerDict.ContainsKey(id))
            {
                _timerDict[id].ResetTimer();
            }
            else if (_realTimerDict.ContainsKey(id))
            {

                _realTimerDict[id].ResetTimer();
            }
            else
            {
                Log.Error("找不到计时器：{0}", id);
            }
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        /// <param name="id"></param>
        public void StartTimer(int id)
        {
            if (_timerDict.ContainsKey(id))
            {
                _timerDict[id]._isRun = true;
            }
            else if (_realTimerDict.ContainsKey(id))
            {

                _realTimerDict[id]._isRun = true;
            }
            else
            {
                Log.Error("找不到计时器：{0}", id);
            }
        }

        /// <summary>
        /// 暂停计时器
        /// </summary>
        /// <param name="id"></param>
        public void PauseTimer(int id)
        {
            if (_timerDict.ContainsKey(id))
            {
                _timerDict[id]._isRun = false;
            }
            else if (_realTimerDict.ContainsKey(id))
            {

                _realTimerDict[id]._isRun = false;
            }
            else
            {
                Log.Error("找不到计时器：{0}", id);
            }
        }
    }
}