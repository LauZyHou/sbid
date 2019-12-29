using sbid.Model;
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
        //private int processId = 1;
        //private int tabNum = 0;
        public GlobalPanel()
        {
            InitializeComponent();
        }

        //[按钮]添加进程模板
        private void Button_Click_Process(object sender, RoutedEventArgs e)
        {
            //fixme
            //创建图形
            ContentControl contentControl = new ContentControl()
            {
                Width = 120,
                Height = 120,
                Template = FindResource("DesignerItemTemplate") as ControlTemplate,
                Content = new Ellipse()
                {
                    Fill = Brushes.Red,
                    IsHitTestVisible = false
                }
            };
            //设置附加属性
            Canvas.SetLeft(contentControl, 100);
            Canvas.SetTop(contentControl, 50);
            //添加到Canvas
            mainCanvas.Children.Add(contentControl);
        }

        //[按钮]添加自定义类型
        private void Button_Click_UserType(object sender, RoutedEventArgs e)
        {
            int globalUserTypeId = UserTypeBlock.getGlobalUserTypeId();
            UserTypeBlock userTypeBlock = new UserTypeBlock(globalUserTypeId);
            UserType userType = new UserType(globalUserTypeId);
            ContentControl contentControl = userTypeBlock.Content as ContentControl;
            userTypeBlock.SetValue(ContentPresenter.ContentProperty, null);
            //设置附加属性
            Canvas.SetLeft(contentControl, 5);
            Canvas.SetTop(contentControl, 50);
            //添加到Canvas
            mainCanvas.Children.Add(contentControl);
        }
    }
}
