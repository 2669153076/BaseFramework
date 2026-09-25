
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
            /// 缓动函数类型。
            /// </summary>
            public enum E_EasingType
            {
                /// <summary>
                /// 无缓动
                /// </summary>
                None,

                #region Quad 二次缓动

                /// <summary>
                /// 二次缓入：开始慢，结束快。
                /// </summary>
                InQuad,

                /// <summary>
                /// 二次缓出：开始快，结束慢。
                /// </summary>
                OutQuad,

                /// <summary>
                /// 二次缓入缓出：开始慢，中间加速，结束减速。
                /// </summary>
                InOutQuad,

                #endregion

                #region Cubic 三次缓动

                /// <summary>
                /// 三次缓入：开始慢，随后快速加速。
                /// </summary>
                InCubic,

                /// <summary>
                /// 三次缓出：开始快速移动，随后逐渐减速。
                /// </summary>
                OutCubic,

                /// <summary>
                /// 三次缓入缓出：开始和结束较慢，中间较快。
                /// </summary>
                InOutCubic,

                #endregion

                #region Quart 四次缓动

                /// <summary>
                /// 四次缓入：开始非常慢，随后快速加速。
                /// </summary>
                InQuart,

                /// <summary>
                /// 四次缓出：开始快速移动，结束时明显减速。
                /// </summary>
                OutQuart,

                /// <summary>
                /// 四次缓入缓出：开始和结束明显减速，中间快速移动。
                /// </summary>
                InOutQuart,

                #endregion

                #region Quint 五次缓动

                /// <summary>
                /// 五次缓入：开始极慢，随后快速加速。
                /// </summary>
                InQuint,

                /// <summary>
                /// 五次缓出：开始快速移动，结束时明显减速。
                /// </summary>
                OutQuint,

                /// <summary>
                /// 五次缓入缓出：开始和结束极慢，中间快速移动。
                /// </summary>
                InOutQuint,

                #endregion

                #region Sine 正弦缓动

                /// <summary>
                /// 正弦缓入：平滑地从慢速开始加速。
                /// </summary>
                InSine,

                /// <summary>
                /// 正弦缓出：平滑地减速至结束。
                /// </summary>
                OutSine,

                /// <summary>
                /// 正弦缓入缓出：整体变化非常平滑、自然。
                /// </summary>
                InOutSine,

                #endregion

                #region Expo 指数缓动

                /// <summary>
                /// 指数缓入：开始极慢，随后呈指数级快速加速。
                /// </summary>
                InExpo,

                /// <summary>
                /// 指数缓出：开始极快，随后快速减速并趋近目标。
                /// </summary>
                OutExpo,

                /// <summary>
                /// 指数缓入缓出：开始和结束极慢，中间快速变化。
                /// </summary>
                InOutExpo,

                #endregion

                #region Circ 圆形缓动

                /// <summary>
                /// 圆形缓入：模拟圆弧运动，开始较慢，随后加速。
                /// </summary>
                InCirc,

                /// <summary>
                /// 圆形缓出：开始较快，随后沿圆弧轨迹减速。
                /// </summary>
                OutCirc,

                /// <summary>
                /// 圆形缓入缓出：结合圆形缓入和缓出，变化平滑。
                /// </summary>
                InOutCirc,

                #endregion

                #region Back 回弹缓动

                /// <summary>
                /// 回弹缓入：开始时会稍微向反方向移动，然后加速向目标移动。
                /// </summary>
                InBack,

                /// <summary>
                /// 回弹缓出：接近目标时会略微超过目标，再回到目标位置。
                /// </summary>
                OutBack,

                /// <summary>
                /// 回弹缓入缓出：开始和结束都会产生轻微的反向/超出效果。
                /// </summary>
                InOutBack,

                #endregion

                #region Elastic 弹性缓动

                /// <summary>
                /// 弹性缓入：开始时带有明显的弹簧/振荡效果，然后向目标移动。
                /// </summary>
                InElastic,

                /// <summary>
                /// 弹性缓出：到达目标附近时产生弹簧式振荡，最后稳定在目标位置。
                /// </summary>
                OutElastic,

                /// <summary>
                /// 弹性缓入缓出：开始和结束均带有弹簧式振荡效果。
                /// </summary>
                InOutElastic,

                #endregion

                #region Bounce 弹跳缓动

                /// <summary>
                /// 弹跳缓入：运动过程中模拟物体弹跳，后期逐渐趋向目标。
                /// </summary>
                InBounce,

                /// <summary>
                /// 弹跳缓出：到达目标时产生多次弹跳，最后停在目标位置。
                /// </summary>
                OutBounce,

                /// <summary>
                /// 弹跳缓入缓出：开始和结束阶段都具有弹跳效果。
                /// </summary>
                InOutBounce,

                #endregion
            }
        }
    }
}