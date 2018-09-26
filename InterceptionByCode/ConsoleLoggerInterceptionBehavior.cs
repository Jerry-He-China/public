using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace InterceptionByCode
{
    public class ConsoleLoggerInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine($"Before call the method");
            var methodReturn = getNext().Invoke(input,getNext);
            var result = methodReturn.ReturnValue;
            Console.WriteLine($"After call the method");
            return methodReturn;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            //return Type.EmptyTypes;
            return new Type[] { typeof(ICallHandler)};
        }

        public bool WillExecute => true;
    }
}
