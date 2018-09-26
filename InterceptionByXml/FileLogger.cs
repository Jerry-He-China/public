using System;

namespace InterceptionByXml
{
    public class FileLogger : ILogger
    {
        public string FileName { get; set; }
        public FileLogger(string fileName)
        {
            FileName = fileName;
        }


        public void Log(string message)
        {
            Console.WriteLine($"Write [{message}] into '{FileName}' file");
        }

        public string Get()
        {
            throw new NotImplementedException();
        }
    }
}
