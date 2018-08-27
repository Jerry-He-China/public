using System.Windows;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace LoggingDemo
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

        private void ButtonWEV_Click(object sender, RoutedEventArgs e)
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

            Logger.Writer.Write(logEntry,"General");
        }

        private void ButtonDatabase_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
