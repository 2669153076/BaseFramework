using UnityEngine;


namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 常用缓动函数工具类。<br/>
        /// 效果预览：https://easings.net/
        /// </summary>
        public static partial class Easing
        {
            /// <summary>
            /// 缓动效果
            /// </summary>
            /// <param name="type"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            public static float Evaluate(E_EasingType type, float t)
            {
                return type switch
                {
                    E_EasingType.InQuad => InQuad(t),
                    E_EasingType.OutQuad => OutQuad(t),
                    E_EasingType.InOutQuad => InOutQuad(t),

                    E_EasingType.InCubic => InCubic(t),
                    E_EasingType.OutCubic => OutCubic(t),
                    E_EasingType.InOutCubic => InOutCubic(t),

                    E_EasingType.InQuart => InQuart(t),
                    E_EasingType.OutQuart => OutQuart(t),
                    E_EasingType.InOutQuart => InOutQuart(t),

                    E_EasingType.InQuint => InQuint(t),
                    E_EasingType.OutQuint => OutQuint(t),
                    E_EasingType.InOutQuint => InOutQuint(t),

                    E_EasingType.InSine => InSine(t),
                    E_EasingType.OutSine => OutSine(t),
                    E_EasingType.InOutSine => InOutSine(t),

                    E_EasingType.InExpo => InExpo(t),
                    E_EasingType.OutExpo => OutExpo(t),
                    E_EasingType.InOutExpo => InOutExpo(t),

                    E_EasingType.InCirc => InCirc(t),
                    E_EasingType.OutCirc => OutCirc(t),
                    E_EasingType.InOutCirc => InOutCirc(t),

                    E_EasingType.InBack => InBack(t),
                    E_EasingType.OutBack => OutBack(t),
                    E_EasingType.InOutBack => InOutBack(t),

                    E_EasingType.InElastic => InElastic(t),
                    E_EasingType.OutElastic => OutElastic(t),
                    E_EasingType.InOutElastic => InOutElastic(t),

                    E_EasingType.InBounce => InBounce(t),
                    E_EasingType.OutBounce => OutBounce(t),
                    E_EasingType.InOutBounce => InOutBounce(t),

                    _ => t
                };
            }

            #region 二次 Quad（轻快）

            /// <summary>
            /// 二次缓入：从慢到快。<br/>
            /// 特点：加速启动明显，适合入场前段"蓄力"。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InQuad(float t) => t * t;

            /// <summary>
            /// 二次缓出：从快到慢。<br/>
            /// 特点：最常用的轻量减速收尾，适合 UI 出入场。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutQuad(float t) => 1f - (1f - t) * (1f - t);

            /// <summary>
            /// 二次缓入缓出：先加速后减速。<br/>
            /// 特点：两端平滑，中间过渡自然。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutQuad(float t) => t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
            #endregion

            #region 三次 Cubic（通用首选）

            /// <summary>
            /// 三次缓入：从慢到快。<br/>
            /// 特点：加速感比二次更强。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InCubic(float t) => t * t * t;

            /// <summary>
            /// 三次缓出：从快到慢。<br/>
            /// 特点：通用首选缓动，收尾柔和，适合按钮、卡片、弹窗。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutCubic(float t) => 1f - (1f - t) * (1f - t) * (1f - t);

            /// <summary>
            /// 三次缓入缓出：先加速后减速。<br/>
            /// 特点：比 InOutQuad 起止感更明显。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutCubic(float t) => t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
            #endregion

            #region 四次 Quart（强力）

            /// <summary>
            /// 四次缓入：从慢到快。<br/>
            /// 特点：加速感很强，适合大位移强调的入场前段。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InQuart(float t) => t * t * t * t;

            /// <summary>
            /// 四次缓出：从快到慢。<br/>
            /// 特点：减速收尾非常柔和顺滑。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutQuart(float t) => 1f - Mathf.Pow(1f - t, 4f);

            /// <summary>
            /// 四次缓入缓出：先加速后减速。<br/>
            /// 特点：起止感强、中间平滑。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutQuart(float t) => t < 0.5f ? 8f * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;
            #endregion

            #region 五次 Quint（极强力）

            /// <summary>
            /// 五次缓入：从慢到快。<br/>
            /// 特点：加速感极强，适合开场大强调。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InQuint(float t) => t * t * t * t * t;

            /// <summary>
            /// 五次缓出：从快到慢。<br/>
            /// 特点：收尾最柔和。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutQuint(float t) => 1f - Mathf.Pow(1f - t, 5f);

            /// <summary>
            /// 五次缓入缓出：先加速后减速。<br/>
            /// 特点：起止感最强，适合大场面开场/收尾强调。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutQuint(float t) => t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
            #endregion

            #region 正弦 Sine（柔和）

            /// <summary>
            /// 正弦缓入：从慢到快。<br/>
            /// 特点：曲线平滑柔和，无突变。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InSine(float t) => 1f - Mathf.Cos(t * Mathf.PI / 2f);

            /// <summary>
            /// 正弦缓出：从快到慢。<br/>
            /// 特点：平滑减速，适合淡入淡出、呼吸循环。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutSine(float t) => Mathf.Sin(t * Mathf.PI / 2f);

            /// <summary>
            /// 正弦缓入缓出：先加速后减速。<br/>
            /// 特点：最平滑的往返过渡。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
            #endregion

            #region 指数 Expo（极快启动/收尾）

            /// <summary>
            /// 指数缓入：从慢到快。<br/>
            /// 特点：启动极慢后急剧加速，适合"蓄力爆发"。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InExpo(float t) => t <= 0f ? 0f : Mathf.Pow(2f, 10f * t - 10f);

            /// <summary>
            /// 指数缓出：从快到慢。<br/>
            /// 特点：开始极快、收尾极慢，适合冲刺后骤停。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutExpo(float t) => t >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);

            /// <summary>
            /// 指数缓入缓出：先加速后减速。<br/>
            /// 特点：两端极缓、中间极快。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutExpo(float t)
            {
                if (t <= 0f)
                    return 0f;
                if (t >= 1f)
                    return 1f;
                return t < 0.5f
                    ? Mathf.Pow(2f, 20f * t - 10f) / 2f
                    : (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
            }
            #endregion

            #region 圆形 Circ（弧线速度）

            /// <summary>
            /// 圆形缓入：公式 1-√(1-t²)。<br/>
            /// 特点：速度按四分之一圆弧变化，过渡圆润。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InCirc(float t) => 1f - Mathf.Sqrt(1f - t * t);

            /// <summary>
            /// 圆形缓出：公式 √(1-(t-1)²)。<br/>
            /// 特点：圆滑减速。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float OutCirc(float t) => Mathf.Sqrt(1f - (t - 1f) * (t - 1f));

            /// <summary>
            /// 圆形缓入缓出：两段四分之一圆弧拼接。<br/>
            /// 特点：整体圆润平滑。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutCirc(float t) => t < 0.5f
                ? (1f - Mathf.Sqrt(1f - 4f * t * t)) / 2f
                : (Mathf.Sqrt(1f - (-2f * t + 2f) * (-2f * t + 2f)) + 1f) / 2f;
            #endregion

            #region 回弹 Back（过冲后回落）

            /// <summary>
            /// 回弹缓入：先反向越过起点再冲入，公式 c3·t³-c1·t²（c1=1.70158）。<br/>
            /// 注意：输出会先低于 0 再回到 [0,1]，带轻微过冲。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（可能短暂 &lt;0 或 &gt;1）</returns>
            private static float InBack(float t) { float c1 = 1.70158f, c3 = c1 + 1f; return c3 * t * t * t - c1 * t * t; }

            /// <summary>
            /// 回弹缓出：先冲过终点再回落到 1，公式 1+c3(t-1)³+c1(t-1)²。<br/>
            /// 常用：弹窗弹出、图标强调的"过冲一下"效果。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（会先 &gt;1 再回落到 1）</returns>
            private static float OutBack(float t) { float c1 = 1.70158f, c3 = c1 + 1f; return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f); }

            /// <summary>
            /// 回弹缓入缓出：两端都有轻微过冲（c2 = c1·1.525 调两侧幅度）。<br/>
            /// 特点：两端都有过冲，比 In/Out 更活跃。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（两端可能短暂越界）</returns>
            private static float InOutBack(float t)
            {
                float c1 = 1.70158f, c2 = c1 * 1.525f;
                return t < 0.5f
                    ? ((2f * t) * (2f * t) * ((c2 + 1f) * 2f * t - c2)) / 2f
                    : (((2f * t - 2f) * (2f * t - 2f) * ((c2 + 1f) * (2f * t - 2f) + c2)) + 2f) / 2f;
            }
            #endregion

            #region 弹性 Elastic（反复震荡）

            /// <summary>
            /// 弹性缓入：从起点来回震荡后冲向终点。<br/>
            /// p 为振荡周期（0.3），越大摆幅越缓；边界已处理为精确 0/1。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（震荡）</returns>
            private static float InElastic(float t)
            {
                if (t <= 0f)
                    return 0f;
                if (t >= 1f)
                    return 1f;
                float p = 0.3f;
                return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t - (1f + p / 4f)) * (2f * Mathf.PI) / p);
            }

            /// <summary>
            /// 弹性缓出：冲向终点后反复震荡回弹，公式 2^(-10t)·sin((t-p/4)·2π/p)+1。<br/>
            /// 趣味性强，适合橡皮筋、强调动画；p 为振荡周期（0.3）。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（震荡）</returns>
            private static float OutElastic(float t) { if (t == 0 || t == 1) return t; float p = 0.3f; return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - p / 4f) * (2f * Mathf.PI) / p) + 1f; }

            /// <summary>
            /// 弹性缓入缓出：两端对称震荡，中间过渡。<br/>
            /// 采用标准 Robert Penner 公式（c5=2π/4.5）。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例（震荡）</returns>
            private static float InOutElastic(float t)
            {
                if (t <= 0f)
                    return 0f;
                if (t >= 1f)
                    return 1f;
                const float c5 = (2f * Mathf.PI) / 4.5f;
                return t < 0.5f
                    ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f
                    : (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f + 1f;
            }
            #endregion

            #region 弹跳 Bounce（落地弹跳）

            /// <summary>
            /// 弹跳缓出：像小球落地，共 4 段抛物线反弹（系数 n1=7.5625, d1=2.75）。<br/>
            /// 适合掉落、小球、菜单落地感。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]（始终不越界）</returns>
            private static float OutBounce(float t)
            {
                const float n1 = 7.5625f, d1 = 2.75f;
                if (t < 1f / d1)
                    return n1 * t * t;
                if (t < 2f / d1)
                { t -= 1.5f / d1; return n1 * t * t + 0.75f; }
                if (t < 2.5f / d1)
                { t -= 2.25f / d1; return n1 * t * t + 0.9375f; }
                t -= 2.625f / d1;
                return n1 * t * t + 0.984375f;
            }

            /// <summary>
            /// 弹跳缓入：从弹跳中逐渐停下来，为 OutBounce 的镜像（1 - OutBounce(1-t)）。<br/>
            /// 适合"下落前先抖"的入场。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InBounce(float t) => 1f - OutBounce(1f - t);

            /// <summary>
            /// 弹跳缓入缓出：前半段反弹进入、后半段弹跳落地，两端对称。<br/>
            /// 特点：进出都有弹跳感。
            /// </summary>
            /// <param name="t">线性进度 [0,1]</param>
            /// <returns>缓动后的比例 [0,1]</returns>
            private static float InOutBounce(float t) => t < 0.5f
                ? (1f - OutBounce(1f - 2f * t)) / 2f
                : (1f + OutBounce(2f * t - 1f)) / 2f;
            #endregion
        }
    }
}