using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BaseFramework.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 默认文本格式化辅助器
        /// 采用[ThreadStatic]线程独立StringBuilder，复用缓冲区减少字符串GC，泛型重载降低值类型装箱
        /// </summary>
        private class DefaultTextHelper : ITextHelper
        {
            /// <summary>
            /// StringBuilder初始预分配字符容量，不是最大上限；用于减少内部char数组扩容产生的GC
            /// </summary>
            private const int StringBuilderCapacity = 1024;

            /// <summary>
            /// 线程独立缓存StringBuilder
            /// [ThreadStatic]特性：每个线程拥有独立实例，线程安全，无需锁；
            /// 不要在声明处直接new，只会在第一个访问线程初始化，其他线程拿到null
            /// </summary>
            [ThreadStatic]
            private static StringBuilder _cachedStringBuilder = null;

            /// <summary>
            /// 格式化字符串（1个泛型参数）
            /// </summary>
            public string Format<T>(string format, T arg)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（2个泛型参数）
            /// </summary>
            public string Format<T1, T2>(string format, T1 arg1, T2 arg2)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（3个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（4个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（5个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（6个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（7个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（8个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（9个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（10个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（11个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（12个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（13个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（14个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（15个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 格式化字符串（16个泛型参数）
            /// </summary>
            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            {
                if (format == null)
                {
                    throw new FrameworkException("传入的format参数为空");
                }

                CheckCachedStringBuilder();
                _cachedStringBuilder.Length = 0;
                _cachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                return _cachedStringBuilder.ToString();
            }

            /// <summary>
            /// 检查当前线程的StringBuilder实例，不存在则新建
            /// </summary>
            private static void CheckCachedStringBuilder()
            {
                if (_cachedStringBuilder == null)
                {
                    _cachedStringBuilder = new StringBuilder(StringBuilderCapacity);
                }
            }
        }
    }
}
