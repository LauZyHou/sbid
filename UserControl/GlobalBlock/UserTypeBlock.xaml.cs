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
using sbid.UserControl.GlobalBlock;
using sbid.Model;
namespace sbid.UserControl
{
    /// <summary>
    /// UserTypeBlock.xaml 的交互逻辑
    /// </summary>
    public partial class UserTypeBlock : System.Windows.Controls.UserControl
    {
        private static int globalUserTypeId = 1;
        private int userTypeId = 1;
        private UserTypeAddHelper userTypeAddHelper;
        private UserType userType;
        public UserTypeBlock()
        {
            InitializeComponent();
            this.userTypeId = globalUserTypeId;
            this.userType = new UserType(globalUserTypeId);
            userTypeAddHelper =  new UserTypeAddHelper(globalUserTypeId, this, this.userType);
            userType = new UserType(globalUserTypeId);
            setBinding();
            increaseGlobalUserType();
        }

        public UserTypeBlock(int userTypeId)
        {
            InitializeComponent();
            this.userTypeId = userTypeId;
            userType = new UserType(userTypeId);
            userTypeAddHelper = new UserTypeAddHelper(userTypeId, this, this.userType);
            setBinding();
            increaseGlobalUserType();
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
        public UserType UserType
        {
            get
            {
                return userType;
            }

            set
            {
                userType = value;
            }
        }
        public static int getGlobalUserTypeId()
        {
            return globalUserTypeId;
        }

        public static int increaseGlobalUserType()
        {
            return ++globalUserTypeId;
        }
        //【右键菜单】添加属性
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        { 
            Canvas canvas = contentControl.Parent as Canvas;
            userTypeAddHelper = new UserTypeAddHelper(this.userTypeId, this, this.userType);
            ContentControl toAdd = userTypeAddHelper.Content as ContentControl;
            userTypeAddHelper.SetValue(ContentPresenter.ContentProperty, null);
            //设置附加属性
            Canvas.SetLeft(toAdd, 50);
            Canvas.SetTop(toAdd, 100);
            //添加到Canvas
            canvas.Children.Add(toAdd);
        }
        //【右键菜单】删除
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            globalUserTypeId--;
            Canvas canvas = contentControl.Parent as Canvas;
            canvas.Children.Remove(contentControl);
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ListBox listBox = this.FindName("attribueList") as ListBox;
            if(listBox.SelectedItem == null)
            {
                MessageBox.Show("要删除的属性不能为空！");
                return;
            }
            ListBoxItem listBoxItem = listBox.SelectedItem as ListBoxItem;
            TextBlock textBlock = (TextBlock)listBoxItem.Content;
            String fullStr = textBlock.Text; //这里就是因为每一条数据包括类型和变量名，所以要拆分
            String[] str = fullStr.Split(" ");
            listBox.Items.RemoveAt(listBox.SelectedIndex); //将属性从前端删掉
            if (!this.userType.deleteAttribute(str[1]))  //将属性从后端userType map中删除
            {
                return;
            }
            MessageBox.Show("已为您删除选中属性: " + textBlock.Text);
        }
        private void setBinding()
        {
            this.userTypeLable.SetBinding(Label.ContentProperty, new Binding("UserTypeId") { Source= this });
        }
    }
}
