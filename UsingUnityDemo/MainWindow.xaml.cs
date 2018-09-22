using System.Configuration;
using System.Linq;
using System.Windows;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace UsingUnityDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IUnityContainer firstUnity = new UnityContainer();
        private IUnityContainer secondUnity = new UnityContainer();
        private IUnityContainer thirdUnity = new UnityContainer();

        public MainWindow()
        {
            InitializeComponent();
            
            firstUnity.RegisterInstance(typeof(IProductFactory), new ShoeProductFactory());


            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            section.Configure(secondUnity);

            thirdUnity.RegisterTypes(
                AllClasses.FromLoadedAssemblies()
                    .Where(x => x.IsPublic
                                && x.GetInterfaces().Any()
                                && !x.IsAbstract
                                && x.IsClass),
                WithMappings.FromAllInterfacesInSameAssembly, 
                type => (thirdUnity.Registrations
                    .Select(x => x.RegisteredType)
                    .Any(r=>type.GetInterfaces().Contains(r)))?WithName.TypeName(type):WithName.Default(type),
                WithLifetime.ContainerControlled);

            forthUnity.RegisterInstance(typeof(IProductFactory), new JacketProductFactory(), WithLifetime.ExternallyControlled(typeof(IProductFactory)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var factory = firstUnity.Resolve<IProductFactory>();
            MessageBox.Show($"Product:{factory.Create().Name}");
        }

        private void ButtonSecond_Click(object sender, RoutedEventArgs e)
        {
            var factory = secondUnity.Resolve<IProductFactory>("shoe");
            MessageBox.Show($"Product:{factory.Create().Name}");

            factory = secondUnity.Resolve<IProductFactory>("jp");
            MessageBox.Show($"Product:{factory.Create().Name}");
        }

        private void ButtonThird_Click(object sender, RoutedEventArgs e)
        {
            //var s= thirdUnity.Resolve<IProductFactory>(typeof(ShoeProductFactory).Name);
            var factory = thirdUnity.Resolve<IProductFactory>();
            MessageBox.Show($"Product:{factory.Create().Name}");
        }


        private IUnityContainer forthUnity = new UnityContainer();
        private void ButtonForth_Click(object sender, RoutedEventArgs e)
        {
            var factories = secondUnity.ResolveAll<IProductFactory>();
        }

        private void ButtonFifth_Click(object sender, RoutedEventArgs e)
        {
            forthUnity.RegisterInstance(typeof(IProductFactory), new JacketProductFactory(), WithLifetime.ExternallyControlled(typeof(IProductFactory)));
            var f = forthUnity.Resolve<IProductFactory>();
            MessageBox.Show($"1 Product:{f.Create().Name}");

            f = forthUnity.Resolve<IProductFactory>(new PropertyOverride("FactoryName", "China"));
            MessageBox.Show($"2 Product:{f.Create().Name}");


            forthUnity.RegisterType<IProductFactory, JacketProductFactory>(new InjectionProperty("FactoryName", "China"));
            f = forthUnity.Resolve<IProductFactory>(new PropertyOverride("FactoryName", "China"));
            MessageBox.Show($"3 Product:{f.Create().Name}");
        }

        private void ButtonTestIfExist_Click(object sender, RoutedEventArgs e)
        {
            var factory = thirdUnity.Registrations.Where(x => x.RegisteredType == typeof(IProductFactory))
                .ToDictionary(x => x.Name ?? string.Empty, x => x.MappedToType);
        }

        /// <summary>
        /// 演示ServiceLocator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonServiceLocator_Click(object sender, RoutedEventArgs e)
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(secondUnity));

            var jp = secondUnity.Resolve<IProductFactory>("jp");

            var shoe = ServiceLocator.Current.GetInstance<IProductFactory>("shoe");
        }

        #region PropertyInjection Demonstration

        /// <summary>
        /// 演示通过属性注入方法注入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPropertyInjection_Click(object sender, RoutedEventArgs e)
        {
            SetupPropertyInjectionEnvironment();

            var nike = secondUnity.Resolve<Nike>();
        }

        private void ButtonPropertyInjection2_Click(object sender, RoutedEventArgs e)
        {
            SetupPropertyInjectionEnvironment();
            var nike = secondUnity.Resolve<Nike>(new PropertyOverride("ProductFactory", new ShoeProductFactory()));
        }

        private void SetupPropertyInjectionEnvironment()
        {
            secondUnity.RegisterType<IProductFactory,ShoeProductFactory>();
            secondUnity.RegisterType<Nike>();
        }
        

        #endregion

    }
}
