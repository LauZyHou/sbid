using System;
using System.Collections.Generic;
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
        private TransitionVM transition;
        //拷贝的Actions,作为listBox的ItemsSource,用于实现保存时再写回
        private List<string> _actions;


        public ArrowEditWindow(TransitionVM _t)
        {
            InitializeComponent();
            // 传入的TransitionVM写入
            this.transition = _t;
            // Guard条件写入文本框
            this.guradText.Text = _t.Guard;
            // 深拷贝到临时的_actions中
            this._actions = _t.Actions.Select(item => (string)item.Clone()).ToList();
            // 临时的_actions作为listBox的ItemsSource
            this.listBox.ItemsSource = _actions;
        }

        // 【按钮】添加Action
        private void Button_Click_AddAction(object sender, RoutedEventArgs e)
        {
            if(this.actionText.Text==null || this.actionText.Text.Length==0)
            {
                MessageBox.Show("[注意]添加的Action不能为空");
                return;
            }
            // 这里不能同步,就添加了两次,fixme
            this._actions.Add(this.actionText.Text);
        }

        private void Button_Click_DelAction(object sender, RoutedEventArgs e)
        {

        }

        //【按钮】全部保存
        private void Button_Click_AllSave(object sender, RoutedEventArgs e)
        {
            // Gurad
            transition.Guard = this.guradText.Text;
            // Actions
            this.transition.Actions = _actions;
        }
    }
}
