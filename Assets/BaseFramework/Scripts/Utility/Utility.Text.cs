
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 字符相关函数，重写便于定位
        /// </summary>
        public static class Text
        {
            private static ITextHelper _textHelper = new DefaultTextHelper();

            /// <summary>
            /// 设置字符辅助器
            /// </summary>
            /// <param name="textHelper"></param>
            public static void SetTextHelper(ITextHelper textHelper)
            {
                _textHelper = textHelper;
            }

            /// <summary>
            /// 获取格式化字符串，相当于string.Format("玩家名:{0},等级{1}级", name, level)
            /// </summary>
            /// <typeparam name="T">字符串参数类型</typeparam>
            /// <param name="format">字符串格式</param>
            /// <param name="arg">参数</param>
            /// <returns>格式化后的字符串</returns>
            /// <exception cref="BaseFrameworkException">游戏框架内的异常</exception>
            public static string Format<T>(string format, T arg)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg);
                }
                return _textHelper.Format(format, arg);
            }

            public static string Format<T1, T2>(string format, T1 arg1, T2 arg2)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2);
                }
                return _textHelper.Format(format, arg1, arg2);
            }

            public static string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3);
                }
                return _textHelper.Format(format, arg1, arg2, arg3);
            }

            public static string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4);
            }

            public static string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5);
            }

            public static string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                }
                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }

            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            {
                if (format == null)
                {
                    Log.Error("传入的参数(Format)为空");
                }
                if (_textHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                }

                return _textHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }

        }
    }
}
