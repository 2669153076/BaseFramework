
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// AssetBundle管理器
    /// </summary>
    public class AssetBundleMgr : SingletonAutoMono<AssetBundleMgr>
    {
        private AssetBundle _mainAB = null; //主包
        private AssetBundleManifest _manifest = null;   //依赖包信息

        /// <summary>
        /// 主包名
        /// </summary>
        private string MainABName
        {
            get
            {
#if UNITY_IOS
                return "IOS";
#elif UNITY_ANDROID
                return "Android";
#else
                return "PC";
#endif
            }
        }
        /// <summary>
        /// AB包存放路径
        /// </summary>
        private string PathURL
        {
            get => Path.Combine(Application.dataPath, $"../AssetBundles/{MainABName}");
        }

        private readonly Dictionary<string, AssetBundle> _abDict = new Dictionary<string, AssetBundle>();   //资源包缓存字典

        /// <summary>
        /// 加载主包
        /// </summary>
        private void LoadMainAB()
        {
            if (_mainAB is null)
            {
                //加载主包与相关的依赖包信息
                _mainAB = AssetBundle.LoadFromFile(Path.Combine(PathURL, MainABName));
                _manifest = _mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                _abDict.Add(MainABName, _mainAB);
            }
        }
        /// <summary>
        /// 加载依赖包
        /// </summary>
        /// <param name="abName"></param>
        private void LoadDependencies(string abName)
        {
            LoadMainAB();

            string[] strs = _manifest.GetAllDependencies(abName);   //获取依赖包的包名
            AssetBundle ab = null;
            for (int i = 0; i < strs.Length; i++)
            {
                if (!_abDict.ContainsKey(strs[i]))
                {
                    //如果缓存字典中没有对应的依赖包数据，就加载
                    ab = AssetBundle.LoadFromFile(Path.Combine(PathURL, strs[i]));
                    _abDict.Add(strs[i], ab);
                }
            }
        }

        /// <summary>
        /// 加载指定AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public AssetBundle LoadAB(string abName)
        {
            LoadMainAB();

            LoadDependencies(abName);

            //如果字典中查询不到，就加载指定包名的包
            if (!_abDict.ContainsKey(abName))
            {
                var ab = AssetBundle.LoadFromFile(Path.Combine(PathURL, abName));
                _abDict.Add(abName, ab);
            }

            if (_abDict[abName] is null)
            {
                Debug.LogWarning($"AB包:{abName}正在异步加载/异步卸载中");
            }
            return _abDict[abName];

        }
        /// <summary>
        /// 异步加载指定AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="callback"></param>
        public void LoadABAsync(string abName, Action<AssetBundle> callback = null)
        {
            StartCoroutine(LoadABAsyncCoroutine(abName,callback));
        }
        private IEnumerator LoadABAsyncCoroutine(string abName,Action<AssetBundle> callback = null)
        {
            LoadMainAB();

            string[] strs = _manifest.GetAllDependencies(abName);   //获取依赖包的包名
            AssetBundleCreateRequest rq = null;
            for (int i = 0; i < strs.Length; i++)
            {
                if (!_abDict.ContainsKey(strs[i]))
                {
                    _abDict.Add(strs[i], null);  //占位，防止多次调用异步加载
                    //如果缓存字典中没有对应的依赖包数据，就加载
                    rq = AssetBundle.LoadFromFileAsync(Path.Combine(PathURL, strs[i]));
                    yield return rq;
                    _abDict[strs[i]]=rq.assetBundle;
                }
                else
                {
                    //字典中存在，表示正在加载中或者加载完毕
                    //如果资源加载中，就一直等待
                    while (_abDict[strs[i]] is null)
                    {
                        yield return null;
                    }
                }
            }

            //如果字典中查询不到，就加载指定包名的包
            if (!_abDict.ContainsKey(abName))
            {
                _abDict.Add(abName, null);
                rq = AssetBundle.LoadFromFileAsync(Path.Combine(PathURL, abName));
                yield return rq;
                _abDict[abName] = rq.assetBundle;
            }
            else
            {
                while (_abDict[abName] is null)
                {
                    yield return null;
                }
            }

            callback?.Invoke(_abDict[abName]);
        }

        /// <summary>
        /// 加载指定资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName">包名</param>
        /// <param name="resName">资源名</param>
        /// <returns></returns>
        public T Load<T>(string abName, string resName) where T : UnityEngine.Object
        {
            LoadAB(abName);

            return _abDict[abName].LoadAsset<T>(resName);   //加载资源

        }

        /// <summary>
        /// 加载指定资源
        /// </summary>
        /// <param name="abName">包名</param>
        /// <param name="resName">资源名</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public UnityEngine.Object Load(string abName, string resName, System.Type type)
        {
            LoadAB(abName);

            return _abDict[abName].LoadAsset(resName, type);
        }

        /// <summary>
        /// 异步加载指定资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="callback"></param>
        public void LoadAsync<T>(string abName, string resName, Action<T> callback = null) where T : UnityEngine.Object
        {
            StartCoroutine(LoadAsyncCoroutine(abName, resName, callback));
        }
        private IEnumerator LoadAsyncCoroutine<T>(string abName, string resName, Action<T> callback = null) where T : UnityEngine.Object
        {
           yield return LoadABAsyncCoroutine(abName);

            var rq = _abDict[abName].LoadAssetAsync<T>(resName);

            yield return rq;

            callback?.Invoke(rq.asset as T);
        }
        /// <summary>
        /// 异步加载指定资源
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        public void LoadAsync(string abName, string resName, System.Type type, Action<UnityEngine.Object> callback = null)
        {
            StartCoroutine(LoadAsyncCoroutine(abName, resName, type, callback));
        }
        private IEnumerator LoadAsyncCoroutine(string abName, string resName, System.Type type, Action<UnityEngine.Object> callback = null)
        {

            yield return LoadABAsyncCoroutine(abName);

            var rq = _abDict[abName].LoadAssetAsync(resName, type);

            yield return rq;

            callback?.Invoke(rq.asset);
        }

        /// <summary>
        /// 卸载AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="unloadAllObjects"></param>
        public void UnloadAB(string abName,bool unloadAllObjects = false)
        {
            if (_abDict.ContainsKey(abName))
            {
                if (_abDict[abName] is null)
                {
                    Debug.Log("正在异步加载中，无法进行卸载");
                    return;
                }
                _abDict[abName].Unload(unloadAllObjects);
                _abDict.Remove(abName);
            }
        }
        /// <summary>
        /// 异步卸载AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="unloadAllObjects"></param>
        public void UnloadABAsync(string abName, bool unloadAllObjects = false)
        {
            if (_abDict.ContainsKey(abName))
            {
                if (_abDict[abName] is null)
                {
                    Debug.Log("正在异步加载中，无法进行卸载");
                    return;
                }
                var ab = _abDict[abName];
                _abDict[abName] = null;
                var aq = ab.UnloadAsync(unloadAllObjects);
                aq.completed += op =>
                {
                    //只有指定AB包为空才移除，防止异步卸载的同时，又调用了异步加载
                    if(_abDict.TryGetValue(abName,out var current)&&current is null)
                    {
                        _abDict.Remove(abName);
                    }
                };
            }
        }

        public void UnloadAllAB()
        {
            StopAllCoroutines();
            AssetBundle.UnloadAllAssetBundles(false);
            _abDict.Clear();
            _mainAB = null;
            _manifest = null;
        }
    }
}
