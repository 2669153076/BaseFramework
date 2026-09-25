using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 引用池信息
    /// </summary>
    public class ReferencePoolInfo
    {
        private readonly Type _type;
        private readonly int _unUsedReferenceCount; //没有使用的数量
        private readonly int _usingReferenceCount;  //使用中的引用数量
        private readonly int _acquireReferenceCount;    //获取的数量
        private readonly int _releaseReferenceCount;    //归还的数量
        private readonly int _addReferenceCount;    //增加的数量
        private readonly int _removeReferenceCount; //移除的数量

        public ReferencePoolInfo(Type type,int unUsedCound,int usingCount,int acquireCount,int releaseCount,int addCount,int removeCount)
        {
            this._type = type;
            this._unUsedReferenceCount = unUsedCound;
            this._usingReferenceCount = usingCount;
            this._acquireReferenceCount = acquireCount;
            this._releaseReferenceCount = releaseCount;
            this._addReferenceCount = addCount;
            this._removeReferenceCount = removeCount;
        }

        /// <summary>
        /// 获取引用池类型。
        /// </summary>
        public Type Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// 获取未使用引用数量。
        /// </summary>
        public int UnusedReferenceCount
        {
            get
            {
                return _unUsedReferenceCount;
            }
        }

        /// <summary>
        /// 获取正在使用引用数量。
        /// </summary>
        public int UsingReferenceCount
        {
            get
            {
                return _usingReferenceCount;
            }
        }

        /// <summary>
        /// 获取获取引用数量。
        /// </summary>
        public int AcquireReferenceCount
        {
            get
            {
                return _acquireReferenceCount;
            }
        }

        /// <summary>
        /// 获取归还引用数量。
        /// </summary>
        public int ReleaseReferenceCount
        {
            get
            {
                return _releaseReferenceCount;
            }
        }

        /// <summary>
        /// 获取增加引用数量。
        /// </summary>
        public int AddReferenceCount
        {
            get
            {
                return _addReferenceCount;
            }
        }

        /// <summary>
        /// 获取移除引用数量。
        /// </summary>
        public int RemoveReferenceCount
        {
            get
            {
                return _removeReferenceCount;
            }
        }
    }
}