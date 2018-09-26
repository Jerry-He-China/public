using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace InterceptionByCode
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CachingHandlerAttribute : HandlerAttribute
    {
        public CachingHandlerAttribute(int hours, int minutes, int seconds)
        {
            this.Duration = new TimeSpan(hours, minutes, seconds);
        }

        public TimeSpan Duration { get; set; }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CachingHandler(Duration) {Order = this.Order};

        }
    }

    public class CachingHandler : ICallHandler
    {
        public CachingHandler(TimeSpan duration)
        {
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            throw new NotImplementedException();
        }

        public int Order { get; set; }
    }
}
