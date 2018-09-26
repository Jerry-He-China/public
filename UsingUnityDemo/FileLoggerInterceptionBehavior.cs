using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace UsingUnityDemo
{
    public class FileLoggerInterceptionBehavior : IInterceptionBehavior
    {
        public bool WillExecute => true;

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine($"Before to call method {input.MethodBase}");
            var result = getNext().Invoke(input, getNext);
            if (result.Exception == null)
            {
                Console.WriteLine($"After call Method {input.MethodBase} returned {result.ReturnValue}");
            }
            else
            {
                Console.WriteLine($"After call Method {input.MethodBase} throw {result.Exception}");
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
    }
}
