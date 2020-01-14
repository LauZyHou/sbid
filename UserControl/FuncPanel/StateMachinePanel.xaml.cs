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
using sbid.ViewModel;

namespace sbid.UserControl
{
    /// <summary>
    /// StateMachinePanel.xaml 的交互逻辑
    /// </summary>
    public partial class StateMachinePanel : System.Windows.Controls.UserControl
    {
        private StateMachineVM stateMachineVM = null;

        // 集成StateMachine的ViewModel
        public StateMachineVM StateMachineVM { get => stateMachineVM; set => stateMachineVM = value; }

        // 在创建面板时需要传入状态机的VM,是新建的还是拿旧的由调用者决定,而不在这里创建状态机的VM
        public StateMachinePanel(StateMachineVM stateMachineVM)
        {
            InitializeComponent();
            this.stateMachineVM = stateMachineVM;
        }
    }
}
