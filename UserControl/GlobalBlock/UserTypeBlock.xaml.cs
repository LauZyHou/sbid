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
            setUserTypeName();
            increaseGlobalUserType();
        }

        public UserTypeBlock(int userTypeId)
        {
            InitializeComponent();
            this.userTypeId = userTypeId;
            userType = new UserType(userTypeId);
            userTypeAddHelper = new UserTypeAddHelper(userTypeId, this, this.userType);
            setUserTypeName();
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
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
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
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            globalUserTypeId--;
            Canvas canvas = contentControl.Parent as Canvas;
            canvas.Children.Remove(contentControl);
        }
        private void setUserTypeName()
        {
            Label label = this.FindName("userTypeLable") as Label;
            label.Content = "UserType " + this.userTypeId;
        }
    }
}
