using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using sbid.UserControl;

namespace sbid.Resources
{
    public partial class DesignerItem : ResourceDictionary
    {
        //[按钮]测试
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }
    }
}
