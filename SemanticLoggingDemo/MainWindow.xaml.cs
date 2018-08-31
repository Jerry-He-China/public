using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using System.Configuration;
using System.Diagnostics.Tracing;
using System.Windows;

namespace SemanticLoggingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region 写入Console示例

            // 创建并订阅一个接收器, 并总是记录所有Log，不分级别
            ConsoleLog.CreateListener(null, null)
                .EnableEvents(MyCompanyEventSource.Log, EventLevel.LogAlways);
            #endregion


            #region 写入普通文件示例
            // 需要引用EnterpriseLibray.SemanticLogging.TextFile
            // 创建并订阅一个FlatFile接收器, 并总是记录所有Informational级别以上的日志
            // 指定消息格式化，启用异步写入
            FlatFileLog.CreateListener("flat.log", new EventTextFormatter(), true)
                .EnableEvents(MyCompanyEventSource.Log, EventLevel.Informational);


            // 创建并订阅一个Rolling文件接收器, 并总是记录所有Informational级别以上的日志
            RollingFlatFileLog.CreateListener("roll.log", 
                    rollSizeKB:5,   //大于5KB，滚动到新文件
                    timestampPattern:"yyyy",
                    rollFileExistsBehavior: RollFileExistsBehavior.Increment, //在原有的文件上追加
                    rollInterval:RollInterval.Day,  //每天滚动
                    formatter: new XmlEventTextFormatter(), //使用XML文件格式, 如果想要JSON格式使用JsonEventTextFormatter
                    maxArchivedFiles:5, 
                    isAsync:true)
                .EnableEvents(MyCompanyEventSource.Log, EventLevel.Informational);
            #endregion

            #region 写入数据库示例
            // 需要引用EnterpriseLibray.SemanticLogging.Database
            // 在写入数据库之前必须创建数据库,用 管理员 权限，运行CreateSemanticLoggingDb.cmd
            // 该文件在packages/EnterpriseLibrary.SemanticLogging.Database.2.0.1406.1/scripts下
            // 或者直接使用两个SQL文件来创建数据库
            SqlDatabaseLog.CreateListener("SemanticLoggingDemo", //可以是任何字符串，但最好使用应用程序名称
                ConfigurationManager.ConnectionStrings["Tracing"].ConnectionString, //连接字符串
                tableName: "Tracing")
                .EnableEvents(MyCompanyEventSource.Log, EventLevel.Critical);   //只写入Critical日志

            #endregion

        }

        private void ButtonSemantic_Click(object sender, RoutedEventArgs e)
        {
            MyCompanyEventSource.Log.Startup();
            MyCompanyEventSource.Log.Failure("可以是任何信息");
        }
    }
}
