using System;

namespace UsingUnityDemo
{
    public class JacketProductFactory : IProductFactory
    {
        public Product Create()
        {
            return FactoryName != null ? new Jacket(FactoryName) : new Jacket();
        }

        public string FactoryName { get; set; }

        public JacketProductFactory()
        {
            var r = new Random();
            FactoryName = "Jacket - "+r.Next(10000).ToString();
        }

        public JacketProductFactory(string name)
        {
            FactoryName = name;
        }
    }
}
