
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public partial class InputMgr : SingletonNormal<InputMgr>
    {

        private Dictionary<EventId, InputInfo> _inputDict = new();
        private InputInfo _curInputInfo;

        private Action<InputInfo> _getInputInfoCallback;    //改键时获取输入信息的委托
        private bool _isCheckInput = false;


        private bool keyboardEnabled;
        private bool mouseEnabled;
        private bool hotKeyEnabled;



        private InputMgr() {
            MonoMgr.GetInstance().AddUpdateListener(InputUpdate);
        }

        public void GetInputInfo(Action<InputInfo> callback)
        {
            _getInputInfoCallback = callback;
            MonoMgr.GetInstance().StartMyCoroutine(BeginCheckInput());
        }

        private IEnumerator BeginCheckInput()
        {
            yield return null;
            _isCheckInput = true;
        }

        private void InputUpdate()
        {

            if (_isCheckInput && Input.anyKeyDown)
            {
                _isCheckInput = false;
                InputInfo inputInfo = null;
                Array keycodes = Enum.GetValues(typeof(KeyCode));
                foreach (KeyCode keyCode in keycodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        inputInfo = new InputInfo(InputInfo.E_InputMethod.Down, keyCode);
                        break;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (Input.GetMouseButtonDown(i))
                    {
                        inputInfo = new InputInfo(InputInfo.E_InputMethod.Down, i);
                        break;
                    }
                }

                _getInputInfoCallback?.Invoke(inputInfo);
                _getInputInfoCallback = null;
            }


            foreach (var key in _inputDict.Keys)
            {
                _curInputInfo = _inputDict[key];
                if(mouseEnabled&& _curInputInfo.inputType == InputInfo.E_InputType.Mouse)
                {
                    switch (_curInputInfo.inputMethod)
                    {
                        case InputInfo.E_InputMethod.Down:
                            if (Input.GetMouseButtonDown(_curInputInfo.mouseId))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                        case InputInfo.E_InputMethod.Up:
                            if (Input.GetMouseButtonUp(_curInputInfo.mouseId))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                        case InputInfo.E_InputMethod.Always:
                            if (Input.GetMouseButton(_curInputInfo.mouseId))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                    }
                }
                if(keyboardEnabled&& _curInputInfo.inputType == InputInfo.E_InputType.Key)
                {
                    switch (_curInputInfo.inputMethod)
                    {
                        case InputInfo.E_InputMethod.Down:
                            if (Input.GetKeyDown(_curInputInfo.keyCode))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                        case InputInfo.E_InputMethod.Up:
                            if (Input.GetKeyUp(_curInputInfo.keyCode))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                        case InputInfo.E_InputMethod.Always:
                            if (Input.GetKey(_curInputInfo.keyCode))
                                EventCenter.GetInstance().EventTrigger(key);
                            break;
                    }
                }
            }

        }

        private void ChangeKeyboardInfo(EventId eventId, KeyCode keyCode,InputInfo.E_InputMethod inputMethod)
        {
            if (!_inputDict.ContainsKey(eventId))
            {
                _inputDict.Add(eventId, new InputInfo(inputMethod, keyCode));
            }
            else
            {
                _inputDict[eventId].inputType = InputInfo.E_InputType.Key;
                _inputDict[eventId].inputMethod = inputMethod;
                _inputDict[eventId].keyCode = keyCode;

            }
        }

        private void ChangeMouseInfo(EventId eventId, int mouseId, InputInfo.E_InputMethod inputMethod)
        {
            if (!_inputDict.ContainsKey(eventId))
            {
                _inputDict.Add(eventId, new InputInfo(inputMethod, mouseId));
            }
            else
            {
                _inputDict[eventId].inputType = InputInfo.E_InputType.Mouse;
                _inputDict[eventId].inputMethod = inputMethod;
                _inputDict[eventId].mouseId = mouseId;

            }
        }

        private void RemoveInputInfo(EventId eventId)
        {
            if (_inputDict.ContainsKey(eventId))
            {
                _inputDict.Remove(eventId);
            }
        }
    }
}