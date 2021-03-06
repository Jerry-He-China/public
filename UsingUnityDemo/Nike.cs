﻿using System.Windows;
using Microsoft.Practices.Unity;

namespace UsingUnityDemo
{
    /// <summary>
    /// 演示通过属性设置来注入依赖
    /// </summary>
    /// <remarks>
    /// !!!InjectionConstructor不能同时应用到多个构造函数中
    /// </remarks>
    public class Nike
    {
        public Nike() { }

        [InjectionConstructor]
        public Nike(IProductFactory factory, IUnityContainer unity)
        {
        }

        public Nike([Dependency("Shoe")]IProductFactory factory)
        {
        }

        [Dependency("Shoe")]
        public IProductFactory ProductFactory { get; set; }

        /// <summary>
        /// 该函数在Unity容器实例化该对象时自动调用
        /// </summary>
        [InjectionMethod]
        public void Initialize()
        {
            MessageBox.Show("Initialize Nike");
        }

    }
}
