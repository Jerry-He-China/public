using System.Diagnostics;
using System.Reflection;
using Microsoft.Practices.Unity;
using System.Windows;

namespace UnityExtendDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUnityContainer _unity = new UnityContainer();
        public MainWindow()
        {
            InitializeComponent();
            
            _unity.RegisterType<ILogger, ConsoleLogger>(new PooledLifetimeManager(2));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var first = _unity.Resolve<ILogger>();
            //var second = _unity.Resolve<ILogger>();
            //var third = _unity.Resolve<ILogger>();

            //Trace.Assert(first!=second);
            //Trace.Assert(first==third);
            //Trace.Assert(second!=third);
        }

        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            var unity = new UnityContainer();
            unity.RegisterTypes(new InterfaceToClassConvention(unity, Assembly.GetExecutingAssembly()));
        }
    }
}
