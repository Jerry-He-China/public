using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Windows;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionByXml
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
            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            section.Configure(_unity);

            _unity.RegisterInstance<ILogger>(new FileLogger("output.txt"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var consoleLogger = _unity.Resolve<ILogger>("ConsoleLogger");
            consoleLogger.Log("This is a message.");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var fileLogger = _unity.Resolve<ILogger>();
            fileLogger.Log("This is a message.");

            var d = Intercept.ThroughProxy<ILogger>(fileLogger, new InterfaceInterceptor(),
                new IInterceptionBehavior[] {new LoggerInterceptionBehavior()});
            d.Log("This is a message22");

        }
    }
}
