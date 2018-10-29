using System;
using System.Collections.Generic;
using System.Text;

namespace DbHelper
{
    /// <summary>
    /// \brief   单例模式。
    /// \details 模板。
    /// \author  RLM
    /// \date    2014.09.04
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        protected Singleton() { }
        public static readonly T Instance = new T();
    }
}