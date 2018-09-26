using System;

namespace UsingUnityDemo
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($@"{message}");
        }
    }
}
