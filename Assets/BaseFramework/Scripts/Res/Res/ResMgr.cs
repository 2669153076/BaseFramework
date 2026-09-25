
using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// Resources资源管理器
    /// </summary>
    public sealed partial class ResMgr : SingletonNormalLocked<ResMgr>
    {
        private readonly Dictionary<string, ResInfoBase> _resDict = new();  //资源信息列表   

        private ResMgr() { }

        public T Load<T>(string path) where T : UnityEngine.Object
        {
            ValidatePath(path);

            string resKey = GetResKey(path, typeof(T));

            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<T> resInfo = _resDict[resKey] as ResInfo<T>;
                switch (resInfo.resState)
                {
                    case EResState.Done:
                        {
                            resInfo.AddRefCount();
                            Action<T> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            return resInfo.asset;
                        }
                    case EResState.Loading:
                    case EResState.PendDel:
                        {
                            if (resInfo.loadCoroutine != null)
                            {
                                MonoMgr.GetInstance().StopMyCoroutine(resInfo.loadCoroutine);
                                resInfo.loadCoroutine = null;
                            }
                            T asset = Resources.Load<T>(path);
                            resInfo.asset = asset;
                            resInfo.AddRefCount();
                            resInfo.resState = EResState.Done;
                            Action<T> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            return resInfo.asset;
                        }
                }
            }
            ResInfo<T> newResInfo = new();
            _resDict.Add(resKey, newResInfo);

            T newAsset = Resources.Load<T>(path);
            newResInfo.asset = newAsset;
            newResInfo.resState = EResState.Done;
            newResInfo.AddRefCount();

            return newAsset;
        }

        [Obsolete("建议使用泛型版本。禁止与泛型版本混用")]
        public UnityEngine.Object Load(string path,Type type)
        {
            ValidatePath(path);

            string resKey = GetResKey(path, type);

            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<UnityEngine.Object> resInfo = _resDict[resKey] as ResInfo<UnityEngine.Object>;
                switch (resInfo.resState)
                {
                    case EResState.Done:
                        {
                            resInfo.AddRefCount();
                            Action<UnityEngine.Object> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            return resInfo.asset;
                        }
                    case EResState.Loading:
                    case EResState.PendDel:
                        {
                            if (resInfo.loadCoroutine != null)
                            {
                                MonoMgr.GetInstance().StopMyCoroutine(resInfo.loadCoroutine);
                                resInfo.loadCoroutine = null;
                            }
                            UnityEngine.Object asset = Resources.Load(path,type);
                            resInfo.asset = asset;
                            resInfo.AddRefCount();
                            resInfo.resState = EResState.Done;
                            Action<UnityEngine.Object> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            return resInfo.asset;
                        }
                }
            }
            ResInfo<UnityEngine.Object> newResInfo = new();
            _resDict.Add(resKey, newResInfo);

            UnityEngine.Object newAsset = Resources.Load(path,type);
            newResInfo.asset = newAsset;
            newResInfo.resState = EResState.Done;
            newResInfo.AddRefCount();

            return newAsset;
        }

        public void LoadAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            ValidatePath(path);
            string resKey = GetResKey(path, typeof(T));

            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<T> resInfo = _resDict[resKey] as ResInfo<T>;
                switch (resInfo.resState)
                {
                    case EResState.Done:
                        resInfo.AddRefCount();
                        callback?.Invoke(resInfo.asset);
                        break;
                    case EResState.Loading:
                        resInfo.AddRefCount();
                        resInfo.onComplete += callback;
                        break;
                    case EResState.PendDel:
                        resInfo.resState = EResState.Loading;
                        resInfo.AddRefCount();
                        resInfo.onComplete += callback;
                        break;
                }
            }
            else
            {
                ResInfo<T> resInfo = new();
                _resDict.Add(resKey, resInfo);

                resInfo.resState = EResState.Loading;
                resInfo.AddRefCount();
                resInfo.onComplete += callback;
                resInfo.loadCoroutine = MonoMgr.GetInstance().StartMyCoroutine(LoadAsyncCoroutine<T>(path));
            }
        }
        private System.Collections.IEnumerator LoadAsyncCoroutine<T>(string path) where T : UnityEngine.Object
        {
            var rq = Resources.LoadAsync<T>(path);
            yield return rq;

            string resKey = GetResKey(path, typeof(T));
            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<T> resInfo = _resDict[resKey] as ResInfo<T>;

                switch (resInfo.resState)
                {
                    case EResState.Loading:
                        {
                            resInfo.asset = rq.asset as T;
                            resInfo.resState = EResState.Done;
                            resInfo.loadCoroutine = null;
                            Action<T> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            break;
                        }
                    case EResState.PendDel:
                        {
                            resInfo.loadCoroutine = null;
                            Resources.UnloadAsset(rq.asset);
                            _resDict.Remove(resKey);
                        }
                        break;
                }
            }
        }
        [Obsolete("建议使用泛型版本。禁止与泛型版本混用")]
        public void LoadAsync(string path,Type type, Action<UnityEngine.Object> callback)
        {
            ValidatePath(path);
            string resKey = GetResKey(path, type);

            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<UnityEngine.Object> resInfo = _resDict[resKey] as ResInfo<UnityEngine.Object>;
                switch (resInfo.resState)
                {
                    case EResState.Done:
                        resInfo.AddRefCount();
                        callback?.Invoke(resInfo.asset);
                        break;
                    case EResState.Loading:
                        resInfo.AddRefCount();
                        resInfo.onComplete += callback;
                        break;
                    case EResState.PendDel:
                        resInfo.resState = EResState.Loading;
                        resInfo.AddRefCount();
                        resInfo.onComplete += callback;
                        break;
                }
            }
            else
            {
                ResInfo<UnityEngine.Object> resInfo = new();
                _resDict.Add(resKey, resInfo);

                resInfo.resState = EResState.Loading;
                resInfo.AddRefCount();
                resInfo.onComplete += callback;
                resInfo.loadCoroutine = MonoMgr.GetInstance().StartMyCoroutine(LoadAsyncCoroutine(path,type));
            }
        }
        private System.Collections.IEnumerator LoadAsyncCoroutine(string path,Type type) 
        {
            var rq = Resources.LoadAsync(path,type);
            yield return rq;

            string resKey = GetResKey(path, type);
            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<UnityEngine.Object> resInfo = _resDict[resKey] as ResInfo<UnityEngine.Object>;

                switch (resInfo.resState)
                {
                    case EResState.Loading:
                        {
                            resInfo.asset = rq.asset;
                            resInfo.resState = EResState.Done;
                            resInfo.loadCoroutine = null;
                            Action<UnityEngine.Object> callback = resInfo.onComplete;
                            resInfo.onComplete = null;
                            callback?.Invoke(resInfo.asset);
                            break;
                        }
                    case EResState.PendDel:
                        {
                            resInfo.loadCoroutine = null;
                            Resources.UnloadAsset(rq.asset);
                            _resDict.Remove(resKey);
                        }
                        break;
                }
            }
        }

        public void UnLoad<T>(string path,Action<T> callback = null) where T : UnityEngine.Object
        {
            string resKey = GetResKey(path, typeof(T));
            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<T> resInfo = _resDict[resKey] as ResInfo<T>;

                switch (resInfo.resState)
                {
                    case EResState.Done:
                        resInfo.SubRefCount();
                        if (resInfo.refCount == 0)
                        {
                            Resources.UnloadAsset(resInfo.asset);
                            _resDict.Remove(resKey);
                        }
                        break;
                    case EResState.Loading:
                        resInfo.SubRefCount();
                        if (callback != null)
                        {
                            resInfo.onComplete -= callback;
                        }
                        if (resInfo.refCount == 0)
                        {
                            resInfo.resState = EResState.PendDel;
                        }
                        break;
                }
            }
            else
            {
                //TODO:打印错误日志
                Debug.LogError($"资源卸载失败,未找到资源:{resKey}");
            }
        }
        [Obsolete("建议使用泛型版本。禁止与泛型版本混用")]
        public void UnLoad(string path,Type type, Action<UnityEngine.Object> callback = null)
        {
            string resKey = GetResKey(path, type);
            if (_resDict.ContainsKey(resKey))
            {
                ResInfo<UnityEngine.Object> resInfo = _resDict[resKey] as ResInfo<UnityEngine.Object>;

                switch (resInfo.resState)
                {
                    case EResState.Done:
                        resInfo.SubRefCount();
                        if (resInfo.refCount == 0)
                        {
                            Resources.UnloadAsset(resInfo.asset);
                            _resDict.Remove(resKey);
                        }
                        break;
                    case EResState.Loading:
                        resInfo.SubRefCount();
                        if (callback != null)
                        {
                            resInfo.onComplete -= callback;
                        }
                        if (resInfo.refCount == 0)
                        {
                            resInfo.resState = EResState.PendDel;
                        }
                        break;
                }
            }
            else
            {
                //TODO:打印错误日志
                Debug.LogError($"资源卸载失败,未找到资源:{resKey}");
            }
        }
        public int GetRefCount<T>(string path) where T : UnityEngine.Object
        {
            string resKey = GetResKey(path, typeof(T));
            if (_resDict.ContainsKey(resKey))
            {
                return (_resDict[resKey] as ResInfo<T>).refCount;
            }
            return 0;
        }
        [Obsolete("建议使用泛型版本。禁止与泛型版本混用")]
        public int GetRefCount(string path,Type type) 
        {
            string resKey = GetResKey(path, type);
            if (_resDict.ContainsKey(resKey))
            {
                return (_resDict[resKey] as ResInfo<UnityEngine.Object>).refCount;
            }
            return 0;
        }

        /// <summary>
        /// 卸载空闲资源
        /// </summary>
        public void UnloadUnusedAssets()
        {
            List<string> resKeyList = new List<string>();
            foreach (var resKey in _resDict.Keys)
            {
                if (_resDict[resKey].refCount == 0)
                {
                    resKeyList.Add(resKey);
                }
            }
            foreach (var resKey in resKeyList)
            {
                _resDict.Remove(resKey);
            }
            Resources.UnloadUnusedAssets();
        }
        /// <summary>
        /// 卸载空闲资源
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void UnloadUnusedAssets(UnityAction callback)
        {
            List<string> resKeyList = new List<string>();
            foreach (var resKey in _resDict.Keys)
            {
                if (_resDict[resKey].refCount == 0)
                {
                    resKeyList.Add(resKey);
                }
            }
            foreach (var resKey in resKeyList)
            {
                _resDict.Remove(resKey);
            }

            var op = Resources.UnloadUnusedAssets();
            op.completed += operation =>
            {
                callback?.Invoke();
            };
        }

        #region 原生API
        /// <summary>
        /// 加载指定路径、指定类型的所有资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">资源路径</param>
        /// <returns>资源数组</returns>
        public T[] LoadAll<T>(string path) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(path);
        }
        /// <summary>
        /// 加载指定路径、指定类型的所有资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type">资源类型</param>
        /// <returns>资源数组</returns>
        [Obsolete("建议使用泛型版本，可获得编译期类型检查")]
        public UnityEngine.Object[] LoadAll(string path, Type type)
        {
            return Resources.LoadAll(path, type);
        }
        /// <summary>
        /// 加载指定路径的所有资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>资源数组</returns>
        public UnityEngine.Object[] LoadAll(string path)
        {
            return Resources.LoadAll(path);
        }
        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="asset">资源引用</param>
        public void UnloadAsset<T>(T asset) where T : UnityEngine.Object
        {
            if (asset == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning("卸载资源失败，传入资源为空");
                return;
            }
            Resources.UnloadAsset(asset);
        }
        
        /// <summary>
        /// 找到当前内存中的、指定类型的所有对象 
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>对象数组</returns>
        public T[] FindObjectsOfTypeAll<T>() where T : UnityEngine.Object
        {
            return Resources.FindObjectsOfTypeAll<T>();
        }
        /// <summary>
        /// 找到当前内存中的、指定类型的所有对象 
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <returns>对象数组</returns>
        public UnityEngine.Object[] FindObjectsOfTypeAll(Type type)
        {
            ValidateType(type);
            return Resources.FindObjectsOfTypeAll(type);
        }
        /// <summary>
        /// 判断指定实例化ID的对象是否有效<br/>
        /// 可以判断对象是否被销毁
        /// </summary>
        /// <param name="guids">资源实例化ID</param>
        /// <returns></returns>
        public bool InstanceIDIsValid(int instanceId)
        {
            return Resources.InstanceIDIsValid(instanceId);
        }
        /// <summary>
        /// 判断指定实例化ID数组的对象是否有效<br/>
        /// 可以判断对象是否被销毁
        /// </summary>
        /// <param name="instanceIds">资源实例化ID数组</param>
        /// <param name="results">查询结果</param>
        public void InstanceIDsToValidArray(ReadOnlySpan<int> instanceIds, Span<bool> results)
        {
            if (results.Length < instanceIds.Length)
            {
                //TODO:抛出参数异常错误
                throw new ArgumentException(
                    "results 的容量不能小于 instanceIds 的容量");
            }
            Resources.InstanceIDsToValidArray(instanceIds, results);
        }
        /// <summary>
        /// 通过InstanceID找回对应的UnityObject 
        /// </summary>
        public T InstanceIDToObject<T>(int instanceId) where T : UnityEngine.Object
        {
            return Resources.InstanceIDToObject(instanceId) as T;
        }
        /// <summary>
        /// 通过InstanceID数组找回对应的UnityObject 
        /// </summary>
        /// <param name="instanceIds">资源实例化ID数组，类型为NativeArray，需要手动Dispose</param>
        /// <param name="results">找回结果List</param>
        public void InstanceIDToObjectList(NativeArray<int> instanceIds, List<UnityEngine.Object> results)
        {
            if (!instanceIds.IsCreated)
            {
                //TODO:抛出参数异常错误
                throw new ArgumentException("instanceIds尚未创建", nameof(instanceIds));
            }
            if (results == null)
            {
                //TODO:抛出参数异常错误
                throw new ArgumentNullException(nameof(results));
            }
            Resources.InstanceIDToObjectList(instanceIds, results);
        }

#endregion

        private string GetResKey(string path, Type type) => $"{path}_{type.FullName}";
        /// <summary>
        /// 检查资源路径是否为空
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentException"></exception>
        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //TODO:抛出运行时错误
                throw new ArgumentException("资源路径不能为空 ", nameof(path));
            }
        }
        /// <summary>
        /// 检查资源类型是否错误
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateType(Type type)
        {
            if (type == null)
            {
                //TODO:抛出运行时异常
                throw new ArgumentNullException(nameof(type));
            }
            if (!typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                //TODO:抛出参数异常错误
                throw new ArgumentException($"type 必须继承 UnityEngine.Object，" + $"type:{type.FullName}", nameof(type));
            }
        }
    }
}
