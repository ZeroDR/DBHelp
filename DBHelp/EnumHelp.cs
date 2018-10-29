using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbHelper
{
    /// <summary>
    /// \brief   枚举帮助类。
    /// \author 
    /// \date    2014.09.05
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