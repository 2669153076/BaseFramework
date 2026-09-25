
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace BaseFramework.Runtime{
    /// <summary>
    /// UnityWebRequest管理器
    /// </summary>
    public class UWRMgr : SingletonAutoMono<UWRMgr>
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">资源路径</param>
        /// <param name="callback">加载成功后的回调函数</param>
        /// <param name="failCallback">加载失败后的回调函数</param>
        public void GetRes<T>(string path, Action<T> callback = null,Action failCallback=null) where T : class
        {
            StartCoroutine(GetResCoroutine(path, callback, failCallback));
        }
        /// <summary>
        /// 用于加载数据库数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <param name="failCallback"></param>
        public void GetData<T>(string path, Action<T> callback = null, Action failCallback = null) where T : class,new()
        {
            StartCoroutine(GetDataCoroutine(path, callback, failCallback));
        }

        private IEnumerator GetResCoroutine<T>(string path, Action<T> callback = null, Action failCallback = null) where T : class
        {
            Type type = typeof(T);

            UnityWebRequest rq = CreateRequest<T>(path);

            if (rq == null)
            {
                //TODO:打印错误日志
                Debug.LogError("传入类型错误");
                yield break;
            }

            yield return rq.SendWebRequest();

            if(rq.result != UnityWebRequest.Result.Success)
            {
                //TODO:打印错误日志
                Debug.LogError(rq.error);
                failCallback?.Invoke();
            }
            else
            {
                callback?.Invoke(GetResult<T>(rq));
            }

            rq.Dispose();
        }
        private IEnumerator GetDataCoroutine<T>(string path, Action<T> callback = null, Action failCallback = null) where T : class, new()
        {
            var rq = CreateRequest<T>(path);
            if (rq == null)
            {
                //TODO:打印错误日志
                Debug.LogError("传入类型错误");
                yield break;
            }
            yield return rq.SendWebRequest();

            if (rq.result != UnityWebRequest.Result.Success)
            {
                //TODO:打印错误日志
                Debug.LogError(rq.error);
                failCallback?.Invoke();
            }
            else
            {
                try
                {
                    T data = JsonMapper.ToObject<T>(rq.downloadHandler.text);
                    callback?.Invoke(data);
                }
                catch (Exception e)
                {
                    //TODO:打印错误日志
                    Debug.LogError($"JSON解析失败：{path}\n{e}");
                    failCallback?.Invoke();
                }
            }

            rq.Dispose();
        }

        /// <summary>
        /// 创建获取资源的请求
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private UnityWebRequest CreateRequest<T>(string path)
        {
            Type type = typeof(T);
            if (type == typeof(string) || type == typeof(byte[]))
            {
                return UnityWebRequest.Get(path);
            }
            else if (type == typeof(Texture) || type == typeof(Texture2D))
            {
                return UnityWebRequestTexture.GetTexture(path);
            }
            else if (type == typeof(AssetBundle))
            {
                return UnityWebRequestAssetBundle.GetAssetBundle(path);
            }
            else if (type == typeof(AudioClip))
            {
                return UnityWebRequestMultimedia.GetAudioClip(path, GetAudioType(path));
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取请求的资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rq"></param>
        /// <returns></returns>
        private T GetResult<T>(UnityWebRequest rq) where T : class 
        {
            Type type = typeof(T);

            if (type == typeof(string))
            {
                 return rq.downloadHandler.text as T;
            }
            else if (type == typeof(byte[]))
            {
                return rq.downloadHandler.data as T;
            }
            else if (type == typeof(Texture) || type == typeof(Texture2D))
            {
                return DownloadHandlerTexture.GetContent(rq) as T;
            }
            else if (type == typeof(AssetBundle))
            {
                return DownloadHandlerAssetBundle.GetContent(rq) as T;
            }
            else if (type == typeof(AudioClip))
            {
                return DownloadHandlerAudioClip.GetContent(rq) as T;
            }else
            {
                return null;
            }
        }

        private AudioType GetAudioType(string path)
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch{
                ".mp2" => AudioType.MPEG,
                ".mp3" => AudioType.MPEG,

                ".ogg" => AudioType.OGGVORBIS,

                ".wav" => AudioType.WAV,

                ".aif" => AudioType.AIFF,
                ".aiff" => AudioType.AIFF,

                ".it" => AudioType.IT,
                ".mod" => AudioType.MOD,
                ".s3m" => AudioType.S3M,
                ".xm" => AudioType.XM,

                //Unity AudioType 中存在，但通常不建议跨平台使用
                ".xma" => AudioType.XMA,
                ".vag" => AudioType.VAG,

                //Unity文档中 ACC 标记为不支持，因此不建议使用
                ".aac" => AudioType.ACC,

                //未知格式,会报异常
                _ => AudioType.UNKNOWN
            };
        }
    }
}
