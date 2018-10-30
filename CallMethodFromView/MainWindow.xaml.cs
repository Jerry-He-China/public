using System.Windows;

namespace CallMethodFromView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }

    public class MainWindowViewModel
    {
        public void Submit()
        {
            MessageBox.Show("Submit");
        }
    }

}
