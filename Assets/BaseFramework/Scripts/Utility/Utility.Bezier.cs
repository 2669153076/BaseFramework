
using System;
using System.Collections;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 贝塞尔曲线工具 · 曲线动画部分
        /// </summary>
        public partial class Bezier
        {
            private Coroutine _routine;
            private static readonly MonoMgr Mono = MonoMgr.GetInstance(); // 缓存单例

            /// <summary>二次贝塞尔：目标、起点、终点、一个控制点、时长</summary>
            public void Play(Transform target, Vector3 start, Vector3 end, Vector3 control, float duration, Action onComplete = null)
                => StartPath(target, new[] { start, control, end }, duration, onComplete);

            /// <summary>三次贝塞尔：目标、起点、终点、两个控制点、时长</summary>
            public void Play(Transform target, Vector3 start, Vector3 end, Vector3 c1, Vector3 c2, float duration, Action onComplete = null)
                => StartPath(target, new[] { start, c1, c2, end }, duration, onComplete);

            /// <summary>立即停止当前动画</summary>
            public void Stop()
            {
                if (_routine != null)
                {
                    Mono.StopCoroutine(_routine); // 关键修复：真正停止，防止并发协程竞争
                    _routine = null;
                }
            }

            /// <summary>开始动画</summary>
            private void StartPath(Transform target, Vector3[] points, float duration, Action onComplete)
            {
                Stop(); // 先停旧的，保证同时只有一个协程在跑
                _routine = Mono.StartCoroutine(MoveRoutine(target, points, duration, onComplete));
            }

            private static IEnumerator MoveRoutine(Transform target, Vector3[] points, float duration, Action onComplete)
            {
                if (duration <= 0f) // 防除零：时长非法直接落到终点
                {
                    target.position = points[points.Length - 1];
                    onComplete?.Invoke();
                    yield break;
                }

                var work = new Vector3[points.Length];
                float elapsed = 0f;
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    float s = Mathf.Clamp01(elapsed / duration);      // 弧长比例
                    float t = UniformToT(points, s);                  // 匀速参数化
                    target.position = Point(points, t, work);
                    yield return null;
                }
                target.position = points[points.Length - 1]; // 收尾精确落到终点
                onComplete?.Invoke();
            }

            #region 静态版本：无需挂组件，配合 StartCoroutine 一行调用
            public static IEnumerator Move(Transform target, Vector3 start, Vector3 end, Vector3 control,
                float duration, Action onComplete = null)
                => MoveRoutine(target, new[] { start, control, end }, duration, onComplete);

            public static IEnumerator Move(Transform target, Vector3 start, Vector3 end, Vector3 c1, Vector3 c2,
                float duration, Action onComplete = null)
                => MoveRoutine(target, new[] { start, c1, c2, end }, duration, onComplete);
            #endregion
        }
    }
}