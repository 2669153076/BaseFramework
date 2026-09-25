using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    [Serializable]
    public class LocalizationTable
    {
        public List<LocalizationEntry> entries = new();
    }
}