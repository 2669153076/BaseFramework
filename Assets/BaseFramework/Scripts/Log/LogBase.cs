using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// 日志工具基类
    /// </summary>
    public static partial class LogBase
    {
        private static ILogHelper _logHelper= new DefaultLogHelper();

        /// <summary>
        /// 设置日志辅助器
        /// </summary>
        /// <param name="logHelper"></param>
        public static void SetLogHelper(ILogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        #region Debug
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(object message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <typeparam name="T">日志参数类型</typeparam>
        /// <param name="format">日志格式</param>
        /// <param name="arg">日志参数</param>
        public static void Debug<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg));
        }
        public static void Debug<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2));
        }
        public static void Debug<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Debug<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Debug<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        #endregion

        #region Info
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, message);

        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <typeparam name="T">日志参数类型</typeparam>
        /// <param name="format">日志格式</param>
        /// <param name="arg">日志参数</param>
        public static void Info<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg));
        }
        public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Info<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        public static void Info<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        #endregion

        #region Warning
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(object message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, message);

        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <typeparam name="T">日志参数类型</typeparam>
        /// <param name="format">日志格式</param>
        /// <param name="arg">日志参数</param>
        public static void Warning<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg));
        }

        public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Warning<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        #endregion

        #region Error
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, message);

        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <typeparam name="T">日志参数类型</typeparam>
        /// <param name="format">日志格式</param>
        /// <param name="arg">日志参数</param>
        public static void Error<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg));
        }
        public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Error<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        public static void Error<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        #endregion

        #region Fatal
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(object message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, message);
        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, message);

        }
        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息
        /// </summary>
        /// <typeparam name="T">日志参数类型</typeparam>
        /// <param name="format">日志格式</param>
        /// <param name="arg">日志参数</param>
        public static void Fatal<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg));
        }
        public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Fatal<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }
            _logHelper.Log(E_LogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        #endregion
    }
}