using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DbHelper
{
    public class Logger : Singleton<Logger>, ILogger
    {
        public Logger()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + @"logs\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _logfile = dir + "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".html";
            _sb.Append("<table style='font-size:13px;table-layout:fixed;word-break: break-all; word-wrap: break-word;'  width=100% border=0 cellspacing=0 cellpadding=0>");
            _sb.Append("<tr><td width=60px bgcolor={0}>时间:</td><td bgcolor={1}>{2}</td></tr>");
            _sb.Append("<tr><td width=240 bgcolor=#BED1ED>类型:</td><td bgcolor=#dfe8f6 >{3}</td></tr>");
            _sb.Append("<tr><td width=240 bgcolor=#BED1ED>函数:</td><td bgcolor=#D9E3F4 >{4}</td></tr>");
            _sb.Append("<tr><td width=240 bgcolor=#BED1ED>信息:</td><td bgcolor=#dfe8f6 >{5}</td></tr>");
            _sb.Append("<tr><td width=240 bgcolor=#BED1ED>堆栈:</td><td bgcolor=#D9E3F4 ><xmp>{6}</xmp></td></tr></table>");
        }

        #region ILogger 成员

        public void LogException(Exception ex)
        {
            Write(ex);
        }

        public void LogException(string msg, Exception ex)
        {
            Write(msg, ex);
        }

        public void LogError(string msg)
        {
            Write(LogLevel.Error, _space, msg, _space);
        }

        public void LogWarning(string msg)
        {
            Write(LogLevel.Warning, _space, msg, _space);
        }

        public void LogFatal(string msg)
        {
            Write(LogLevel.Fatal, _space, msg, _space);
        }

        public void LogInfo(string msg)
        {
            Write(LogLevel.Info, _space, msg, _space);
        }

        public void LogDebug(string msg)
        {
            Write(LogLevel.Debug, _space, msg, _space);
        }

        #endregion

        private void Write(Exception ex)
        {
            Write(LogLevel.Throw, ex.TargetSite.ToString(), ex.Message, ex.StackTrace.Trim());
        }

        private void Write(string msg, Exception ex)
        {
            Write(LogLevel.Throw, ex.TargetSite.ToString(), msg + "|" + ex.Message, ex.StackTrace.Trim());
        }

        private void Write(LogLevel level, string source, string msg, string stacktrace)
        {
            StreamWriter sw = null;
            lock (_lockedOb)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(_logfile);
                    if (fileInfo.Exists)
                    {//存在日志文件
                        sw = fileInfo.AppendText();
                    }
                    else
                    {//创建日志文件
                        sw = fileInfo.CreateText();
                        sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
                    }

                    sw.WriteLine(FormatString(level, source, msg, stacktrace));
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
        }

        private string FormatString(LogLevel level, string source, string msg, string stacktrace)
        {
            string color = "#F29744";
            switch (level)
            {
                case LogLevel.Fatal: { color = "Red"; break; }
                case LogLevel.Error: { color = "ff8000"; break; }
                case LogLevel.Warning: { color = "Yellow"; break; }
                case LogLevel.Throw: { color = "#ea12f5"; break; }
                default: { color = "Silver"; break; }
            }
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff");
            return string.Format(_sb.ToString(), color, color, time, level.ToString(), source, msg, stacktrace);
        }

        readonly string _logfile;
        StringBuilder _sb = new StringBuilder();
        readonly string _space = string.Empty;
        object _lockedOb = new object();
    }
}