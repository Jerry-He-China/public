using System;
using System.Windows;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionByCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IUnityContainer _unityContainer = new UnityContainer();

        public MainWindow()
        {
            InitializeComponent();
            _unityContainer.AddNewExtension<Interception>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _unityContainer.RegisterType<ILogger, ConsoleLogger>(
                new Interceptor<TransparentProxyInterceptor>(),
                new InterceptionBehavior(new ConsoleLoggerInterceptionBehavior())
                );

            var logger = _unityContainer.Resolve<ILogger>();
            
            logger.Log("This is a message.");
            Console.WriteLine($"__________________________________");
            logger.Get();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _unityContainer.RegisterType<MyObject>(
                new Interceptor<TransparentProxyInterceptor>(),
                new InterceptionBehavior(new ConsoleLoggerInterceptionBehavior())
            );
            var myObject = _unityContainer.Resolve<MyObject>();
            myObject.Name = "set name";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _unityContainer.RegisterType<ICalculator, SimpleCalculator>()
                .Configure<Interception>()
                .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());


            var simpleCalculator = _unityContainer.Resolve<ICalculator>();
            Console.WriteLine($@"(1+2)={simpleCalculator.Add(1, 2)}");
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            _unityContainer
                //.RegisterType<ILogger, ConsoleLogger>(
                //    new Interceptor<TransparentProxyInterceptor>(),
                //    new InterceptionBehavior(new ConsoleLoggerInterceptionBehavior())
                //)
                //.AddExtension(new Interception())
                .Configure<Interception>()
                .AddPolicy("Add MeasureCallHandler to ConsoleLogger.Log")
                .AddMatchingRule(new TypeMatchingRule(typeof(ConsoleLogger)))
                .AddMatchingRule(new MemberNameMatchingRule("xLog2"))
                .AddCallHandler<MeasureCallHandler>();
            _unityContainer
                .RegisterType<ILogger, ConsoleLogger>(
                    new Interceptor<TransparentProxyInterceptor>(),
                    new InterceptionBehavior(new ConsoleLoggerInterceptionBehavior())
                );

            var logger = _unityContainer.Resolve<ILogger>();

            logger.Log("This is a message22.");
        }
    }

    public class PolicyInjectionStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            base.PreBuildUp(context);
            if (context.Policies.Get<IBuilderPolicy>(context.BuildKey) == null)
            {
                context.Policies.Set<IBuilderPolicy>(new BuildKeyMappingPolicy(context.BuildKey),context.BuildKey);
            }
        }
    }

}
