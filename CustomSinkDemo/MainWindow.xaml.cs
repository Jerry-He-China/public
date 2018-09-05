using System.Diagnostics.Tracing;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace CustomSinkDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var listener = new ObservableEventListener();
            listener.LogToEmail("smtp.live.com", 25, "cheeralen@163.com", "In Proc Sample", "etw");
            listener.EnableEvents(MyCompanyEventSource.Log, EventLevel.LogAlways, EventKeywords.All);

            MyCompanyEventSource.Log.Startup();
        }
    }
}
