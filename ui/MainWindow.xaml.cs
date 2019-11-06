using System;
using System.Collections.Generic;
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

namespace sbid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserVM _uvm;

        public MainWindow()
        {
            InitializeComponent();
            //this.WindowState = System.Windows.WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            _uvm = base.DataContext as UserVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _uvm.UserName = "刘知昊";
            _uvm.CompanyName = "ecnu";
            Console.WriteLine("okk");
            MessageBox.Show("123");
        }
    }
}
