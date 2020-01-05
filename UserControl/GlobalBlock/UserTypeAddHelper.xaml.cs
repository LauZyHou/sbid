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
            setUserTypeAddHelperName();
        }
        public UserTypeAddHelper(int userTypeId, UserTypeBlock userTypeBlock, UserType userType)
        {
            InitializeComponent();
            this.userTypeId = userTypeId;
            this.userTypeBlock = userTypeBlock;
            this.userType = userType;
            setUserTypeAddHelperName();
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
            ListBox listBoxInUserTypeBlock = this.userTypeBlock.FindName("attributeList") as ListBox;
            ListBox listBoxInUserTypeAddHelper = this.FindName("attributeList") as ListBox;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = str;
            ListBoxItem listBoxItem1 = new ListBoxItem();
            ListBoxItem listBoxItem2 = new ListBoxItem();
            listBoxItem1.Content = textBlock;
            listBoxItem2.Content = textBlock;
            listBoxInUserTypeAddHelper.Items.Add(listBoxItem2);
            listBoxInUserTypeBlock.Items.Add(listBoxItem1);         
            MessageBox.Show("添加属性成功");
        }
        private Model.Type convertToType(String str)
        {
            //所有的逻辑判断：
            //  1 用一张全局哈希表存储对应的类型名和实际类型
            //  2 每次转换都先判断是否已经存储，若已经存储，则只需要读出，否则重新创建
            return hashTable.GetValueOrDefault(str);
        }
        private void setUserTypeAddHelperName()
        {
            Label label = this.FindName("label_TabTitle") as Label;
            label.Content = "编辑" + "UserType " + this.userTypeId ;
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ListBox listBoxInUserTypeHelper = this.FindName("attributeList") as ListBox;
            ListBox listBoxInUserTypeBlock = this.userTypeBlock.FindName("attributeList") as ListBox;
            if (listBoxInUserTypeHelper.SelectedItem == null)
            {
                MessageBox.Show("要删除的属性不能为空！");
                return;
            }
            int toBeDeletedIndex = listBoxInUserTypeHelper.SelectedIndex;
            ListBoxItem listBoxItem = listBoxInUserTypeHelper.SelectedItem as ListBoxItem;
            TextBlock textBlock = (TextBlock)listBoxItem.Content;
            String fullStr = textBlock.Text; //这里就是因为每一条数据包括类型和变量名，所以要拆分
            String[] str = fullStr.Split(" ");
            if (!this.userType.deleteAttribute(str[1]))  //将属性从后端userType map中删除
            {
                return;
            }
            listBoxInUserTypeHelper.Items.RemoveAt(toBeDeletedIndex); //将属性从UserTypeHelper删掉
            listBoxInUserTypeBlock.Items.RemoveAt(toBeDeletedIndex); //将属性从UserTypeBlock删掉
            MessageBox.Show("已为您删除选中属性: " + textBlock.Text);
        }
    }
}
