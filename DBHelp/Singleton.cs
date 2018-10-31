using System;
using System.Collections.Generic;
using System.Text;

namespace DbHelper
{
    /// <summary>
    /// 单例模式。   
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        protected Singleton() { }
        public static readonly T Instance = new T();
    }
}