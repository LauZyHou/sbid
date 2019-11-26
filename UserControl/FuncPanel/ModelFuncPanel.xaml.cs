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
    /// ModelFuncPanel.xaml 的交互逻辑
    /// 不应TabItem拿出来设计的，这里就是基于这个考虑，把TabControl里的内容抽成UserControl
    /// </summary>
    public partial class ModelFuncPanel : System.Windows.Controls.UserControl
    {
        public ModelFuncPanel()
        {
            InitializeComponent();
        }
    }
}
