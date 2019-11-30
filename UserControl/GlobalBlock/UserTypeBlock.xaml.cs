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
    /// UserTypeBlock.xaml 的交互逻辑
    /// </summary>
    public partial class UserTypeBlock : System.Windows.Controls.UserControl
    {
        public UserTypeBlock()
        {
            InitializeComponent();
        }

        //【右键菜单】编辑
        private void Menu_Click_Edit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("edit");
        }

        //【右键菜单】删除
        private void Menu_Click_Delete(object sender, RoutedEventArgs e)
        {
            Canvas canvas = contentControl.Parent as Canvas;
            canvas.Children.Remove(contentControl);
        }
    }
}
