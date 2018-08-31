using System.Windows;

using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace LoggingToEventViewerDemo
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

        private void ButtonWriteLog_Click(object sender, RoutedEventArgs e)
        {
            var logEntry = new LogEntry
            { 
                Message = "Write Log into Event demo",
                MachineName = "Dummy PC",
                Title = "dummy log"
            };
            logEntry.Categories.Add("Unprocessed Category");
            logEntry.Categories.Add("General");
            Logger.Writer.Write(logEntry);
        }
    }
}
