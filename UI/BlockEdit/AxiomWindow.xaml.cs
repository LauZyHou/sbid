using sbid.ViewModel;
using System;
using System.Collections.Generic;
using sbid.Resources;
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
using System.Collections.ObjectModel;

namespace sbid.UI
{
    /// <summary>
    /// AxiomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AxiomWindow : Window
    {
        private ObservableCollection<Model.Attribute> ownMethodAttrs = new ObservableCollection<Model.Attribute>();
        private AxiomVM axiomVM;
        public AxiomVM AxiomVM
        {
            get
            {
                return axiomVM;
            }
            set
            {
                axiomVM = value;
            }
        }
        public AxiomWindow()
        {
            InitializeComponent();
        }
        public AxiomWindow(string suffixName){
            InitializeComponent();
            this.Title += suffixName;
        }
        public AxiomWindow(AxiomVM _utm)
        {
            InitializeComponent();
            this.axiomVM = _utm;
            this.Title = "编辑 " + _utm.Axiom.Name + "窗口";
            // 有了这个xaml中才能binding到这里的public属性
            this.DataContext = this;
            OwnMethodRetTypeComboBox.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            OwnMethodParamTypeComboBox.ItemsSource = ResourceManager.currentProtocol.AllTypes;
            Axiom_AttrListBox.ItemsSource = AxiomVM.Axiom.Ax;
            OwnMethodAttributeListBox.ItemsSource = ownMethodAttrs;
            //todo method
        }
        private void Axiom_AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (Axiom_AttrListBox.SelectedItem == null)
                return;
            // 获取当前选中的Axiom
            string nowAxiom = Axiom_AttrListBox.SelectedItem.ToString();
            // 设置左侧显示的变量名
            Axiom_TextBox.Text = nowAxiom;
        }
        private void Button_Click_Axiom_Add(object sender, RoutedEventArgs e)
        {
            if (Axiom_TextBox.Text.Length < 1)
            {
                MessageBox.Show("需要写入 公理");
                return;
            }
            // 添加
            if (AxiomVM.Axiom.Ax.Contains(Axiom_TextBox.Text))
            {
                MessageBox.Show("该公理已经存在");
                return;
            }
            AxiomVM.Axiom.Ax.Add(Axiom_TextBox.Text);
        }
        private void Button_Click_Axiom_Update(object sender, RoutedEventArgs e)
        {
            if (Axiom_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("请选中你要更新的公理");
                return;
            }
            // 右侧选中的CTL的下标
            string nowText = Axiom_TextBox.Text;
            int idx = Axiom_AttrListBox.SelectedIndex;
            if (idx >= AxiomVM.Axiom.Ax.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            AxiomVM.Axiom.Ax[idx] = nowText;
        }
        private void Button_Click_Axiom_Delete(object sender, RoutedEventArgs e)
        {
            if (Axiom_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的公理");
                return;
            }
            // 右侧选中的公理的下标
            int idx = Axiom_AttrListBox.SelectedIndex;
            if (idx >= AxiomVM.Axiom.Ax.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            AxiomVM.Axiom.Ax.RemoveAt(idx);
        }
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
            ownMethodAttrs = new ObservableCollection<Model.Attribute>();
            foreach (Model.Attribute attribute in nowMethod.Parameters)
            {
                ownMethodAttrs.Add(attribute);
            }
            OwnMethodAttributeListBox.ItemsSource = ownMethodAttrs;
        }

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
                new Model.Attribute(
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
            ownMethodAttrs[attrIdx] = new Model.Attribute(
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
        private void OwnMethodAttributeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (OwnMethodAttributeListBox.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Model.Attribute attribute = ((Model.Attribute)OwnMethodAttributeListBox.SelectedItem);
            // 设置参数类型
            OwnMethodParamTypeComboBox.SelectedItem = OwnMethodParamTypeComboBox.Items[ResourceManager.currentProtocol.AllTypes.IndexOf(attribute.Type)];
            // 设置参数名
            OwnMethodParamNameTextBox.Text = attribute.Identifier;
        }
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
            foreach (Model.Attribute a in ownMethodAttrs)
            {
                newMethod.Parameters.Add(a);
            }

            // todo 判重
            // 添加
            AxiomVM.Axiom.Methods.Add(newMethod);
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
            foreach (Model.Attribute a in ownMethodAttrs)
            {
                newMethod.Parameters.Add(a);
            }
            // todo 判重
            // 选中的Method的下标
            int methodIdx = MethodListBox2.SelectedIndex;
            // 更新
            AxiomVM.Axiom.Methods[methodIdx] = newMethod;
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
            AxiomVM.Axiom.Methods.RemoveAt(methodIdx);
        }

        }
    }
