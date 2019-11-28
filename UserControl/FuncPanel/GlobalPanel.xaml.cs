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

namespace sbid.UserControl
{
    /// <summary>
    /// GlobalPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalPanel : System.Windows.Controls.UserControl
    {
        private int processId = 1;
        //private int tabNum = 0;


        public GlobalPanel()
        {
            InitializeComponent();
        }

        //[按钮]添加进程模板
        private void Button_Click_Process(object sender, RoutedEventArgs e)
        {
            //todo
        }
    }
}
