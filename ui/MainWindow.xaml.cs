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
using sbid.UserControl;

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
            /*
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
            */
            TabItem ti = new TabItem();
            ti.VerticalAlignment = VerticalAlignment.Stretch;
            ti.HorizontalAlignment = HorizontalAlignment.Stretch;
            ti.Header = "xx图" + (tabIndex + 1);
            TabControl1.Items.Add(ti);
            ti.Content = new GraphPanel();

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

        //双击大面板上的选项卡
        private void TabItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //关闭选项卡
            TabItem s = (TabItem)sender;
            TabControl1.Items.Remove(s);
        }

        //点击菜单[图]之下的菜单项时触发
        private void MenuItem_Click_Graph(object sender, RoutedEventArgs e)
        {
            MenuItem mit = (MenuItem)sender;
            if (mit != null)
            {
                bottomTextBlock.Text = "当前选择：" + mit.Header.ToString();
            }
        }

        //菜单 > 文件 > 新模型
        private void MenuItem_Click_NewModel(object sender, RoutedEventArgs e)
        {
            //新模型的选项卡，添加到总的选项卡容器
            TabItem ti = new TabItem()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "模型" + (tabIndex + 1)
            };
            TabControl1.Items.Add(ti);
            //选项卡的内容又是选项卡容器，在其中添加一个默认的概览选项卡
            TabControl tc = new TabControl()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            ti.Content = tc;
            TabItem overViewTab = new TabItem()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "概览"
            };
            tc.Items.Add(overViewTab);

            //每造一个新窗口便默认突出显示为新窗口
            TabControl1.SelectedIndex = tabIndex;
            tabIndex++;
            //双击每个选项卡触发的事件
            ti.MouseDoubleClick += TabItem_DoubleClick;
        }

        //菜单 > 模型 > 添加状态机(State Machine)
        private void MenuItem_Click_Add_State_Machine(object sender, RoutedEventArgs e)
        {
            //获取当前活动的Model选项卡下的TabControl
            TabItem tabItem = TabControl1.Items.GetItemAt(tabIndex - 1) as TabItem;
            TabControl tabControl = tabItem.Content as TabControl;
            //在其中添加状态机选项卡
            TabItem ti = new TabItem()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "状态机1"
            };
            //ti.MouseDoubleClick += ???
            tabControl.Items.Add(ti);
        }
        //菜单 > 模型 > 添加自定义类型(User Type)
        //菜单 > 模型 > 添加公理(Axiom)

    }
}
