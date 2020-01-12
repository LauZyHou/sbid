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
        // 用于绑定自定Method的ItemsSource的参数列表(用户清空后切换到使用此列表为源)
        private ObservableCollection<Attribute> ownMethodAttrs = new ObservableCollection<Attribute>();
        // 永远指向自定Method的ItemsSource
        private ObservableCollection<Attribute> ownMethodAttrs_IS = null;

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
            OwnMethodRetTypeComboBox.ItemsSource = ResourceManager.currentProtocal.AllTypes;
            OwnMethodParamTypeComboBox.ItemsSource = ResourceManager.currentProtocal.AllTypes;
            // 内置函数
            InnerMethodListBox.ItemsSource = ResourceManager.innerMethods;
            // 加密算法
            CryptoNameListBox.ItemsSource = ResourceManager.cryptoNames;
            // 指向自定Method的ItemsSource
            ownMethodAttrs_IS = ownMethodAttrs;
            OwnMethodAttributeListBox.ItemsSource = ownMethodAttrs_IS;
        }

        // Attribute右侧的条目改变选中
        private void AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AttrListBox.SelectedItems.Count < 1)
                return;
            // 获取当前选中的Attribute
            Attribute nowAttr = ((Attribute)AttrListBox.SelectedItems[0]);
            AttrParamIdtTextBox.Text = nowAttr.Identifier;
        }

        // 内置Method右侧的条目改变选中
        private void MethodListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (MethodListBox1.SelectedItems.Count < 1)
                return;
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox1.SelectedItems[0]);
            // todo
        }

        // 自定Method右侧的条目改变选中
        private void MethodListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (MethodListBox2.SelectedItems.Count < 1)
                return;
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox2.SelectedItems[0]);
            // todo
        }

        // CommMethod右侧的条目改变选中
        private void CommMethodListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (CommMethodListBox.SelectedItems.Count < 1)
                return;
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
            if (AllTypesListBox_Attr.SelectedItems.Count < 1 || AttrParamIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要选中 变量类型 并写入 变量名称");
                return;
            }
            // todo 判重
            string nowType = (string)AllTypesListBox_Attr.SelectedItems[0];
            string nowIdt = AttrParamIdtTextBox.Text;
            // 添加
            MyProcessVM.Process.Attributes.Add(new Attribute(nowType, nowIdt));
        }

        // [按钮]Attribute->更新
        private void Button_Click_Attr_Update(object sender, RoutedEventArgs e)
        {
            if (AllTypesListBox_Attr.SelectedItems.Count < 1 || AttrListBox.SelectedItems.Count < 1)
            {
                MessageBox.Show("变量类型 和 右侧Attribute 都要选中");
                return;
            }
            // todo 判重
            string nowType = (string)AllTypesListBox_Attr.SelectedItems[0];
            string nowIdt = AttrParamIdtTextBox.Text;
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MyProcessVM.Process.Attributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MyProcessVM.Process.Attributes[idx] = new Attribute(nowType, nowIdt);
        }

        // [按钮]Attribute->删除
        private void Button_Click_Attr_Delete(object sender, RoutedEventArgs e)
        {
            if (AttrListBox.SelectedItems.Count < 1)
            {
                MessageBox.Show("需要选中 右侧Attribute");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MyProcessVM.Process.Attributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MyProcessVM.Process.Attributes.RemoveAt(idx);
        }

        // [按钮]内置Method->添加
        private void Button_Click_InnerMethod_Add(object sender, RoutedEventArgs e)
        {
            if (InnerMethodListBox.SelectedItems.Count < 1)
            {
                MessageBox.Show("需要选中 内置Method");
                return;
            }
            // todo 判重
            // 拷贝选中的内置的Method
            int idx = InnerMethodListBox.SelectedIndex;
            Method newMethod = sbid.Tools.TransExpV2<Method, Method>.Trans(
                ResourceManager.innerMethods[idx]
                );
            // 可选加密算法
            if (CryptoNameListBox.SelectedItems.Count > 0)
            {
                int crptoIndex = CryptoNameListBox.SelectedIndex;
                newMethod.CryptoName = ResourceManager.cryptoNames[crptoIndex];
            }
            // 添加
            MyProcessVM.Process.Methods.Add(newMethod);
        }

        // [按钮]内置Method->更新
        private void Button_Click_InnerMethod_Update(object sender, RoutedEventArgs e)
        {
            if (InnerMethodListBox.SelectedItems.Count < 1 || MethodListBox1.SelectedItems.Count < 1)
            {
                MessageBox.Show("需要选中 内置Method 和 右侧Method");
                return;
            }
            // todo 判重
            // 拷贝选中的内置的Method
            int idx = InnerMethodListBox.SelectedIndex;
            Method newMethod = sbid.Tools.TransExpV2<Method, Method>.Trans(
                ResourceManager.innerMethods[idx]
                );
            // 可选加密算法
            if (CryptoNameListBox.SelectedItems.Count > 0)
            {
                int crptoIndex = CryptoNameListBox.SelectedIndex;
                newMethod.CryptoName = ResourceManager.cryptoNames[crptoIndex];
            }
            // 右侧Method的下标
            int rightIdx = MethodListBox1.SelectedIndex;
            // 更新
            MyProcessVM.Process.Methods[rightIdx] = newMethod;
        }

        // [按钮]内置Method->删除
        private void Button_Click_InnerMethod_Delete(object sender, RoutedEventArgs e)
        {
            if (MethodListBox1.SelectedItems.Count < 1)
            {
                MessageBox.Show("需要选中 右侧Method");
                return;
            }
            // 右侧Method的下标
            int rightIdx = MethodListBox1.SelectedIndex;
            // 删除
            MyProcessVM.Process.Methods.RemoveAt(rightIdx);
        }

        // [按钮]自定Method->添加参数(Attribute)
        private void Button_Click_OwnMethod_AddAttr(object sender, RoutedEventArgs e)
        {
            if (OwnMethodParamTypeComboBox.SelectedItem == null || OwnMethodParamNameTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要选中 参数类型 并写入 参数名称");
                return;
            }
            // todo 判重
            // 添加
            ownMethodAttrs.Add(
                new Attribute(
                  ResourceManager.currentProtocal.AllTypes[OwnMethodParamTypeComboBox.SelectedIndex], // 参数类型
                  OwnMethodParamNameTextBox.Text // 参数名称
                )
             );
        }

        // [按钮]自定Method->更新参数(Attribute)
        private void Button_Click_OwnMethod_UpdateAttr(object sender, RoutedEventArgs e)
        {
            if (
                OwnMethodParamTypeComboBox.SelectedItem == null || 
                OwnMethodParamNameTextBox.Text.Length < 1 ||
                OwnMethodAttributeListBox.Items.Count < 1
                )
            {
                MessageBox.Show("需要选中 参数类型 并写入 参数名称 并选中 要修改的Attribute");
                return;
            }
            // todo 判重
            // 选中的Attribute的下标
            int attrIdx = OwnMethodAttributeListBox.SelectedIndex;
            // 更新
            ownMethodAttrs[attrIdx] = new Attribute(
                ResourceManager.currentProtocal.AllTypes[OwnMethodParamTypeComboBox.SelectedIndex], // 参数类型
                OwnMethodParamNameTextBox.Text // 参数名称
            );
        }

        // [按钮]自定Method->删除参数(Attribute)
        private void Button_Click_OwnMethod_DeleteAttr(object sender, RoutedEventArgs e)
        {
            if (OwnMethodAttributeListBox.Items.Count < 1)
            {
                MessageBox.Show("需要选中 要修改的Attribute");
                return;
            }
            // 选中的Attribute的下标
            int attrIdx = OwnMethodAttributeListBox.SelectedIndex;
            // 删除
            ownMethodAttrs.RemoveAt(attrIdx);
        }

        #endregion 按钮控制
    }
}
