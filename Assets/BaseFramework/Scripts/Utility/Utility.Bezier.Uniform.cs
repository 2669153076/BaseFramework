using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        public partial class Bezier
        {
            /// <summary>
            /// 把"弧长比例 s"换算成贝塞尔参数 t，用于沿曲线匀速移动。
            /// 为什么需要：贝塞尔按参数 t 均匀取值时，曲线弯曲处走速快、平直处走速慢（速度不均匀）。
            /// 本方法先细采样累积每段弦长，再用二分定位目标弧长所在分段并线性插值，
            /// 使 s=0→1 时物体走过的实际路径长度均匀。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="s">弧长比例，取值 [0,1]（0=起点，1=终点），会被 clamp</param>
            /// <param name="steps">采样段数，默认 128；越大匀速精度越高、开销越大</param>
            /// <returns>与弧长比例 s 对应的贝塞尔参数 t</returns>
            public static float UniformToT(Vector3[] points, float s, int steps = 128)
            {
                int n = points.Length;
                if (n < 2)
                    return Mathf.Clamp01(s);
                var work = new Vector3[n];
                float step = 1.0f / steps;
                var lengths = new float[steps + 1];

                Vector3 prev = Point(points, 0f, work);
                for (int i = 1; i <= steps; i++)
                {
                    Vector3 cur = Point(points, i * step, work);
                    lengths[i] = lengths[i - 1] + Vector3.Distance(prev, cur);
                    prev = cur;
                }

                float total = lengths[steps];
                if (total <= 1e-6f)
                    return Mathf.Clamp01(s);
                float target = Mathf.Clamp01(s) * total;

                int lo = 0, hi = steps; // 二分定位分段
                while (lo < hi)
                {
                    int mid = (lo + hi + 1) >> 1;
                    if (lengths[mid] < target)
                        lo = mid;
                    else
                        hi = mid - 1;
                }
                float segLen = lengths[lo + 1] - lengths[lo];
                float frac = segLen > 1e-6f ? (target - lengths[lo]) / segLen : 0f;
                return Mathf.Lerp(lo * step, (lo + 1) * step, Mathf.Clamp01(frac));
            }

            /// <summary>
            /// 把"弧长比例 s"换算成贝塞尔参数 t（2D 版本），用于沿曲线匀速移动。
            /// 原理同 Vector3 版本：弦长累积表 + 二分定位 + 线性插值。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="s">弧长比例，取值 [0,1]，会被 clamp</param>
            /// <param name="steps">采样段数，默认 128</param>
            /// <returns>与弧长比例 s 对应的贝塞尔参数 t</returns>
            public static float UniformToT(Vector2[] points, float s, int steps = 128)
            {
                int n = points.Length;
                if (n < 2)
                    return Mathf.Clamp01(s);
                var work = new Vector2[n];
                float step = 1.0f / steps;
                var lengths = new float[steps + 1];

                Vector2 prev = Point(points, 0f, work);
                for (int i = 1; i <= steps; i++)
                {
                    Vector2 cur = Point(points, i * step, work);
                    lengths[i] = lengths[i - 1] + Vector2.Distance(prev, cur);
                    prev = cur;
                }

                float total = lengths[steps];
                if (total <= 1e-6f)
                    return Mathf.Clamp01(s);
                float target = Mathf.Clamp01(s) * total;

                int lo = 0, hi = steps;
                while (lo < hi)
                {
                    int mid = (lo + hi + 1) >> 1;
                    if (lengths[mid] < target)
                        lo = mid;
                    else
                        hi = mid - 1;
                }
                float segLen = lengths[lo + 1] - lengths[lo];
                float frac = segLen > 1e-6f ? (target - lengths[lo]) / segLen : 0f;
                return Mathf.Lerp(lo * step, (lo + 1) * step, Mathf.Clamp01(frac));
            }


            /// <summary>
            /// 按弧长比例匀速取曲线上的点（快捷组合方法）。
            /// 等价于：Point(points, UniformToT(points, s, steps))，即先换算出 t 再求点。
            /// 适合"沿曲线匀速移动"场景，如路径动画、子弹弧线飞行。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="s">弧长比例，取值 [0,1]（0=起点，1=终点）</param>
            /// <param name="steps">采样段数，默认 128</param>
            /// <returns>弧长比例 s 对应的曲线坐标</returns>
            public static Vector3 UniformPoint(Vector3[] points, float s, int steps = 128)
                => Point(points, UniformToT(points, s, steps));

            /// <summary>
            /// 按弧长比例匀速取曲线上的点（2D 版本）。
            /// 等价于：Point(points, UniformToT(points, s, steps))。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="s">弧长比例，取值 [0,1]（0=起点，1=终点）</param>
            /// <param name="steps">采样段数，默认 128</param>
            /// <returns>弧长比例 s 对应的曲线坐标</returns>
            public static Vector2 UniformPoint(Vector2[] points, float s, int steps = 128)
                => Point(points, UniformToT(points, s, steps));

        }
    }
}