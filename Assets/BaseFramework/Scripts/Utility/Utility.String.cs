using System.IO;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        public static class String
        {
            /// <summary>获取资源名</summary>
            /// <param name="path">资源路径</param>
            /// <param name="hasExtension">是否保留扩展名</param>
            /// <returns>资源名</returns>
            public static string SubResourceName(string path, bool hasExtension = false)
            {
                if (string.IsNullOrEmpty(path))
                {
                    //TODO:打印警告日志
                    Debug.LogWarning("传入的路径为空");
                    return path;
                }

                if (hasExtension)
                {
                    return Path.GetFileName(path);
                }

                return Path.GetFileNameWithoutExtension(path);
            }

            /// <summary>移除尾部(clone)</summary>
            /// <param name="name">需要处理的字符串</param>
            /// <returns>移除后的文件名</returns>
            public static string RemoveCloneSuffix(string name)
            {
                string result = name;

                if (string.IsNullOrEmpty(result))
                {
                    //TODO:打印警告日志
                    Debug.LogWarning("传入的名字为空");
                    return result;
                }

                while (result.EndsWith("(Clone)"))
                {
                    result = result.Substring(0, result.Length - "(Clone)".Length);
                }

                return result;
            }
        }
    }
}