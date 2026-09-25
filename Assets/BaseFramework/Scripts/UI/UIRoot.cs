using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    public class UIRoot : MonoBehaviour
    {
        public Camera _uiCamera;
        public Canvas _canvasStatic;
        public Canvas _canvasDynamic;

        public Transform _bottomStatic;
        public Transform _middleStatic;
        public Transform _topStatic;
        public Transform _systemStatic;

        public Transform _bottomDynamic;
        public Transform _middleDynamic;
        public Transform _topDynamic;
        public Transform _systemDynamic;

        public Transform GetLayer(EPanelType panelType,EPanelLayer layer)
        {
            if(panelType == EPanelType.Static)
            {
                return layer switch
                {
                    EPanelLayer.Bottom => _bottomStatic,
                    EPanelLayer.Middle => _middleStatic,
                    EPanelLayer.Top => _topStatic,
                    EPanelLayer.System => _systemStatic,
                    _=>null
                };
            }
            else 
            {
                return layer switch
                {
                    EPanelLayer.Bottom => _bottomDynamic,
                    EPanelLayer.Middle => _middleDynamic,
                    EPanelLayer.Top => _topDynamic,
                    EPanelLayer.System => _systemStatic,
                    _ => null
                };
            }
        }
    }
}