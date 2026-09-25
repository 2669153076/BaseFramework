using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseFramework.Runtime
{
    /// <summary>按钮动画</summary>
    [RequireComponent(typeof(Button))]
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector3 _originScale;
        [SerializeField] private float _targetRatio = 0.9f;  //目标缩放倍率
        [SerializeField] private float _ratioDuration = 0.1f;    //缩放动画时长

        private Coroutine _scaleCoroutine;

        private void Awake()
        {
            _originScale = transform.localScale;
            GetComponent<Button>().transition = Selectable.Transition.None;
        }

        private void OnEnable()
        {
            this.transform.localScale = _originScale;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            this.PlayScale(_originScale);
        }
        public void OnPointerDown(PointerEventData eventData)
        {

            this.PlayScale(_originScale * _targetRatio);
        }

        /// <summary>缩放后又变回原来大小</summary>
        private void PlayScalePingPong()
        {
            if (_scaleCoroutine != null)
            {
                StopCoroutine(_scaleCoroutine);
                _scaleCoroutine = null;
            }
            _scaleCoroutine = StartCoroutine(PingPongCoroutine());
        }

        private IEnumerator PingPongCoroutine()
        {
            //缩小(放大)
            yield return ScaleToCoroutine(_originScale * _targetRatio);

            //返回
            yield return ScaleToCoroutine(_originScale);
        }


        /// <summary>播放单次缩放动画</summary>
        /// <param name="target">目标缩放值</param>
        private void PlayScale(Vector3 target)
        {
            if (_scaleCoroutine != null)
            {
                StopCoroutine(_scaleCoroutine);
                _scaleCoroutine = null;
            }
            _scaleCoroutine = StartCoroutine(ScaleToCoroutine(target));
        }

        private IEnumerator ScaleToCoroutine(Vector3 target)
        {
            Vector3 startScale = transform.localScale;
            float time = 0f;
            float raw = 0f;
            float t = 0f;
            while (time < _ratioDuration)
            {
                time += Time.deltaTime;
                raw = Mathf.Clamp01(time / _ratioDuration);
                //t = Utility.Easing.OutCubic(raw);
                this.transform.localScale = Vector3.Lerp(startScale, target, t);

                yield return null;
            }

            this.transform.localScale = target;
        }
    }
}