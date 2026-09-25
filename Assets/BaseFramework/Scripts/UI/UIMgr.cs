
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    public class UIMgr : SingletonMono<UIMgr>
    {

        private Dictionary<string, PanelBase> _panelDict = new();
        private UIRoot _uiRoot;


        private UIMgr()
        {
            _uiRoot = GameObject.Instantiate(ResMgr.GetInstance().Load<GameObject>("UI/UIRoot")).GetComponent<UIRoot>();
            DontDestroyOnLoad(_uiRoot.gameObject);
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelPath">路径</param>
        /// <param name="isAsync">是否异步加载</param>
        /// <param name="panelType">类型</param>
        /// <param name="panelLayer">层级</param>
        /// <param name="callback">委托函数</param>
        public void ShowPanel<T>(string panelPath,bool isAsync = false,EPanelType panelType = EPanelType.Static,EPanelLayer panelLayer=EPanelLayer.Bottom,Action<T> callback = null) where T : PanelBase
        {
            string panelName = Utility.String.SubResourceName(panelPath)+typeof(T).FullName;
            Transform father = _uiRoot.GetLayer(panelType, panelLayer);
            if (!_panelDict.ContainsKey(panelName))
            {
                T panel = null;
                if(isAsync){
                    AssetBundleMgr.GetInstance().LoadAsync<GameObject>("UI", panelName, (newpanel) =>
                    {
                        GameObject panelObj = GameObject.Instantiate(newpanel, father, false);

                        panel = panelObj.GetComponent<T>();
                        if (panel == null)
                        {
                            //TODO:打印错误日志
                            Debug.LogError($"无法获取脚本{panelPath}");
                        }

                        _panelDict.Add(panelName, panel);

                        panel.OnInit();
                        panel.OnOpen();
                        callback?.Invoke(panel);

                    });
                }
                else
                {
                    GameObject panelObj = GameObject.Instantiate(AssetBundleMgr.GetInstance().Load<GameObject>("UI", panelName),father,false);
                    panel = panelObj.GetComponent<T>();
                    if (panel == null)
                    {
                        //TODO:打印错误日志
                        Debug.LogError($"无法获取脚本{panelPath}");
                    }
                    _panelDict.Add(panelName, panel);
                    
                    panel.OnInit();
                    panel.OnOpen();
                    callback?.Invoke(panel);
                }
            }
            else
            {
                _panelDict[panelName].OnReveal();
                callback?.Invoke(_panelDict[panelName] as T);
            }
        }


        public void HidePanel<T>(string panelName) where T : PanelBase
        {
            panelName += typeof(T).FullName;
            if (_panelDict.TryGetValue(panelName, out PanelBase panel))
            {
                (panel as T).OnClose();
            }

        }
        public void DestroyPanel<T>(string panelName) where T : PanelBase
        {
            panelName += typeof(T).FullName;
            if (_panelDict.TryGetValue(panelName, out PanelBase panel))
            {
                (panel as T).OnRecycle();
                GameObject.Destroy(panel.gameObject);
                _panelDict.Remove(panelName);
            }
        }

        public T GetPanel<T>(string panelName) where T : PanelBase 
        {
            panelName += typeof(T).FullName;
            if (_panelDict.TryGetValue(panelName, out PanelBase panel))
            {
                return panel as T;
            }

            return null;
        }

    }
}