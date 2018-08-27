
using System;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.Caching;

using System.Runtime.Caching;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICacheManager _primitivesCache;

        private MemoryCache _memoryCache;


        public MainWindow()
        {
            InitializeComponent();

            _primitivesCache = CacheFactory.GetCacheManager();
            _memoryCache = MemoryCache.Default;
        }

        private void ButtonGet_Click(object sender, RoutedEventArgs e)
        {
            var product = (_primitivesCache.GetData("1001") as Product);
            MessageBox.Show(product.ProductName + ":" + product.ProductPrice);

            var p1 = MemoryCache.Default.Get("Nike") as Product;

            MessageBox.Show(p1.ProductName + ":" + p1.ProductPrice);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product
            {
                ProductId = 1001,
                ProductName = "Nike",
                ProductPrice = 320.00M
            };

            _primitivesCache.Add(product.ProductId.ToString(), product);

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5)
            };


            MemoryCache.Default.Add(product.ProductName, product, policy);
        }
    }
}
