using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ActionsEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ActionsEditWindow : Window
    {
        // 将传入的Actions写入,并绑定到这个窗体中的ListBox上
        private ObservableCollection<string> actions = null;

        public ActionsEditWindow(ObservableCollection<string> _actions)
        {
            InitializeComponent();
            this.actions = _actions;
            this.ActionsListBox.ItemsSource = this.actions;
        }

        #region 按钮控制

        // [按钮]添加Action
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (ActionTextBox.Text.Length < 1)
            {
                MessageBox.Show("输入的Aciton不能为空");
                return;
            }
            this.actions.Add(ActionTextBox.Text);
        }

        // [按钮]更新Action
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定 要更新的Action");
                return;
            }
            if (ActionTextBox.Text.Length < 1)
            {
                MessageBox.Show("输入的Aciton不能为空");
                return;
            }
            // 要更新的Action在列表中的下标 
            int idx = ActionsListBox.SelectedIndex;
            // 更新
            this.actions[idx] = ActionTextBox.Text;
        }

        // [按钮]删除Action
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定 要删除的Action");
                return;
            }
            // 要删除的Action在列表中的下标
            int idx = ActionsListBox.SelectedIndex;
            // 删除
            this.actions.RemoveAt(idx);
        }

        #endregion 按钮控制
    }
}
