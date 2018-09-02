using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace NoExceptionPolicyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 编码方式定义异常策略
            //----------------------------------------------------------------------------------------
            //var policies = new List<ExceptionPolicyDefinition>();

            ////定义一个捕获FormatException异常的策略，什么也不做，只是过滤
            //var myTestExceptionPolicy = new List<ExceptionPolicyEntry>
            //{
            //    {
            //        new ExceptionPolicyEntry(typeof(FormatException), PostHandlingAction.None,
            //            new IExceptionHandler[]
            //        {
            //        })
            //    }
            //};
            //policies.Add(new ExceptionPolicyDefinition("MyTestExceptionPolicy", myTestExceptionPolicy));


            //// 把异常策略加入异常管理
            //var exManager = new ExceptionManager(policies);
            //-------------------------------------------------------------------------------------------

            #endregion

            #region 通过配置文件来定义异常策略
            // 通过配置文件来定义策略

            var logWriter = new LogWriterFactory().Create();
            Logger.SetLogWriter(logWriter);
            var exManager = new ExceptionPolicyFactory().CreateManager();
            #endregion

            ExceptionPolicy.SetExceptionManager(exManager);

            demo2();
            demo1();

            Console.ReadKey();
        }

        static void demo1()
        {

            try
            {
                ThrowFormatException();
            }
            catch (FormatException ex)
            {
                ExceptionPolicy.HandleException(ex, "NoThrowPolicy");
                Console.WriteLine("捕获到Format异常");
            }
            catch (Exception)
            {
                Console.WriteLine("捕获到异常");
            }
        }

        static void demo2()
        {
            try
            {
                throw new NotImplementedException("This is a test exception.");
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "LogPolicy");
            }
        }

        private static void ThrowFormatException()
        {
            Console.WriteLine("马上就抛出异常");
            var i = int.Parse("A");   //throw a FormatException
            Console.WriteLine("***不应该执行到这里***");
        }
    }
}
