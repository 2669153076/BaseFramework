using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// 全局路径管理器
    /// 所有配置路径从本表读取
    /// </summary>
    public static class GlobalConfigPaths
    {
        //唯一硬编码：路径总表自身的StreamingAssets相对路径，无法由表配置自己
        private const string RootPathTableRelative = "Config/GlobalPathTable.json";
        private static readonly string _pathPrefix = Application.streamingAssetsPath;


        private static Dictionary<int, GlobalConfigPathMeta> _pathMetaDict; //路径元数据

        /// <summary>加载路径表、程序运行时加载一次</summary>
        public static void LoadRootTable()
        {
            string fullpath = _pathPrefix + RootPathTableRelative;
            //TODO:修改资源加载方式
            string jsonText = ReadStreamingAssetsText(fullpath);

            if (string.IsNullOrEmpty(jsonText))
            {
                //TODO:打印错误日志
                Debug.LogError($"GlobalConfigPaths 根路径表加载失败，fullPath:{fullpath}");
                return;
            }

            //反序列化
            var list = JsonMapper.ToObject<List<GlobalConfigPathMeta>>(jsonText);

            _pathMetaDict = new Dictionary<int, GlobalConfigPathMeta>();
            foreach (var meta in list)
            {
                if (_pathMetaDict.ContainsKey(meta.id))
                {
                    //TODO:打印错误日志
                    Debug.LogError($"GlobalPathTable 重复id = {meta.id}");
                    continue;
                }
                _pathMetaDict.Add(meta.id, meta);
            }
            //TODO:打印普通日志
            Debug.Log($"GlobalConfigPaths 根路径表加载完成，共 {_pathMetaDict.Count} 条路径配置");
        }

        /// <summary>根据id获取配置路径</summary>
        /// <param name="id">路径id</param>
        /// <returns></returns>
        public static string GetConfigRelativePath(int id)
        {
            if (_pathMetaDict == null)
            {
                //TODO:打印错误日志
                Debug.LogError("GlobalConfigPaths 根路径表尚未 LoadRootTable");
                return null;
            }
            if (_pathMetaDict.TryGetValue(id, out var meta))
            {
                return meta.path;
            }
            //TODO:打印错误日志
            Debug.LogError($"GlobalPathTable 不存在id = {id}");
            return null;
        }

        /// <summary>根据id获取完整配置路径</summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static string GetConfigFullPath(int id)
        {
            string rel = GetConfigRelativePath(id);
            if (string.IsNullOrEmpty(rel))
                return null;

            //TODO:完整路径,可修改
            return Path.Combine(_pathPrefix, rel);
        }

        /// <summary>根据id获取路径配置元数据</summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static GlobalConfigPathMeta GetMeta(int id)
        {
            GlobalConfigPathMeta meta = null;
            _pathMetaDict?.TryGetValue(id, out meta);

            //TODO:打印错误日志
            Debug.LogError($"路径元数据获取失败,ID={id}");
            return meta;
        }

        /// <summary>卸载资源</summary>
        public static void Unload()
        {
            _pathMetaDict?.Clear();
            _pathMetaDict = null;
        }

        /// <summary>加载文件（后期可修改加载方式)</summary>
        /// <param name="fullPath">完整路径</param>
        /// <returns></returns>
        public static string ReadStreamingAssetsText(string fullPath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW www = new WWW(fullPath);
        while (!www.isDone) { }
        return www.text;
#else
            try
            {
                return File.ReadAllText(fullPath);
            }
            catch (Exception e)
            {
                //TODO:打印错误日志
                Debug.LogError($"ReadStreamingAssetsText 异常:{e.Message}");
                return null;
            }
#endif
        }
    }
}