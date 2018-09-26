#define bXD

using System;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;


namespace InterceptionByCode
{
    #region MeasureCallHandler
    public class MeasureCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
#if XD  
            var result = input.CreateMethodReturn(30);
            return result;
#else
            var watch = Stopwatch.StartNew();
            input.InvocationContext["message"] = "In Mesaure CallHandler";
            var result = getNext()(input, getNext);
            var time = watch.ElapsedMilliseconds;
            //var logger = ServiceLocator.Current.GetInstance<ILogger>();
            Console.WriteLine($"Method {input.MethodBase} took {time} milliseconds to complete");

            return result;
#endif
        }

        public int Order { get; set; }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MeasureAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MeasureCallHandler();
        }
    }

    #endregion


    # region MeasuringCallHandler

    public class MeasuringCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (input.InvocationContext.ContainsKey("message") == true)
            {
                //someone got here first... 
            }
            else
            {
                input.InvocationContext["message"] = "In Measuing Handler";
            }
            return (getNext()(input, getNext));
        }

        public int Order { get; set; }
    }

    public class MeasuringAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MeasuringCallHandler();
        }
    }
    #endregion
}
