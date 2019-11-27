using System;
using System.Collections.Generic;
using System.Text;
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

namespace sbid.UserControl
{
    /// <summary>
    /// ModelFuncPanel.xaml 的交互逻辑
    /// 不应TabItem拿出来设计的，这里就是基于这个考虑，把TabControl里的内容抽成UserControl
    /// </summary>
    public partial class ModelFuncPanel : System.Windows.Controls.UserControl
    {
        private int processId = 1;
        //private int tabNum = 0;


        public ModelFuncPanel()
        {
            InitializeComponent();
        }

        //[按钮]添加进程模板
        private void Button_Click_Process(object sender, RoutedEventArgs e)
        {
            CloseableTabItem closeableTabItem = new CloseableTabItem()
            {
                Title = "进程模板" + (this.processId++).ToString()
            };
            tabControl.Items.Add(closeableTabItem);
            closeableTabItem.Focus();
        }
    }
}
