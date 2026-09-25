
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public sealed partial class JsonFileMgr : SingletonNormal<JsonFileMgr>
    {
        public enum EJsonType{
            JsonUtility,
            LitJson

        }
    }
}