using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>贝塞尔曲线工具：弧长部分</summary>
        public partial class Bezier
        {
            /// <summary>
            /// 计算曲线总长度。
            /// 原理：对速度范数 |B'(t)| 在 [0,1] 上做 Simpson 数值积分（加权 4/2/4/2… 的抛物线近似），
            /// 相比等分段弦长累加，同采样数下精度高一个量级。
            /// steps 越大越精确，代价是更多求导运算。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="steps">采样段数，需为偶数，默认 32；越大精度越高、开销越大</param>
            /// <returns>曲线总长度（>= 0）</returns>
            public static float ArcLength(Vector3[] points, int steps = 32)
            {
                int n = points.Length;
                if (n < 2)
                    return 0f;
                if (steps < 2)
                    steps = 2;
                var work = new Vector3[n];

                float h = 1.0f / steps;
                float sum = Tangent(points, 0f, work).magnitude + Tangent(points, 1f, work).magnitude;
                for (int i = 1; i < steps; i++)
                {
                    float w = (i % 2 == 0) ? 2f : 4f; // Simpson 权重
                    sum += w * Tangent(points, i * h, work).magnitude;
                }
                return sum * h / 3f;
            }

            /// <summary>
            /// 计算曲线总长度（2D 版本）。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="steps">采样段数，需为偶数，默认 32</param>
            /// <returns>曲线总长度（>= 0）</returns>
            public static float ArcLength(Vector2[] points, int steps = 32)
            {
                int n = points.Length;
                if (n < 2)
                    return 0f;
                if (steps < 2)
                    steps = 2;
                var work = new Vector2[n];

                float h = 1.0f / steps;
                float sum = Tangent(points, 0f, work).magnitude + Tangent(points, 1f, work).magnitude;
                for (int i = 1; i < steps; i++)
                {
                    float w = (i % 2 == 0) ? 2f : 4f;
                    sum += w * Tangent(points, i * h, work).magnitude;
                }
                return sum * h / 3f;
            }


        }
    }
}