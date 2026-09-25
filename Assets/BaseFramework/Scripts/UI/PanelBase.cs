using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseFramework.Runtime
{
    public abstract class PanelBase:MonoBehaviour
    {

        public Dictionary<string,UIBehaviour> _controlDict = new();

        private readonly List<string> _defaultName = new() { "Image", "Text (TMP)", "RawImage", "Background", "Checkmark", "Label", "Text (Legacy)", "Arrow", "Placeholder", "Fill", "Handle", "Viewport", "Scrollbar Horizontal", "Scrollbar Vertical" };

        /// <summary>
        /// 界面初始化
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// 界面打开
        /// </summary>
        public virtual void OnOpen() 
        {
            this.FindChildrenControl<Button>();
            this.FindChildrenControl<Toggle>();
            this.FindChildrenControl<Slider>();
            this.FindChildrenControl<ScrollRect>();
            this.FindChildrenControl<Dropdown>();
            this.FindChildrenControl<InputField>();
            this.FindChildrenControl<Scrollbar>();
            this.FindChildrenControl<Image>();
            this.FindChildrenControl<RawImage>();
            this.FindChildrenControl<Text>();
            this.FindChildrenControl<TextMeshPro>();

        }
        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetControl<T>(string name) where T : UIBehaviour
        {
            if (_controlDict.ContainsKey(name))
            {
                T control = _controlDict[name] as T;
                if(control == null)
                {
                    return control;
                }
                Debug.LogError($"不存在类型为{typeof(T).Name}的组件");
                return null;
            }
            else
            {
                Debug.LogError($"不存在对应名字{name}的组件");
                return null;
            }
        }

        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>(true);
            for (int i = 0; i < controls.Length; i++)
            {
                if (!_controlDict.ContainsKey(controls[i].gameObject.name))
                {
                    if (!_defaultName.Contains(controls[i].gameObject.name))
                    {
                        _controlDict.Add(controls[i].gameObject.name, controls[i]);
                    }
                }
                else
                {
                    Debug.LogError("界面存在同名同类型组件，请处理" + controls[i].gameObject.name);
                }
            }
        }

        /// <summary>
        /// 界面关闭
        /// </summary>
        public virtual void OnClose() { }
        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause() { }
        /// <summary>
        /// 界面暂停恢复
        /// </summary>
        public virtual void OnResume() { }
        /// <summary>
        /// 界面遮挡
        /// </summary>
        public virtual void OnCover() { }
        /// <summary>
        /// 界面遮挡恢复
        /// </summary>
        public virtual void OnReveal() { }
        /// <summary>
        /// 界面激活
        /// </summary>
        public virtual void OnRefocus() { }
        /// <summary>
        /// 界面回收
        /// </summary>
        public virtual void OnRecycle() { }
    }
}