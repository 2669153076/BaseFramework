using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 贝塞尔曲线工具：切线部分
        /// <br>一阶导，可做速度/朝向</br>
        /// </summary>
        public partial class Bezier
        {
            /// <summary>
            /// 二次贝塞尔曲线的切线（一阶导数），即运动速度方向向量。
            /// <br>用于获取物体沿曲线运动的朝向：Quaternion.LookRotation(tangent.normalized)。</br>
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos">控制点 P1</param>
            /// <param name="endPos">终点 P2</param>
            /// <param name="t">曲线参数</param>
            /// <returns>t 处的速度方向向量（未归一化，长度表示速度大小）</returns>
            public static Vector3 QuadraticTangent(Vector3 startPos, Vector3 controlPos, Vector3 endPos, float t)
            {
                float u = 1.0f - t;
                return 2.0f * u * (controlPos - startPos) + 2.0f * t * (endPos - controlPos);
            }

            /// <summary>
            /// 二次贝塞尔曲线的切线（一阶导数，2D 版本）。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos">控制点 P1</param>
            /// <param name="endPos">终点 P2</param>
            /// <param name="t">曲线参数</param>
            /// <returns>t 处的速度方向向量（未归一化）</returns>
            public static Vector2 QuadraticTangent(Vector2 startPos, Vector2 controlPos, Vector2 endPos, float t)
            {
                float u = 1.0f - t;
                return 2.0f * u * (controlPos - startPos) + 2.0f * t * (endPos - controlPos);
            }

            /// <summary>
            /// 三次贝塞尔曲线的切线（一阶导数），即运动速度方向向量。
            /// <br>用于获取物体沿曲线运动的朝向：Quaternion.LookRotation(tangent.normalized)。</br>
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos01">控制点 P1</param>
            /// <param name="controlPos02">控制点 P2</param>
            /// <param name="endPos">终点 P3</param>
            /// <param name="t">曲线参数</param>
            /// <returns>t 处的速度方向向量（未归一化，长度表示速度大小）</returns>
            public static Vector3 CubicTangent(Vector3 startPos, Vector3 controlPos01, Vector3 controlPos02, Vector3 endPos, float t)
            {
                float u = 1.0f - t;
                return 3.0f * u * u * (controlPos01 - startPos)
                     + 6.0f * u * t * (controlPos02 - controlPos01)
                     + 3.0f * t * t * (endPos - controlPos02);
            }

            /// <summary>
            /// 三次贝塞尔曲线的切线（一阶导数，2D 版本）。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos01">控制点 P1</param>
            /// <param name="controlPos02">控制点 P2</param>
            /// <param name="endPos">终点 P3</param>
            /// <param name="t">曲线参数</param>
            /// <returns>t 处的速度方向向量（未归一化）</returns>
            public static Vector2 CubicTangent(Vector2 startPos, Vector2 controlPos01, Vector2 controlPos02, Vector2 endPos, float t)
            {
                float u = 1.0f - t;
                return 3.0f * u * u * (controlPos01 - startPos)
                     + 6.0f * u * t * (controlPos02 - controlPos01)
                     + 3.0f * t * t * (endPos - controlPos02);
            }

            /// <summary>
            /// 任意阶贝塞尔曲线的切线（通用实现）。
            /// 供 ArcLength 等内部使用，避免与公开的公式法切线重复实现。
            /// </summary>
            /// <param name="points">控制点数组</param>
            /// <param name="t">曲线参数</param>
            /// <param name="work">工作缓冲数组（长度 >= points.Length）</param>
            /// <returns>t 处的切线向量（未归一化）</returns>
            private static Vector3 Tangent(Vector3[] points, float t, Vector3[] work)
            {
                int n = points.Length;
                if (n < 2)
                    return Vector3.forward;
                int m = n - 1;
                for (int i = 0; i < m; i++)
                    work[i] = points[i + 1] - points[i]; // 差分

                float u = 1.0f - t;
                for (int level = m - 1; level > 0; level--)
                    for (int i = 0; i < level; i++)
                        work[i] = work[i] * u + work[i + 1] * t;
                return work[0] * m;
            }

            /// <summary>
            /// 任意阶贝塞尔曲线的切线（通用实现，2D 版本）。
            /// </summary>
            /// <param name="points">控制点数组</param>
            /// <param name="t">曲线参数</param>
            /// <param name="work">工作缓冲数组（长度 >= points.Length）</param>
            /// <returns>t 处的切线向量（未归一化）</returns>
            private static Vector2 Tangent(Vector2[] points, float t, Vector2[] work)
            {
                int n = points.Length;
                if (n < 2)
                    return Vector2.up;
                int m = n - 1;
                for (int i = 0; i < m; i++)
                    work[i] = points[i + 1] - points[i];

                float u = 1.0f - t;
                for (int level = m - 1; level > 0; level--)
                    for (int i = 0; i < level; i++)
                        work[i] = work[i] * u + work[i + 1] * t;
                return work[0] * m;
            }
        }
    }
}