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
    /// SafetyPropertyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SafetyPropertyWindow : Window
    {
        private SafetyPropertyVM mySafetyPropertyVM;

        public SafetyPropertyVM MySafetyPropertyVM { get => mySafetyPropertyVM; set => mySafetyPropertyVM = value; }

        public SafetyPropertyWindow(SafetyPropertyVM _spvm)
        {
            InitializeComponent();
            this.mySafetyPropertyVM = _spvm;
            this.Title += _spvm.SafetyProperty.Name;
            // 有了这个xaml中才能binding到这里的public属性
            this.DataContext = this;
            IVAR_AttrListBox.ItemsSource = mySafetyPropertyVM.SafetyProperty.Invariants;
            CTL_AttrListBox.ItemsSource = mySafetyPropertyVM.SafetyProperty.CTLs;
        }

        private void IVAR_AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (IVAR_AttrListBox.SelectedItem == null)
                return;
            // 获取当前选中的IVAR
            string nowIVAR = IVAR_AttrListBox.SelectedItem.ToString();
            // 设置左侧显示的变量名
            IVAR_TextBox.Text = nowIVAR;
        }

        private void CTL_AttrListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (CTL_AttrListBox.SelectedItem == null)
                return;
            // 获取当前选中的IVAR
            string nowCTL = CTL_AttrListBox.SelectedItem.ToString();
            // 设置左侧显示的变量名
            CTL_TextBox.Text = nowCTL;
        }

        private void Button_Click_IVAR_Add(object sender, RoutedEventArgs e)
        {
            if (IVAR_TextBox.Text.Length < 1)
            {
                MessageBox.Show("需要写入 变量名称");
                return;
            }
            // 添加
            if (MySafetyPropertyVM.SafetyProperty.Invariants.Contains(IVAR_TextBox.Text))
            {
                MessageBox.Show("该变量已经存在");
                return;
            }
            MySafetyPropertyVM.SafetyProperty.Invariants.Add(IVAR_TextBox.Text);
        }

        private void Button_Click_CTL_Add(object sender, RoutedEventArgs e)
        {
            if (CTL_TextBox.Text.Length < 1)
            {
                MessageBox.Show("需要写入 变量名称");
                return;
            }
            // 添加
            if (MySafetyPropertyVM.SafetyProperty.CTLs.Contains(IVAR_TextBox.Text))
            {
                MessageBox.Show("该变量已经存在");
                return;
            }
            MySafetyPropertyVM.SafetyProperty.CTLs.Add(CTL_TextBox.Text);
        }

        private void Button_Click_IVAR_Delete(object sender, RoutedEventArgs e)
        {
            if (IVAR_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的IVAR");
                return;
            }
            // 右侧选中的IVAR的下标
            int idx = IVAR_AttrListBox.SelectedIndex;
            if (idx >= MySafetyPropertyVM.SafetyProperty.Invariants.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySafetyPropertyVM.SafetyProperty.Invariants.RemoveAt(idx);
        }

        private void Button_Click_CTL_Delete(object sender, RoutedEventArgs e)
        {
            if (CTL_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的CTL");
                return;
            }
            // 右侧选中的CTL的下标
            int idx = CTL_AttrListBox.SelectedIndex;
            if (idx >= MySafetyPropertyVM.SafetyProperty.CTLs.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySafetyPropertyVM.SafetyProperty.CTLs.RemoveAt(idx);
        }

        private void Button_Click_IVAR_Update(object sender, RoutedEventArgs e)
        {
            if (IVAR_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("请选中你要更新的IVAR");
                return;
            }
            // 右侧选中的IVAR的下标
            string nowText = IVAR_TextBox.Text;
            int idx = IVAR_AttrListBox.SelectedIndex;
            if (idx >= MySafetyPropertyVM.SafetyProperty.Invariants.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySafetyPropertyVM.SafetyProperty.Invariants[idx] = nowText;
        }

        private void Button_Click_CTL_Update(object sender, RoutedEventArgs e)
        {
            if (CTL_AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("请选中你要更新的CTL");
                return;
            }
            // 右侧选中的CTL的下标
            string nowText = CTL_TextBox.Text;
            int idx = CTL_AttrListBox.SelectedIndex;
            if (idx >= MySafetyPropertyVM.SafetyProperty.CTLs.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySafetyPropertyVM.SafetyProperty.CTLs[idx] = nowText;
        }
    }
}
