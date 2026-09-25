using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 忽略图片透明区域的Image
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/TranslateImage")]
    public class TranslateImage : Image
    {
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            Sprite sprite = overrideSprite;
            if (sprite == null || !sprite.texture.isReadable)
            {
                return false;   
            }


            //将点击后的屏幕左边转换为本地坐标
           if( RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, screenPoint,eventCamera,out Vector2 localPoint))
            {
                return false;
            }

            Rect rect = this.rectTransform.rect;
            //判断是否在RectTransform内
            if (rect.Contains(localPoint))
            {
                return false;
            }

            float u = Mathf.InverseLerp(rect.xMin, rect.xMax, localPoint.x);
            float v = Mathf.InverseLerp(rect.yMin, rect.yMax, localPoint.y);

            if (u < 0f || u > 1f || v < 0f || v > 1f)
            {
                return false;
            }

            //获取点击位置的像素颜色
            float x = sprite.textureRect.xMin / sprite.texture.width + u * sprite.textureRect.width / sprite.texture.width;
            float y = sprite.textureRect.yMin / sprite.texture.height + v * sprite.textureRect.height / sprite.texture.height;

            Color pixelColor = sprite.texture.GetPixelBilinear(x, y);

            return pixelColor.a > 0;    
        }
    }
}