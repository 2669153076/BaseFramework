using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 提供给引用对象的接口
    /// </summary>
    public interface IReference
    {
        /// <summary>
        /// 清理引用
        /// </summary>
        void Clear();
    }
}