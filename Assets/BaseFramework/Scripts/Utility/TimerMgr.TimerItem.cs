using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// 计时器管理器
    /// </summary>
    public partial class TimerMgr : SingletonNormal<TimerMgr>
    {
        private class TimerItem : IReference
        {
            public int _id;
            /// <summary>
            /// 计时结束后的委托回调
            /// </summary>
            public Action _callback;
            /// <summary>
            /// 在一次计时中需要间隔的委托回调
            /// </summary>
            public Action _intervalCallback;
            /// <summary>
            /// 计时器时间
            /// </summary>
            public ulong _totalTime;
            /// <summary>
            /// 需要计时的总时间
            /// </summary>
            public ulong _initTotalTime;
            /// <summary>
            /// 在一次计时中需要间隔运行的时间
            /// </summary>
            public ulong _intervalTime;
            /// <summary>
            /// 默认在一次计时中需要间隔运行的时间
            /// </summary>
            public ulong _initIntervalTime;

            public bool _isRun;   //是否进行计时

            public TimerItem()
            {

            }
            /// <summary>
            /// 初始化计时器数据
            /// </summary>
            /// <param name="id"></param>
            /// <param name="totalTime">计时器总计时时间</param>
            /// <param name="callback">计时回调</param>
            /// <param name="intervalTime">在一次计时中需要间隔运行的时间</param>
            /// <param name="intervalCallback">在一次计时中需要间隔的委托回调</param>
            public void InitInfo(int id, uint totalTime, Action callback, uint intervalTime = 0, Action intervalCallback = null)
            {
                this._id = id;
                this._initTotalTime = this._totalTime = totalTime;
                this._callback = callback;
                this._initIntervalTime = this._intervalTime = intervalTime;
                this._intervalCallback = intervalCallback;
            }
            public void InitInfo(int id, ulong totalTime, Action callback, ulong intervalTime = 0, Action intervalCallback = null)
            {
                this._id = id;
                this._initTotalTime = this._totalTime = totalTime;
                this._callback = callback;
                this._initIntervalTime = this._intervalTime = intervalTime;
                this._intervalCallback = intervalCallback;
            }

            /// <summary>
            /// 重置计时器
            /// </summary>
            public void ResetTimer()
            {
                this._totalTime = this._initTotalTime;
                this._intervalTime = this._initIntervalTime;
            }

            /// <summary>
            /// 清理
            /// </summary>
            public void Clear()
            {
                this._callback = null;
                this._intervalCallback = null;
            }
        }
    }
}