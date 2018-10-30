using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;

namespace CustomProviderLib
{
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class SimpleFileLogHandler : IExceptionHandler
    {
        //public SimpleFileLogHandler(string logFilePath, string exceptionMessage,Type wrapExceptionType)
        //    : this(logFilePath, new ConstantStringResolver(exceptionMessage), wrapExceptionType)
        //{ }

        public SimpleFileLogHandler(NameValueCollection configValues)
        {
            // Extract values from Name/Value collection and validate
            //string logFilePath = configValues[configLogFilePath];
            //string exceptionMessage = configValues[configExceptionMessage];
            //string wrapException = configValues[configWrapException];
            //if (null == logFilePath || null == wrapException
            //                        || logFilePath == String.Empty || wrapException == String.Empty)
            //{
            //    throw new Exception("Invalid values in SimpleFileLogHandler configuration.");
            //}
            //else
            //{
            //    // Save configuration values in local variables for use as required
            //}
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            throw new NotImplementedException();
        }
    }
}
