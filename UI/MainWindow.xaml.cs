﻿using System.Collections.Generic;
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
using sbid.Model;

namespace sbid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tabId = 1; // 在新建模型选项卡时用于区分的自增数字

        public MainWindow()
        {
            InitializeComponent();
            // 初始化全局资源
            InitializeResourceManager();
            // 下方提示条的引用存入全局资源
            ResourceManager.tipTextBlock = bottomTextBlock;
        }

        private void InitializeResourceManager()
        {
            // 将内置函数和加密算法写入全局资源
            Method enc = new Method("enc");
            enc.Parameters.Add(new Attribute("Msg", "m"));
            enc.Parameters.Add(new Attribute("Key", "k"));
            Method dec = new Method("dec");
            dec.Parameters.Add(new Attribute("Msg", "m"));
            dec.Parameters.Add(new Attribute("Key", "k"));
            ResourceManager.innerMethods.Add(enc);
            ResourceManager.innerMethods.Add(dec);
            ResourceManager.cryptoNames.Add("AES");
            ResourceManager.cryptoNames.Add("DES");
            ResourceManager.cryptoNames.Add("SHA256");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
        private void MenuItem_Click_NewProtocal(object sender, RoutedEventArgs e)
        {
            //创建新的模型面板
            //先创建模型面板内包含的全局面板，放到TabControl里
            TabControl modelTabControl = new TabControl();
            //modelTabControl.Items.Add(
            //    new CloseableTabItem()
            //    {
            //        Title = "全局",
            //        Content = new GlobalPanel().Content as Grid
            //    }
            //);
            //[!]新的全局面板，第二类实现
            modelTabControl.Items.Add(
                new CloseableTabItem()
                {
                    Title = "全局",
                    Content = new GlobalPanel2().Content as Grid
                }
            );
            // 使用协议名称创建协议的对象
            string protocalName = "协议" + (this.tabId++).ToString();
            // [todo] 在切换Protocal选项卡时修改当前引用的Protocal对象
            ResourceManager.currentProtocol = new Protocol(protocalName);
            ResourceManager.protocols.Add(ResourceManager.currentProtocol);
            // 再创建包含它的模型TabItem
            CloseableTabItem tabItem = new CloseableTabItem
            {
                Title = protocalName,
                Content = modelTabControl
            };
            //整个模型TabItem放到大的mainTabControl里面
            mainTabControl.Items.Add(tabItem);
            //每造一个新窗口便默认突出显示为活动窗口
            tabItem.Focus();
        }

        //菜单 > 模型 > 添加攻击树
        private void MenuItem_Click_AttackTree(object sender, RoutedEventArgs e)
        {
            add_new_panel("攻击树", new AttackTreePanel().Content as Grid);
        }

        //菜单 > 模型 > 添加计算树逻辑
        private void MenuItem_Click_CTL(object sender, RoutedEventArgs e)
        {
            add_new_panel("计算树逻辑", new CTLPanel().Content as Grid);
        }

        // 菜单 > 模型 > 添加顺序图
        private void MenuItem_Click_Sequence_Diagram(object sender, RoutedEventArgs e)
        {
            add_new_panel("顺序图", new Grid());
        }

        // 菜单 > 模型 > 添加拓扑图
        private void MenuItem_Click_Topo_Graph(object sender, RoutedEventArgs e)
        {
            add_new_panel("拓扑图", new Grid());
        }

        //---------------------以下仅是用于给上面调用的，抽象出来的方法---------------------

        //在当前模型下添加面板
        public void add_new_panel(string title, FrameworkElement frameworkElement)
        {
            //在这里使用传入的标题和内容面板
            CloseableTabItem tabItem = new CloseableTabItem
            {
                Title = title,
                Content = frameworkElement
            };
            //检索当前活动的Protocal面板CloseableTabItem
            CloseableTabItem currentModel_TabItem = null;
            foreach (CloseableTabItem c in mainTabControl.Items)
            {
                if (c.IsSelected)
                    currentModel_TabItem = c;
            }
            if (currentModel_TabItem == null)
                return;
            //将新的TabItem添加进这个活动的Model面板中
            TabControl currentModel_TabControl = currentModel_TabItem.Content as TabControl;
            currentModel_TabControl.Items.Add(tabItem);
            tabItem.Focus();
        }

        // [按钮]生成XML
        private void Button_Click_GenerateXML(object sender, RoutedEventArgs e)
        {
            const string filePath = "D:\\data\\test.xml";
            ResourceManager.Protocol2Xml(ResourceManager.currentProtocol, filePath);
            //Test_GenerateXML();
            MessageBox.Show("写入在" + filePath);
        }

        #region 测试

        private void Test_GenerateXML()
        {
            Protocol protocol = new Protocol("测试protocol");
            Process process1 = new Process();
            process1.Attributes.Add(new Attribute("int", "a1"));
            process1.Attributes.Add(new Attribute("bool", "a2"));
            protocol.processes.Add(process1);
            ResourceManager.Protocol2Xml(protocol, "D:\\data\\test.xml");
        }

        #endregion 测试
    }
}
