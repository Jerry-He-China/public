using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.ComponentModel;
using System.Configuration;

namespace CustomProviderLib
{
    [ResourceDescription(typeof(Resources),"")]
    public class FullIntegrationSimpleFileLogHandlerData : ExceptionHandlerData
    {
        public FullIntegrationSimpleFileLogHandlerData() : base(typeof(FullIntegrationSimpleFileLogHandler))
        { }

        public FullIntegrationSimpleFileLogHandlerData(string name, string logFilePath,
            string exceptionMessage, string wrapExceptionTypeName):
            base(name,typeof(FullIntegrationSimpleFileLogHandler))
        {
            LogFilePath = logFilePath;
            ExceptionMessage = exceptionMessage;
            WrapExceptionTypeName = wrapExceptionTypeName;
        }

        public string WrapExceptionTypeName { get; set; }

        [ConfigurationProperty("logFilePathProperty", IsRequired = true,
            DefaultValue = @"c:.temp.test.txt")]
        [ResourceDescription(typeof(Resources), "SimpleFileLogDataLogFilePathDescription")]
        [ResourceDisplayName(typeof(Resources), "SimpleFileLogDataLogFilePathDisplayName")]
        [Editor(CommonDesignTime.EditorTypes.FilteredFilePath,
            CommonDesignTime.EditorTypes.UITypeEditor)]
        public string LogFilePath { get; set; }

        [ConfigurationProperty("exceptionMessageProperty", IsRequired = false)]
        [Editor(CommonDesignTime.EditorTypes.MultilineText,
            CommonDesignTime.EditorTypes.FrameworkElement)]
        public string ExceptionMessage { get; set; }

        [ConfigurationProperty("ExceptionMessageResourceTypeNameProperty")]
        [ResourceCategory(typeof(ResourceCategoryAttribute), "CategoryLocalization")]
        [Editor(CommonDesignTime.EditorTypes.TypeSelector,
            CommonDesignTime.EditorTypes.UITypeEditor)]
        [BaseType(typeof(Object), TypeSelectorIncludes.None)]
        public string ExceptionMessageResourceType { get; set; }
    }

    public class FullIntegrationSimpleFileLogHandler : IExceptionHandler
    {
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            throw new NotImplementedException();
        }
    }
}
