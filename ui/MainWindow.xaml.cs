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
        private int tabId = 1; // 在新建模型选项卡时用于区分的自增数字

        public MainWindow()
        {
            InitializeComponent();
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

        }

        //[菜单]文件 > 退出
        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ----------------------------------------------------------------

        //[双击]模型选项卡的标签
        private void TabItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //移除选项卡
            TabItem s = (TabItem)sender;
            mainTabControl.Items.Remove(s);
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
            //创建TabItem并包含新的模型控件
            CloseableTabItem tabItem = new CloseableTabItem
            {
                Title = "模型" + (this.tabId++).ToString(),
                Content = new ModelFuncPanel()
            };
            mainTabControl.Items.Add(tabItem);
            //每造一个新窗口便默认突出显示为活动窗口
            tabItem.Focus();
        }

        //todo 放到模型控件里面去做
        //菜单 > 模型 > 添加状态机(State Machine)
        //菜单 > 模型 > 添加自定义类型(User Type)
        //菜单 > 模型 > 添加公理(Axiom)

    }
}
