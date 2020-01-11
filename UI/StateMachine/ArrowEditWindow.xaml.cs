using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using sbid.ViewModel;

namespace sbid.UI
{
    /// <summary>
    /// ArrowEditWindow.xaml 的交互逻辑,用于对TransitionVM进行编辑的窗口
    /// </summary>
    public partial class ArrowEditWindow : Window
    {
        // 转移关系,不需要对外暴露,只要自己修改掉就可以
        private TransitionVM transition = null;
        // 拷贝的Actions,作为listBox的ItemsSource,用于实现保存时再写回
        // 这里用ObservableCollection因为它已经实现了INotify...,可以立刻显示变化
        private ObservableCollection<string> _actions = null;


        public ArrowEditWindow(TransitionVM _t)
        {
            InitializeComponent();
            // 传入的TransitionVM写入
            this.transition = _t;
            // Guard条件写入文本框
            this.guradText.Text = _t.Guard;
            // 拷贝到临时的_actions中
            _actions = new ObservableCollection<string>(_t.Actions);
            // 临时的_actions作为listBox的ItemsSource
            this.listBox.ItemsSource = this._actions;
        }

        // 【按钮】添加新的Action
        private void Button_Click_AddAction(object sender, RoutedEventArgs e)
        {
            if (this.actionText.Text == null || this.actionText.Text.Length == 0)
            {
                MessageBox.Show("[注意]添加的Action不能为空");
                return;
            }
            // 这里不能同步,就添加了两次,fixme
            this._actions.Add(this.actionText.Text);
        }

        // 【按钮】删除选中的Action
        private void Button_Click_DelAction(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItems.Count == 0)
                return;
            _actions.Remove((string)listBox.SelectedItems[0]);
        }

        //【按钮】全部保存
        private void Button_Click_AllSave(object sender, RoutedEventArgs e)
        {
            // Gurad
            transition.Guard = this.guradText.Text;
            // Actions
            this.transition.Actions = _actions.ToList<string>();
        }
    }
}
