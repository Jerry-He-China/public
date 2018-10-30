using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.Practices.Unity;

namespace UnityExtendDemo
{
    public class DefaultInterception : Interception
    {
        private static readonly IInterceptor[] interceptors = typeof(IInterceptor)
            .Assembly
            .GetExportedTypes()
            .Where(type => typeof(IInterceptor).IsAssignableFrom(type) && type.IsAbstract && type.IsInterface)
            .Select(type => Activator.CreateInstance(type) as IInterceptor)
            .ToArray();

        protected override void Initialize()
        {
            base.Initialize();
            var configSource = ConfigurationSourceFactory.Create();
            var section = configSource.GetSection(PolicyInjectionSettings.SectionName) as PolicyInjectionSettings;
            if (section != null)
            {
                PolicyInjectionSettings.ConfigureContainer(Container, configSource);
            }

            foreach (var registration in Container.Registrations)
            {
                if (registration.RegisteredType.Assembly != typeof(IUnityContainer).Assembly)
                {
                    SetInterceptorFor(registration.RegisteredType, registration.MappedToType, registration.Name);
                }
            }

            Context.Registering += delegate(object sender, RegisterEventArgs e)
            {
                SetInterceptorFor(e.TypeFrom ?? e.TypeTo, e.TypeTo, e.Name);
            };

            Context.RegisteringInstance += delegate(object sender, RegisterInstanceEventArgs e)
            {
                SetInterceptorFor(e.RegisteredType, e.Instance.GetType(), e.Name);
            };
        }

        private void SetInterceptorFor(Type typeFrom, Type typeTo, string name)
        {
            foreach (var interceptor in interceptors)
            {
                if (interceptor.CanIntercept(typeFrom) && interceptor.GetInterceptableMethods(typeFrom, typeTo).Any())
                {
                    if (interceptor is IInstanceInterceptor)
                    {
                        Container.Configure<Interception>()
                            .SetInterceptorFor(typeFrom, name, interceptor as IInstanceInterceptor);
                    }
                    else if (interceptor is ITypeInterceptor)
                    {
                        Container.Configure<Interception>()
                            .SetInterceptorFor(typeFrom, name, interceptor as ITypeInterceptor);
                    }

                    break;
                }
            }
        }

    }
}
