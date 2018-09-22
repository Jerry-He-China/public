using System;

namespace UsingUnityDemo
{
    public class ShoeProductFactory : IProductFactory
    {
        public Product Create()
        {
            return FactoryName != null ? new Shoe(FactoryName) : new Shoe();
        }
        public string FactoryName { get; set; }

        public ShoeProductFactory()
        {
            var r = new Random();
            FactoryName = "Shoe - " + r.Next(10000).ToString();
        }
    }
}
