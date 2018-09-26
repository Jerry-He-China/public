using System;

namespace InterceptionByXml
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
