
using LitJson;
using System;
using System.IO;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// Json文件管理器
    /// </summary>
    public sealed partial class JsonFileMgr : SingletonNormal<JsonFileMgr>
    {
        private const string jsonExtension = ".json";
        private JsonFileMgr() { }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename">文件名</param>
        /// <param name="path">文件路径</param>
        /// <param name="jsonType">Json类型</param>
        /// <returns></returns>
        public T LoadData<T>(string filename, string path = null, EJsonType jsonType = EJsonType.LitJson)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                Log.Error("Json 文件名为空");
                return default;
            }

            filename = filename.EndsWith(jsonExtension, StringComparison.OrdinalIgnoreCase) ? filename : filename + jsonExtension;

            string fullpath = FindFilePath(filename, path);

            if (string.IsNullOrEmpty(fullpath))
            {
                Log.Warning("找不到 Json 文件: {0}", filename);
                return default;
            }

            try
            {
                string jsonStr = File.ReadAllText(fullpath);
                return FromJson<T>(jsonStr);
            }
            catch (Exception e)
            {
                Log.Error("加载 Json 文件失败\nPath: {0}\nError: {1}", fullpath, e);

                return default;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="filename">文件名</param>
        /// <param name="path">文件路径</param>
        /// <param name="jsonType">Json类型</param>
        public bool SaveData(object data, string filename, string path = null, EJsonType jsonType = EJsonType.LitJson)
        {
            if (data == null)
            {
                Log.Error("保存 Json 数据为空");
                return false;
            }

            if (string.IsNullOrWhiteSpace(filename))
            {
                Log.Error("Json 文件名为空");
                return false;
            }


            filename = filename.EndsWith(jsonExtension, StringComparison.OrdinalIgnoreCase) ? filename : filename + jsonExtension;
            path ??= Application.persistentDataPath;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fullpath = Path.Combine(path, filename);

                string jsonStr = ToJson(data,jsonType);
                File.WriteAllText(fullpath, jsonStr);

                return true;
            }
            catch (Exception e)
            {
                Log.Error("保存 Json 文件失败\nPath: {0}\nError: {1}", Path.Combine(path, filename), e);

                return false;
            }
        }

        /// <summary>
        /// Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <param name="jsonType"></param>
        /// <returns></returns>
        public T FromJson<T>(string jsonStr, EJsonType jsonType = EJsonType.LitJson)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                Log.Warning("Json 字符串为空");
                return default;
            }

            try
            {
                T data = default;
                switch (jsonType)
                {
                    case EJsonType.JsonUtility:
                        data = JsonUtility.FromJson<T>(jsonStr);
                        break;
                    case EJsonType.LitJson:
                        data = JsonMapper.ToObject<T>(jsonStr);
                        break;
                }
                return data;
            }
            catch (Exception e)
            {
                Log.Error("Json 解析失败\nType: {0}\nError: {1}", typeof(T).FullName, e);

                return default;
            }
        }

        /// <summary>
        /// dioxin转换为字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="jsonType"></param>
        /// <returns></returns>
        public string ToJson(object data, EJsonType jsonType = EJsonType.LitJson)
        {
            if (data == null)
            {
                Log.Warning("Json 数据为空");
                return string.Empty;
            }

            try
            {
                string jsonStr = string.Empty;
                switch (jsonType)
                {
                    case EJsonType.JsonUtility:
                        jsonStr = JsonUtility.ToJson(data);
                        break;
                    case EJsonType.LitJson:
                        jsonStr = JsonMapper.ToJson(data);
                        break;
                }
                return jsonStr;
            }
            catch (Exception e)
            {
                Log.Error("Json 序列化失败\nType: {0}\nError: {1}", data.GetType().FullName, e);

                return string.Empty;
            }
        }

        /// <summary>
        /// 查找文件路径，优先级：自定义传入路径 > StreamingAssets > PersistentDataPath
        /// </summary>
        /// <param name="filename">目标文件名（带后缀）</param>
        /// <param name="path">指定查找目录，不为空时仅在该目录查找</param>
        /// <returns>找到返回完整文件路径，未找到返回null</returns>
        private string FindFilePath(string filename, string path)
        {
            //指定路径时，只检查指定路径。
            if (!string.IsNullOrEmpty(path))
            {
                string customPath = Path.Combine(path, filename);
                if (File.Exists(customPath))
                {
                    return customPath;
                }
                return null;
            }

            //StreamingAssets目录查找
            string streamingPath = Path.Combine(Application.streamingAssetsPath, filename);
            if (File.Exists(streamingPath))
            {
                return streamingPath;
            }

            //PersistentDataPath目录查找
            string persistentPath = Path.Combine(Application.persistentDataPath, filename);
            if (File.Exists(persistentPath))
            {
                return persistentPath;
            }

            return null;
        }

    }
}