using BaseFramework.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 新手引导遮罩
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class FocusMask : Graphic, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        /// <summary>
        /// 要显示的目标
        /// </summary>
        public RectTransform Target;
        /// <summary>
        /// 扩展
        /// </summary>
        public RectOffset FocusAreaPadding;
        /// <summary>
        /// 缓动类型
        /// </summary>

        public Utility.Easing.E_EasingType EasingType = Utility.Easing.E_EasingType.InOutQuad;
        /// <summary>
        /// 动画曲线，横轴时间，纵轴进<br/>
        /// 仅当 EasingType 为 None 时生效
        /// </summary>
        public AnimationCurve FocusAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        /// <summary>
        /// 按下事件
        /// </summary>
        public event Action TargetPointerDown;
        /// <summary>
        /// 抬起事件
        /// </summary>
        public event Action TargetPointerUp;
        /// <summary>
        /// 点击事件
        /// </summary>
        public event Action TargetPointerClick;

        private Vector2 _innerMin = Vector2.zero;   //空心区域左下角
        private Vector2 _innerMax = Vector2.zero;   //空心区域右上角
        private Vector2 _outerMin = Vector2.zero;   //背景区域左下角
        private Vector2 _outerMax = Vector2.zero;   //背景区域右上角
        private float _outerFillProgress = 1f;  //聚焦动画当前进度
        private Coroutine _focusCoroutine;  //聚焦动画协程


        private void Update()
        {
            CalculateBounds();
            SetAllDirty();
        }

        /// <summary>
        /// 计算边界
        /// </summary>
        private void CalculateBounds()
        {
            if (Target == null)
            {
                return;
            }

            _outerMin = rectTransform.rect.min;
            _outerMax = rectTransform.rect.max;

            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform, Target);
            _innerMin = bounds.min;
            _innerMax = bounds.max;

            //扩展镂空区域
            _innerMin -= new Vector2(FocusAreaPadding.left, FocusAreaPadding.bottom);
            _innerMax += new Vector2(FocusAreaPadding.right, FocusAreaPadding.top);

            //聚焦动画
            _innerMin = Vector2.Lerp(_outerMin, _innerMin, _outerFillProgress);
            _innerMax = Vector2.Lerp(_outerMax, _innerMax, _outerFillProgress);
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            //添加顶点，用于绘制图形
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;
            //左下角
            vertex.position = new Vector3(_outerMin.x, _outerMin.y);
            vh.AddVert(vertex);
            //左上角
            vertex.position = new Vector3(_outerMin.x, _outerMax.y);
            vh.AddVert(vertex);
            //右上角
            vertex.position = new Vector3(_outerMax.x, _outerMax.y);
            vh.AddVert(vertex);
            //右下角
            vertex.position = new Vector3(_outerMax.x, _outerMin.y);
            vh.AddVert(vertex);

            //左下角
            vertex.position = new Vector3(_innerMin.x, _innerMin.y);
            vh.AddVert(vertex);
            //左上角
            vertex.position = new Vector3(_innerMin.x, _innerMax.y);
            vh.AddVert(vertex);
            //右上角
            vertex.position = new Vector3(_innerMax.x, _innerMax.y);
            vh.AddVert(vertex);
            //右下角
            vertex.position = new Vector3(_innerMax.x, _innerMin.y);
            vh.AddVert(vertex);

            //绘制三角形
            vh.AddTriangle(0, 1, 4);
            vh.AddTriangle(1, 4, 5);
            vh.AddTriangle(1, 5, 2);
            vh.AddTriangle(2, 5, 6);
            vh.AddTriangle(2, 6, 3);
            vh.AddTriangle(6, 3, 7);
            vh.AddTriangle(4, 7, 3);
            vh.AddTriangle(0, 4, 3);

        }

        /// <summary>
        /// 聚焦动画
        /// </summary>
        /// <param name="duration"></param>
        public void DoFocus(float duration)
        {
            if (_focusCoroutine != null)
            {
                StopCoroutine(_focusCoroutine);
            }

            if (duration <= 0)
            {
                _outerFillProgress = 1;
                return;
            }

            _outerFillProgress = 0.0f;
            _focusCoroutine = StartCoroutine(DoFocusCoroutine(duration));
        }

        private IEnumerator DoFocusCoroutine(float duration)
        {
            float timer = 0f;
            float normalizedTime = 0f;

            if (EasingType == Utility.Easing.E_EasingType.None)
            {

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    normalizedTime = Mathf.Clamp01(timer / duration);
                    _outerFillProgress = FocusAnimationCurve.Evaluate(normalizedTime);
                    yield return null;
                }
            }
            else
            {

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    normalizedTime = Mathf.Clamp01(timer / duration);
                    _outerFillProgress = Utility.Easing.Evaluate(EasingType, normalizedTime);
                    yield return null;
                }
            }

            _outerFillProgress = 1f;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
        }
        public void OnPointerClick(PointerEventData eventData)
        {

            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }

        private void PassEvent<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> fun) where T : IEventSystemHandler
        {
            if (Target == null)
            {
                return;
            }

            //TODO:引用池
            //捕获射线
            List<RaycastResult> results = ListPool<RaycastResult>.Get();
            EventSystem.current.RaycastAll(eventData, results);

            //判断是否点击了目标物体，如果点击了，就执行目标物体身上的事件
            foreach (var item in results)
            {
                Transform current = item.gameObject.transform;
                //判断当前 Raycast 命中的 UI 物体，是不是 Target 或者 Target 的子物体。
                if (current != Target && current.IsChildOf(Target) == false)
                {
                    continue;
                }

                //从这个 物体 开始，沿着父物体层级往上找，找到能够处理这个事件的组件，然后执行它
                ExecuteEvents.ExecuteHierarchy(item.gameObject, eventData, fun);

                Type type = typeof(T);
                if (type == typeof(IPointerDownHandler))
                {
                    TargetPointerDown?.Invoke();
                }
                else if (type == typeof(IPointerUpHandler))
                {
                    TargetPointerUp?.Invoke();
                }
                else if (type == typeof(IPointerClickHandler))
                {
                    TargetPointerClick?.Invoke();
                }
                break;
            }
            ListPool<RaycastResult>.Release(results);
        }
    }
}