using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>贝塞尔曲线工具：点坐标部分</summary>
        public partial class Bezier
        {
            /// <summary>
            /// 二次贝塞尔曲线上的点。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos">控制点 P1（决定曲线弯曲方向与程度）</param>
            /// <param name="endPos">终点 P2</param>
            /// <param name="t">曲线参数，取值 [0,1] 为曲线内部；超出范围则沿曲线方向外插</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector3 QuadraticPoint(Vector3 startPos, Vector3 controlPos, Vector3 endPos, float t)
            {
                float u = 1.0f - t;
                return u * u * startPos + 2.0f * u * t * controlPos + t * t * endPos;
            }

            /// <summary>
            /// 二次贝塞尔曲线上的点（2D 版本）。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos">控制点 P1</param>
            /// <param name="endPos">终点 P2</param>
            /// <param name="t">曲线参数，取值 [0,1] 为曲线内部；超出范围则外插</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector2 QuadraticPoint(Vector2 startPos, Vector2 controlPos, Vector2 endPos, float t)
            {
                float u = 1.0f - t;
                return u * u * startPos + 2.0f * u * t * controlPos + t * t * endPos;
            }

            /// <summary>
            /// 三次贝塞尔曲线上的点。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos01">控制点 P1（影响起点端切线方向）</param>
            /// <param name="controlPos02">控制点 P2（影响终点端切线方向）</param>
            /// <param name="endPos">终点 P3</param>
            /// <param name="t">曲线参数，取值 [0,1] 为曲线内部；超出范围则外插</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector3 CubicPoint(Vector3 startPos, Vector3 controlPos01, Vector3 controlPos02, Vector3 endPos, float t)
            {
                float u = 1.0f - t;
                float uu = u * u;
                float tt = t * t;
                return uu * u * startPos + 3.0f * uu * t * controlPos01 + 3.0f * u * tt * controlPos02 + tt * t * endPos;
            }

            /// <summary>
            /// 三次贝塞尔曲线上的点（2D 版本）。
            /// </summary>
            /// <param name="startPos">起点 P0</param>
            /// <param name="controlPos01">控制点 P1</param>
            /// <param name="controlPos02">控制点 P2</param>
            /// <param name="endPos">终点 P3</param>
            /// <param name="t">曲线参数，取值 [0,1] 为曲线内部；超出范围则外插</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector2 CubicPoint(Vector2 startPos, Vector2 controlPos01, Vector2 controlPos02, Vector2 endPos, float t)
            {
                float u = 1.0f - t;
                float uu = u * u;
                float tt = t * t;
                return uu * u * startPos + 3.0f * uu * t * controlPos01 + 3.0f * u * tt * controlPos02 + tt * t * endPos;
            }


            /// <summary>
            /// 任意阶贝塞尔曲线上的点（de Casteljau 迭代算法）。
            /// 控制点数组长度 = 阶数 + 1，即：
            ///   2 个点 = 一次（直线）、3 个点 = 二次、4 个点 = 三次，以此类推。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="t">曲线参数</param>
            /// <param name="work">工作缓冲数组（长度 >= points.Length）。传入可复用数组避免每次调用 new 产生 GC；传 null 或留空则内部自动分配</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector3 Point(Vector3[] points, float t, Vector3[] work = null)
            {
                int n = points.Length;
                if (n == 0)
                    return Vector3.zero;
                if (n == 1)
                    return points[0];
                if (work == null || work.Length < n)
                    work = new Vector3[n];
                for (int i = 0; i < n; i++)
                    work[i] = points[i];

                float u = 1.0f - t;
                for (int level = n - 1; level > 0; level--)
                    for (int i = 0; i < level; i++)
                        work[i] = work[i] * u + work[i + 1] * t;
                return work[0];
            }

            /// <summary>
            /// 任意阶贝塞尔曲线上的点（2D 版本）。
            /// </summary>
            /// <param name="points">控制点数组（起点 → 控制点… → 终点）</param>
            /// <param name="t">曲线参数</param>
            /// <param name="work">工作缓冲数组（长度 >= points.Length），复用防 GC；传 null 则内部自动分配</param>
            /// <returns>参数 t 对应的曲线坐标</returns>
            public static Vector2 Point(Vector2[] points, float t, Vector2[] work = null)
            {
                int n = points.Length;
                if (n == 0)
                    return Vector2.zero;
                if (n == 1)
                    return points[0];
                if (work == null || work.Length < n)
                    work = new Vector2[n];
                for (int i = 0; i < n; i++)
                    work[i] = points[i];

                float u = 1.0f - t;
                for (int level = n - 1; level > 0; level--)
                    for (int i = 0; i < level; i++)
                        work[i] = work[i] * u + work[i + 1] * t;
                return work[0];
            }

            /// <summary>
            /// 把一条三次贝塞尔曲线在参数 t 处切成两条首尾相接的三次曲线（左段 + 右段）。
            /// 原理：de Casteljau 细分——逐层取中点得到 6 个细分点，
            /// 左段控制点为 (P0, P01, P012, P0123)，右段为 (P0123, P123, P23, P3)。
            /// 两段拼回原曲线，用于分段渲染、递归求交、局部采样等。
            /// 使用 LerpUnclamped 保证 t 超出 [0,1] 时细分结果仍正确。
            /// </summary>
            /// <param name="startPos">原曲线起点 P0</param>
            /// <param name="controlPos01">原曲线控制点 P1</param>
            /// <param name="controlPos02">原曲线控制点 P2</param>
            /// <param name="endPos">原曲线终点 P3</param>
            /// <param name="t">细分位置，取值 [0,1]</param>
            /// <param name="l0">左段起点</param>
            /// <param name="l1">左段控制点 1</param>
            /// <param name="l2">左段控制点 2</param>
            /// <param name="l3">左段终点（= 右段起点 = 原曲线在 t 处的点）</param>
            /// <param name="r0">右段起点</param>
            /// <param name="r1">右段控制点 1</param>
            /// <param name="r2">右段控制点 2</param>
            /// <param name="r3">右段终点</param>
            public static void SplitCubic(Vector3 startPos, Vector3 controlPos01, Vector3 controlPos02, Vector3 endPos, float t,
                out Vector3 l0, out Vector3 l1, out Vector3 l2, out Vector3 l3,
                out Vector3 r0, out Vector3 r1, out Vector3 r2, out Vector3 r3)
            {
                //左段
                Vector3 p01 = Vector3.LerpUnclamped(startPos, controlPos01, t);
                Vector3 p12 = Vector3.LerpUnclamped(controlPos01, controlPos02, t);
                Vector3 p23 = Vector3.LerpUnclamped(controlPos02, endPos, t);

                //右段
                Vector3 p012 = Vector3.LerpUnclamped(p01, p12, t);
                Vector3 p123 = Vector3.LerpUnclamped(p12, p23, t);
                Vector3 p0123 = Vector3.LerpUnclamped(p012, p123, t);

                //左段
                l0 = startPos;
                l1 = p01;
                l2 = p012;
                l3 = p0123;

                //右段
                r0 = p0123;
                r1 = p123;
                r2 = p23;
                r3 = endPos;
            }

        }
    }
}