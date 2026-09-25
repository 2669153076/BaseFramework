
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseFramework.Runtime
{
    public partial class InputMgr : SingletonNormal<InputMgr>
    {
        public class InputInfo
        {
            public enum E_InputType
            {
                Key,Mouse
            }
            public enum E_InputMethod
            {
                Down,Up,Always
            }

            public E_InputType inputType;
            public E_InputMethod inputMethod;
            public KeyCode keyCode;
            public int mouseId;

            public InputInfo(E_InputMethod method,  KeyCode keyCode)
            {
                this.inputType = E_InputType.Key;
                this.inputMethod = method;
                this.keyCode = keyCode;
            }
            public InputInfo(E_InputMethod method, int mouseId)
            {
                this.inputType = E_InputType.Mouse;
                this.inputMethod = method;
                this.mouseId = mouseId;
            }
        }
    }
}