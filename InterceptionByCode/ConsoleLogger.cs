using System;

namespace InterceptionByCode
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public string Get()
        {
            return "Get";
        }
    }
}
