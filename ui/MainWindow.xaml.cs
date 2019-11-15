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
        private int tabIndex = 0; // 在新建tab(实际上是新建图)时用于区分默认的新建名称

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

        //[菜单]图 > SysML图 > 测试用
        private void MenuItem_Click_Test(object sender, RoutedEventArgs e)
        {
            //创建新的选项卡
            TabItem ti = new TabItem();
            ti.Header = "xx图" + (tabIndex + 1);
            TabControl1.Items.Add(ti);
            //为新选项卡加入有背景色的Grid
            Color color = (Color)ColorConverter.ConvertFromString("LightBlue");
            SolidColorBrush brush = new SolidColorBrush(color);
            Grid gd = new Grid();
            gd.Background = brush;
            ti.Content = gd;

            //每造一个新窗口便默认突出显示为新窗口
            TabControl1.SelectedIndex = tabIndex + 1;
            tabIndex++;
            //双击每个选项卡触发的事件
            ti.MouseDoubleClick += TabItem_DoubleClick;
        }

        //[菜单]文件 > 退出
        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ----------------------------------------------------------------

        //双击选项卡
        private void TabItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //关闭选项卡
            TabItem s = (TabItem)sender;
            TabControl1.Items.Remove(s);
        }
    }
}
