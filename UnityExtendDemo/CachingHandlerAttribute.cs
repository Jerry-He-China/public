using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityExtendDemo
{
    public class CachingHandlerAttribute : HandlerAttribute, ICallHandler
    {
        private readonly Guid keyGuid = new Guid("ECFD1B0F-0CBA-4AA1-89A0-179B636381CA");

        public CachingHandlerAttribute(int hours, int minutes, int seconds)
        {
            Duration = new TimeSpan(hours, minutes, seconds);
        }

        public TimeSpan Duration { get; private set; }

        public override ICallHandler CreateHandler(IUnityContainer ignored)
        {
            return this;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (TargetMethodReturnsVoid(input))
            {
                return getNext()(input, getNext);
            }

            var inputs = new object[input.Inputs.Count];
            for (var i = 0; i < inputs.Length; ++i)
            {
                inputs[i] = input.Inputs[i];
            }

            var cacheKey = CreateCacheKey(input.MethodBase, inputs);
            var cache = MemoryCache.Default;
            var cachedResult = (Object[]) cache.Get(cacheKey);
            if (cachedResult == null)
            {
                var realReturn = getNext()(input, getNext);
                if (realReturn.Exception == null)
                {
                    AddToCache(cacheKey, realReturn.ReturnValue);
                }

                return realReturn;
            }

            var cachedReturn = input.CreateMethodReturn(cachedResult[0], input.Arguments);
            return cachedReturn;

        }

        private void AddToCache(string cacheKey, object value)
        {
            var cache = MemoryCache.Default;
            var cacheValue = new object[] { value };
            cache.Add(cacheKey, cacheValue, DateTime.Now + Duration);
        }

        private string CreateCacheKey(MethodBase method, object[] inputs)
        {
            var sb = new StringBuilder();
            sb.AppendFormat($"{Process.GetCurrentProcess().Id}:");
            sb.AppendFormat($"{keyGuid}");
            if (method.DeclaringType != null)
            {
                sb.Append(method.DeclaringType.FullName);
            }

            sb.Append(':');
            sb.Append(method);
            if (inputs != null)
            {
                foreach (var input in inputs)
                {
                    sb.Append(':');
                    if (input != null)
                    {
                        sb.Append(input.GetHashCode().ToString());
                    }
                }
            }

            return sb.ToString();
        }

        private bool TargetMethodReturnsVoid(IMethodInvocation input)
        {
            var targetMethod = input.MethodBase as MethodInfo;
            return targetMethod?.ReturnType == typeof(void);
        }
    }
}
