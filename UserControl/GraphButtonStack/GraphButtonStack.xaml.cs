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
    /// GraphButtonStack.xaml 的交互逻辑
    /// </summary>
    public partial class GraphButtonStack : System.Windows.Controls.UserControl
    {
        public GraphButtonStack()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //todo 1 让父窗体bottomTextBox显示内容
            Window father = Window.GetWindow(this);
            father.Title = "123";
        }
    }
}
