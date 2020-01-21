using sbid.Model;
using sbid.ViewModel;
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
        private UserType2VM myUserTypeVM;

        public UserType2VM MyUserTypeVM { get => myUserTypeVM; set => myUserTypeVM = value; }

        public UserTypeWindow(UserType2VM _utvm)
        {
            InitializeComponent();
            this.myUserTypeVM = _utvm;
            this.Title += _utvm.UserType2.Name;
            // 有了这个xaml中才能binding到这里的public属性
            this.DataContext = this;
            // "int","bool"和所有UserType的Name显示在列表中
            AllTypesListBox_Attr.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            AttrListBox.ItemsSource = myUserTypeVM.UserType2.Attributes;

        }

        private void AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AttrListBox.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Attribute nowAttr = ((Attribute)AttrListBox.SelectedItem);
            // 设置左侧显示的变量名
            AttrParamIdtTextBox.Text = nowAttr.Identifier;
            // 设置左侧选中的类型
            AllTypesListBox_Attr.SelectedItem = AllTypesListBox_Attr.Items[ResourceManager.currentProtocol.AllTypes.IndexOf(nowAttr.Type)];
        }

        private void Button_Click_Attr_Add(object sender, RoutedEventArgs e)
        {
            if (AllTypesListBox_Attr.SelectedItem == null || AttrParamIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要选中 变量类型 并写入 变量名称");
                return;
            }
            // todo 判重
            string nowType = (string)AllTypesListBox_Attr.SelectedItems[0];
            string nowIdt = AttrParamIdtTextBox.Text;
            if (nowType.Equals(MyUserTypeVM.UserType2.Name))
            {
                MessageBox.Show("添加变量类型不能为自身");
                return;
            }
            // 添加
            MyUserTypeVM.UserType2.Attributes.Add(new Attribute(nowType, nowIdt));
        }

        private void Button_Click_Attr_Update(object sender, RoutedEventArgs e)
        {
            if (AllTypesListBox_Attr.SelectedItem == null || AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("变量类型 和 右侧Attribute 都要选中");
                return;
            }
            // todo 判重
            string nowType = (string)AllTypesListBox_Attr.SelectedItems[0];
            string nowIdt = AttrParamIdtTextBox.Text;
            if (nowType.Equals(MyUserTypeVM.UserType2.Name))
            {
                MessageBox.Show("更新变量类型不能为自身");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MyUserTypeVM.UserType2.Attributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MyUserTypeVM.UserType2.Attributes[idx] = new Attribute(nowType, nowIdt);
        }

        private void Button_Click_Attr_Delete(object sender, RoutedEventArgs e)
        {
            if (AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的Attribute");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MyUserTypeVM.UserType2.Attributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MyUserTypeVM.UserType2.Attributes.RemoveAt(idx);
        }
    }
}
