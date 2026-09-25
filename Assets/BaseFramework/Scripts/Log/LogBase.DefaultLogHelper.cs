

using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class LogBase
    {
        /// <summary>
        /// 默认日志输出器
        /// </summary>
        private class DefaultLogHelper : ILogHelper
        {
            public void Log(LogBase.E_LogLevel level, object message)
            {
                switch (level)
                {
                    case LogBase.E_LogLevel.Debug:
                       UnityEngine.Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message.ToString()));
                        break;
                    case LogBase.E_LogLevel.Info:
                        UnityEngine.Debug.Log(message.ToString());
                        break;
                    case LogBase.E_LogLevel.Warning:
                        UnityEngine.Debug.LogWarning(message.ToString());
                        break;
                    case LogBase.E_LogLevel.Error:
                        UnityEngine.Debug.LogError(message.ToString());
                        break;
                    case LogBase.E_LogLevel.Fatal:
                        throw new FrameworkException(message.ToString());
                }
            }
        }
    }
}