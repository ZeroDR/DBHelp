using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbHelper
{
    /// <summary>
    /// \brief   写日志接口。
    /// \author  RLM
    /// \date    2014.09.04
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="ex"></param>
        void LogException(Exception ex);
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void LogException(string msg, Exception ex);
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg"></param>
        void LogError(string msg);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        void LogWarning(string msg);
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="msg"></param>
        void LogFatal(string msg);
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="msg"></param>
        void LogInfo(string msg);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg"></param>
        void LogDebug(string msg);
    }

    internal enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 通知
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 4,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 8,
        /// <summary>
        /// 严重错误
        /// </summary>
        Fatal = 16,
        /// <summary>
        /// 抛出异常
        /// </summary>
        Throw = 32
    }
}