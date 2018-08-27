using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Windows;

namespace LoggingToXmlDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonDatabase_Click(object sender, RoutedEventArgs e)
        {
            var logEntry = new LogEntry
            {
                EventId = 1,
                Priority = 1,
                Title = "Test",
                Message = "input your any comments here",
            };

            logEntry.Categories.Add("C# Study");
            logEntry.Categories.Add("Finished");

            Logger.Writer.Write(logEntry, "General");
        }
    }
}
