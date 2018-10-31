using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbHelper
{
    /// <summary>
    /// 枚举帮助类。
    /// </summary>
    public class EnumHelp : Singleton<EnumHelp>
    {
        public T GetInstance<T>(object member)
        {
            return GetInstance<T>(member.ToString());
        }

        public T GetInstance<T>(string member)
        {
            return (T)Enum.Parse(typeof(T), member, true);
        }
    }
}