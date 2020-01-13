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
        #region 参数与属性

        private ProcessVM myProcessVM;
        // 用于绑定自定Method的ItemsSource的参数列表,当用户点击右侧的Method时,仅拷贝其内容到这里
        private ObservableCollection<Attribute> ownMethodAttrs = new ObservableCollection<Attribute>();
        // 用于绑定CommMethod的ItemsSource的参数列表,当用户点击右侧的Method时,仅拷贝其内容到这里
        private ObservableCollection<Attribute> commMethodAttrs = new ObservableCollection<Attribute>();
        // 永远指向自定Method的ItemsSource
        //private ObservableCollection<Attribute> ownMethodAttrs_IS = null;

        public ProcessVM MyProcessVM { get => myProcessVM; set => myProcessVM = value; }

        #endregion 参数与属性

        #region 构造

        // 在构造时必须将ViewModel传入,以作修改和显示
        public ProcessWindow(ProcessVM _pvm)
        {
            InitializeComponent();
            this.myProcessVM = _pvm;
            this.Title += _pvm.Process.Name;
            // 有了这个xaml中才能binding到这里的public属性
            this.DataContext = this;
            // "int","bool"和所有UserType的Name显示在列表中
            AllTypesListBox_Attr.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            OwnMethodRetTypeComboBox.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            OwnMethodParamTypeComboBox.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            CommMethodParamTypeComboBox.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            // 内置函数
            InnerMethodListBox.ItemsSource = ResourceManager.innerMethods;
            // 加密算法
            CryptoNameListBox.ItemsSource = ResourceManager.cryptoNames;
            // 指向自定Method的Attribute列表的ItemsSource
            //ownMethodAttrs_IS = ownMethodAttrs;
            OwnMethodAttributeListBox.ItemsSource = ownMethodAttrs;
            // 指向CommMethod的Attribute列表的ItemsSource
            CommMethodAttributeListBox.ItemsSource = commMethodAttrs;
        }

        #endregion 构造

        #region 条目改变选中的事件处理

        // Attribute右侧的条目改变选中
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

        // 内置Method右侧的条目改变选中
        private void MethodListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (MethodListBox1.SelectedItem == null)
                return;
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox1.SelectedItem);
            // todo 设置左侧的内置函数
            // 设置左侧的加密算法
            CryptoNameListBox.SelectedItem = CryptoNameListBox.Items[ResourceManager.cryptoNames.IndexOf(nowMethod.CryptoName)];
        }

        // 自定Method右侧的条目改变选中
        private void MethodListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (MethodListBox2.SelectedItem == null)
                return;
            // 获取当前选中的Method
            Method nowMethod = ((Method)MethodListBox2.SelectedItem);
            // 设置返回值类型
            OwnMethodRetTypeComboBox.SelectedItem = OwnMethodRetTypeComboBox.Items[ResourceManager.currentProtocol.AllTypes.IndexOf(nowMethod.ReturnType)];
            // 设置函数名称
            OwnMethodIdtTextBox.Text = nowMethod.Identifier;
            // 设置参数列表
            ownMethodAttrs = new ObservableCollection<Attribute>();
            foreach (Attribute attribute in nowMethod.Parameters)
            {
                ownMethodAttrs.Add(attribute);
            }
            OwnMethodAttributeListBox.ItemsSource = ownMethodAttrs;
        }

        // 自定Method左侧的参数列表改变选中
        private void OwnMethodAttributeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (OwnMethodAttributeListBox.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Attribute attribute = ((Attribute)OwnMethodAttributeListBox.SelectedItem);
            // 设置参数类型
            OwnMethodParamTypeComboBox.SelectedItem = OwnMethodParamTypeComboBox.Items[ResourceManager.currentProtocol.AllTypes.IndexOf(attribute.Type)];
            // 设置参数名
            OwnMethodParamNameTextBox.Text = attribute.Identifier;
        }

        // CommMethod右侧的条目改变选中
        private void CommMethodListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (CommMethodListBox.SelectedItem == null)
                return;
            // 获取当前选中的CommMethod
            CommMethod nowCommMethod = ((CommMethod)CommMethodListBox.SelectedItem);
            // 设置函数名称
            CommMethodIdtTextBox.Text = nowCommMethod.Identifier;
            // 设置参数列表
            commMethodAttrs = new ObservableCollection<Attribute>();
            foreach (Attribute attribute in nowCommMethod.Parameters)
            {
                commMethodAttrs.Add(attribute);
            }
            CommMethodAttributeListBox.ItemsSource = commMethodAttrs;
            // 设置输入输出
            if (nowCommMethod.InOut == "in")
            {
                InRadioButton.IsChecked = true;
            }
            else
            {
                OutRadioButton.IsChecked = true;
            }
        }

        // CommMethod左侧的参数列表改变选中
        private void CommMethodAttributeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (CommMethodAttributeListBox.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Attribute attribute = ((Attribute)CommMethodAttributeListBox.SelectedItem);
            // 设置参数类型
            CommMethodParamTypeComboBox.SelectedItem = CommMethodParamTypeComboBox.Items[ResourceManager.currentProtocol.AllTypes.IndexOf(attribute.Type)];
            // 设置参数名
            CommMethodParamNameTextBox.Text = attribute.Identifier;
        }

        #endregion 条目改变选中的事件处理

        #region 按钮控制

        //----------------------------------Attribute-----------------------------------------

        // [按钮]Attribute->添加
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
            // 添加
            MyProcessVM.Process.Attributes.Add(new Attribute(nowType, nowIdt));
        }

        // [按钮]Attribute->更新
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
            if (AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的Attribute");
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

        //----------------------------------内置Method-----------------------------------------

        // [按钮]内置Method->添加
        private void Button_Click_InnerMethod_Add(object sender, RoutedEventArgs e)
        {
            if (InnerMethodListBox.SelectedItem == null)
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
            if (InnerMethodListBox.SelectedItem == null || MethodListBox1.SelectedItem == null)
            {
                MessageBox.Show("需要选中 内置Method 和 要更新的Method");
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
            if (MethodListBox1.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的Method");
                return;
            }
            // 右侧Method的下标
            int rightIdx = MethodListBox1.SelectedIndex;
            // 删除
            MyProcessVM.Process.Methods.RemoveAt(rightIdx);
        }

        //----------------------------------自定Method-----------------------------------------

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
                  ResourceManager.currentProtocol.AllTypes[OwnMethodParamTypeComboBox.SelectedIndex], // 参数类型
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
                OwnMethodAttributeListBox.SelectedItem == null
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
                ResourceManager.currentProtocol.AllTypes[OwnMethodParamTypeComboBox.SelectedIndex], // 参数类型
                OwnMethodParamNameTextBox.Text // 参数名称
            );
        }

        // [按钮]自定Method->删除参数(Attribute)
        private void Button_Click_OwnMethod_DeleteAttr(object sender, RoutedEventArgs e)
        {
            if (OwnMethodAttributeListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的Attribute");
                return;
            }
            // 选中的Attribute的下标
            int attrIdx = OwnMethodAttributeListBox.SelectedIndex;
            // 删除
            ownMethodAttrs.RemoveAt(attrIdx);
        }

        // [按钮]自定Method->添加
        private void Button_Click_OwnMethod_Add(object sender, RoutedEventArgs e)
        {
            if (OwnMethodRetTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要添加的Method]的[返回值类型]");
                return;
            }
            if (OwnMethodIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要给出[要添加的Method]的[函数名]");
                return;
            }
            if (ownMethodAttrs.Count < 1)
            {
                MessageBox.Show("需要给出[要添加的Method]的[形参表]");
                return;
            }
            Method newMethod = new Method(OwnMethodIdtTextBox.Text);
            newMethod.ReturnType = (string)OwnMethodRetTypeComboBox.SelectedItem;
            foreach (Attribute a in ownMethodAttrs)
            {
                newMethod.Parameters.Add(a);
            }

            // todo 判重
            // 添加
            MyProcessVM.Process.Methods.Add(newMethod);
        }

        // [按钮]自定Method->更新
        private void Button_Click_OwnMethod_Update(object sender, RoutedEventArgs e)
        {
            if (OwnMethodRetTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要更新的Method]的[返回值类型]");
                return;
            }
            if (OwnMethodIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要给出[要更新的Method]的[函数名]");
                return;
            }
            if (ownMethodAttrs.Count < 1)
            {
                MessageBox.Show("需要给出[要更新的Method]的[形参表]");
                return;
            }
            if (MethodListBox2.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要更新的Method]");
                return;
            }
            Method newMethod = new Method(OwnMethodIdtTextBox.Text);
            newMethod.ReturnType = (string)OwnMethodRetTypeComboBox.SelectedItem;
            foreach (Attribute a in ownMethodAttrs)
            {
                newMethod.Parameters.Add(a);
            }
            // todo 判重
            // 选中的Method的下标
            int methodIdx = MethodListBox2.SelectedIndex;
            // 更新
            MyProcessVM.Process.Methods[methodIdx] = newMethod;
        }

        // [按钮]自定Method->删除
        private void Button_Click_OwnMethod_Delete(object sender, RoutedEventArgs e)
        {
            if (MethodListBox2.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要删除的Method]");
                return;
            }
            // 选中的Method的下标
            int methodIdx = MethodListBox2.SelectedIndex;
            // 删除
            MyProcessVM.Process.Methods.RemoveAt(methodIdx);
        }

        //----------------------------------CommMethod-----------------------------------------

        // [按钮]CommMethod->添加参数(Attribute)
        private void Button_Click_CommMethod_AddAttr(object sender, RoutedEventArgs e)
        {
            if (CommMethodParamTypeComboBox.SelectedItem == null || CommMethodParamNameTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要选中 参数类型 并写入 参数名称");
                return;
            }
            // todo 判重
            // 添加
            commMethodAttrs.Add(
                new Attribute(
                  (string)CommMethodParamTypeComboBox.SelectedItem, // 参数类型
                  CommMethodParamNameTextBox.Text // 参数名称
                )
             );
        }

        // [按钮]CommMethod->更新参数(Attribute)
        private void Button_Click_CommMethod_UpdateAttr(object sender, RoutedEventArgs e)
        {
            if (
                CommMethodParamTypeComboBox.SelectedItem == null ||
                CommMethodParamNameTextBox.Text.Length < 1 ||
                CommMethodAttributeListBox.SelectedItem == null
                )
            {
                MessageBox.Show("需要选中 参数类型 并写入 参数名称 并选中 要修改的Attribute");
                return;
            }
            // todo 判重
            // 选中的Attribute的下标
            int attrIdx = CommMethodAttributeListBox.SelectedIndex;
            // 更新
            commMethodAttrs[attrIdx] = new Attribute(
                (string)CommMethodParamTypeComboBox.SelectedItem, // 参数类型
                CommMethodParamNameTextBox.Text // 参数名称
            );
        }

        // [按钮]CommMethod->删除参数(Attribute)
        private void Button_Click_CommMethod_DeleteAttr(object sender, RoutedEventArgs e)
        {
            if (CommMethodAttributeListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的Attribute");
                return;
            }
            // 选中的Attribute的下标
            int attrIdx = CommMethodAttributeListBox.SelectedIndex;
            // 删除
            commMethodAttrs.RemoveAt(attrIdx);
        }

        // [按钮]CommMethod->添加
        private void Button_Click_CommMethod_Add(object sender, RoutedEventArgs e)
        {
            if (InRadioButton.IsChecked == false && OutRadioButton.IsChecked == false)
            {
                MessageBox.Show("需要选定[要添加的CommMethod]的[IN/OUT]");
                return;
            }
            if (CommMethodIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要给出[要添加的CommMethod]的[函数名]");
                return;
            }
            if (commMethodAttrs.Count < 1)
            {
                MessageBox.Show("需要给出[要添加的CommMethod]的[形参表]");
                return;
            }
            CommMethod newCommMethod = new CommMethod(CommMethodIdtTextBox.Text);
            newCommMethod.InOut = InRadioButton.IsChecked == true ? "in" : "out";
            foreach (Attribute a in commMethodAttrs)
            {
                newCommMethod.Parameters.Add(a);
            }

            // todo 判重
            // 添加
            MyProcessVM.Process.CommMethods.Add(newCommMethod);
        }

        // [按钮]CommMethod->更新
        private void Button_Click_CommMethod_Update(object sender, RoutedEventArgs e)
        {
            if (InRadioButton.IsChecked == false && OutRadioButton.IsChecked == false)
            {
                MessageBox.Show("需要选定[要更新的CommMethod]的[IN/OUT]");
                return;
            }
            if (CommMethodIdtTextBox.Text.Length < 1)
            {
                MessageBox.Show("需要给出[要更新的CommMethod]的[函数名]");
                return;
            }
            if (commMethodAttrs.Count < 1)
            {
                MessageBox.Show("需要给出[要更新的CommMethod]的[形参表]");
                return;
            }
            if (CommMethodListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要更新的CommMethod]");
                return;
            }
            CommMethod newCommMethod = new CommMethod(CommMethodIdtTextBox.Text);
            newCommMethod.InOut = InRadioButton.IsChecked == true ? "in" : "out";
            foreach (Attribute a in commMethodAttrs)
            {
                newCommMethod.Parameters.Add(a);
            }

            // todo 判重
            // 选中的Method的下标
            int commMethodIdx = CommMethodListBox.SelectedIndex;
            // 更新
            MyProcessVM.Process.CommMethods[commMethodIdx] = newCommMethod;
        }

        // [按钮]自定Method->删除
        private void Button_Click_CommMethod_Delete(object sender, RoutedEventArgs e)
        {
            if (CommMethodListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定[要删除的CommMethod]");
                return;
            }
            // 选中的CommMethod的下标
            int commMethodIdx = CommMethodListBox.SelectedIndex;
            // 删除
            MyProcessVM.Process.CommMethods.RemoveAt(commMethodIdx);
        }

        #endregion 按钮控制

    }
}
