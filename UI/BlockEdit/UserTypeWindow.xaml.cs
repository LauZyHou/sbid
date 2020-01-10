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
using System.Windows.Shapes;

namespace sbid.UI
{
    /// <summary>
    /// UserTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserTypeWindow : Window
    {
        public UserTypeWindow()
        {
            InitializeComponent();
        }
        public UserTypeWindow(string suffixName)
        {
            InitializeComponent();
            this.Title += suffixName;
        }

        private void button_add_click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = this.FindName("varName") as TextBox;
            String varName = textBox.Text;
            ComboBox typeDropDownList = this.FindName("typeDropDownList") as ComboBox;
            String str = typeDropDownList.SelectedItem.ToString().Substring(38);
            ListBox listBoxInUserTypeWindow = this.FindName("attributeList") as ListBox;
            listBoxInUserTypeWindow.Items.Add(str + " " + varName);


        }

        private void button_update_click(object sender, RoutedEventArgs e)
        {

        }

        private void button_delete_click(object sender, RoutedEventArgs e)
        {

        }

        private void attributeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
