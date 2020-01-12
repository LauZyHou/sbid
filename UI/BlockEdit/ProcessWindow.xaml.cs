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
using sbid.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace sbid.UI
{
    /// <summary>
    /// ProcessWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessWindow : Window
    {
        private ProcessVM myProcessVM;

        public ProcessVM MyProcessVM { get => myProcessVM; set => myProcessVM = value; }

        // 在构造时必须将ViewModel传入,以作修改和显示
        public ProcessWindow(ProcessVM _pvm)
        {
            InitializeComponent();
            this.myProcessVM = _pvm;
            this.Title += _pvm.Process.Name;
            // 有了这个xaml中才能binding到这里的public属性
            this.DataContext = this;
            // "int","bool"和所有UserType的Name显示在列表中
            AllTypesListBox_Attr.ItemsSource = ResourceManager.currentProtocal.AllTypes;
            // 内置函数
            InnerMethodListBox.ItemsSource = ResourceManager.innerMethods;
            // 加密算法
            CryptoNameListBox.ItemsSource = ResourceManager.cryptoNames;
        }

        // Attribute右侧的条目改变选中
        private void AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 获取当前选中的Attribute
            Attribute nowAttr = ((Attribute)AttrListBox.SelectedItems[0]);
            AttrParamIdtTextBox.Text = nowAttr.Identifier;
        }

        // 内置Method右侧的条目改变选中
        private void MethodListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox1.SelectedItems[0]);
            // todo
        }

        // 自定Method右侧的条目改变选中
        private void MethodListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox2.SelectedItems[0]);
            // todo
        }

        // CommMethod右侧的条目改变选中
        private void CommMethodListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 获取当前选中的CommMethod
            CommMethod nowCommMethod = ((CommMethod)CommMethodListBox.SelectedItems[0]);
            CommMethodIdtTextBox.Text = nowCommMethod.Identifier;
            CommMethodParamListBox.ItemsSource = nowCommMethod.Parameters;
            if (nowCommMethod.InOut == "in")
            {
                InRadioButton.IsChecked = true;
            }
            else
            {
                OutRadioButton.IsChecked = true;
            }
        }

        #region 按钮控制


        // [按钮]Attribute->添加
        private void Button_Click_Attr_Add(object sender, RoutedEventArgs e)
        {
            string nowType = (string)AllTypesListBox_Attr.SelectedItems[0];
            string nowIdt = AttrParamIdtTextBox.Text;
            MyProcessVM.Process.Attributes.Add(new Attribute(nowType, nowIdt));
        }

        #endregion 按钮控制
    }
}
