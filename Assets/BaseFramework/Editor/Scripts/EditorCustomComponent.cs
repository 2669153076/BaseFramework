using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BaseFramework.Runtime;

namespace BaseFramework.Editor{
    public class EditorCustomComponent
    {
        [MenuItem("GameObject/UI/TranslateImage")]
        public static void TranslateImage()
        {
            var canvas = GameObject.FindAnyObjectByType<Canvas>();

            if (canvas == null)
            {
                canvas = new GameObject("Canvas").AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.gameObject.AddComponent<CanvasScaler>();
                canvas.gameObject.AddComponent<GraphicRaycaster>();


                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();

            }

            GameObject go = new GameObject("TranslateImage");
            go.AddComponent<TranslateImage>();
            go.transform.SetParent(canvas.transform, false);
        }
    }
}