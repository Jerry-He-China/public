using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace UnityExtendDemo
{
    public class InterfaceToClassConvention : RegistrationConvention
    {
        private readonly IUnityContainer unity;
        private readonly IEnumerable<Type> types;

        public InterfaceToClassConvention(IUnityContainer unity, params Assembly[] assemblies) : this(unity, assemblies.SelectMany(a=>a.GetExportedTypes()).ToArray())
        {
            this.unity = unity;
        }

        public InterfaceToClassConvention(IUnityContainer unity, params Type[] types)
        {
            this.unity = unity;
            this.types = types ?? Enumerable.Empty<Type>();
        }

        public override IEnumerable<Type> GetTypes()
        {
            return types.Where(x => x.IsPublic && x.GetInterfaces().Any() && !x.IsAbstract && x.IsClass);
        }

        public override Func<Type, IEnumerable<Type>> GetFromTypes()
        {
            return WithMappings.FromAllInterfacesInSameAssembly;
        }

        public override Func<Type, string> GetName()
        {
            return type => unity.Registrations.Select(x => x.RegisteredType).Any(r => r.GetInterfaces().Contains(r))
                ? WithName.TypeName(type)
                : WithName.Default(type);
        }

        public override Func<Type, LifetimeManager> GetLifetimeManager()
        {
            return WithLifetime.None;
        }

        public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers()
        {
            return (x => Enumerable.Empty<InjectionMember>());
        }
    }
}
