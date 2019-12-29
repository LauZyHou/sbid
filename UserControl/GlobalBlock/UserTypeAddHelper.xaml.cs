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

namespace sbid.UserControl.GlobalBlock
{
    /// <summary>
    /// UserTypeAddHelper.xaml 的交互逻辑
    /// </summary>
    public partial class UserTypeAddHelper : System.Windows.Controls.UserControl
    {
        private UserTypeBlock userTypeBlock;
        private UserType userType;
        private int userTypeId = 0;
        private static Dictionary<String, Model.Type> hashTable = new Dictionary<string, Model.Type>();

        static UserTypeAddHelper()
        {
            hashTableInit();
        }
        public UserTypeAddHelper()
        {
            InitializeComponent();
            setBinding();
        }
        public UserTypeAddHelper(int userTypeId, UserTypeBlock userTypeBlock, UserType userType)
        {
            InitializeComponent();
            this.userTypeId = userTypeId;
            this.userTypeBlock = userTypeBlock;
            this.userType = userType;
            setBinding();
        }
        public int UserTypeId
        {
            get
            {
                return userTypeId;
            }
            set
            {
                userTypeId = value;
            }
        }
        private static void hashTableInit()
        {
            hashTable.Add("int", new Model.IntType());
            hashTable.Add("bool", new Model.BoolType());
            hashTable.Add("UserType", new Model.UserType());
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Canvas canvas = contentControl.Parent as Canvas;
            canvas.Children.Remove(contentControl);
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            TextBox textBox = this.FindName("varName") as TextBox;
            String varName = textBox.Text;
            ComboBox typeDropDownList = this.FindName("typeDropDownList") as ComboBox;
            String str = typeDropDownList.SelectedItem.ToString().Substring(38);
            //add actual class to backend 
            Model.Type toAddType = convertToType(str);
            if(!this.userType.addAttribute(new Model.Attribute(varName, toAddType), varName))
            {
                return;
            }
            //to the user
            str += " ";
            str += textBox.Text;
            ListBox listBox = this.userTypeBlock.FindName("attribueList") as ListBox;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = str;
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = textBlock;
            listBox.Items.Add(listBoxItem);
            MessageBox.Show("添加属性成功");
        }
        private Model.Type convertToType(String str)
        {
            //所有的逻辑判断：
            //  1 用一张全局哈希表存储对应的类型名和实际类型
            //  2 每次转换都先判断是否已经存储，若已经存储，则只需要读出，否则重新创建
            return hashTable.GetValueOrDefault(str);
        }
        private void setBinding()
        {
            this.label_TabTitle.SetBinding(Label.ContentProperty, new Binding("UserTypeId") {Source = this });
        }
    }
}
