using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessApplicationBlockDemo
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
            var dbProviderFactory = new DatabaseProviderFactory();
            var defaultDb = dbProviderFactory.CreateDefault();
            // var defaultDb = dbProviderFactory.Create("ExampleDatabase");

            using (var reader = defaultDb.ExecuteReader(CommandType.Text, "select * from log"))
            {
                DisplayRowValues(reader);
            }
        }

        void DisplayRowValues(IDataReader reader)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine("{0} = {1}", reader.GetName(i), reader[i].ToString());
                }
                Console.WriteLine();
            }
        }
    }
}
