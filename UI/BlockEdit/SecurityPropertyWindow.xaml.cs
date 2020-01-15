using sbid.Model;
using sbid.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace sbid.UI
{
    /// <summary>
    /// SecurityPropertyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SecurityPropertyWindow : Window
    {
        #region 参数与属性

        private SecurityPropertyVM mySecurityPropertyVM = null;
        private List<Process> processes = null;

        public SecurityPropertyVM MySecurityPropertyVM { get => mySecurityPropertyVM; set => mySecurityPropertyVM = value; }
        public List<Process> Processes { get => processes; set => processes = value; }

        #endregion 参数与属性

        #region 构造

        public SecurityPropertyWindow(SecurityPropertyVM _spvm)
        {
            InitializeComponent();
            this.mySecurityPropertyVM = _spvm;
            this.Title += _spvm.SecurityProperty.Name;
            this.DataContext = this;
            AllProcessListBox_Attr.ItemsSource = ResourceManager.currentProtocol.processes;
            AllProcessComboBox_Attr1.ItemsSource = ResourceManager.currentProtocol.processes;
            AllProcessComboBox_Attr2.ItemsSource = ResourceManager.currentProtocol.processes;
            ConfidentialListBox.ItemsSource = mySecurityPropertyVM.SecurityProperty.Confidentials;
            AuthenticityListBox.ItemsSource = mySecurityPropertyVM.SecurityProperty.Authenticities;
        }

        #endregion 构造

        #region 条目改变选中的事件处理


        // Confidential的进程改变选中
        private void AllProcessListBox_Attr_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessListBox_Attr.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessListBox_Attr.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (KeyValuePair<string, StateMachine> item in list)
            {
                AllStatesListBox_Attr.ItemsSource = item.Value.States;
            }
        }

        // Authenticity的进程1改变选中
        private void AllProcessComboBox_Attr1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessComboBox_Attr1.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessComboBox_Attr1.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (var item in list)
            {
                AllStatesComboBox_Attr1.ItemsSource = item.Value.States;
            }
        }

        // Authenticity的进程2名称改变选中
        private void AllProcessComboBox_Attr2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessComboBox_Attr2.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessComboBox_Attr2.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (var item in list)
            {
                AllStatesComboBox_Attr2.ItemsSource = item.Value.States;
            }
        }

        #endregion 条目改变选中的事件处理

        #region 按钮控制


        // [按钮]Confidential添加
        private void Button_Click_AddConfidential(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr.SelectedItem == null || AllStatesListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("需要选中 进程类型 和 状态机 ");
                return;
            }
            // todo 判重
            Process process = (Process)AllProcessListBox_Attr.SelectedItem;
            State state = (State)AllStatesListBox_Attr.SelectedItem;
            // 添加
            MySecurityPropertyVM.SecurityProperty.Confidentials.Add(new Confidential(process, state));
        }

        // [按钮]Confidential更新
        private void Button_Click_UpdateConfidential(object sender, RoutedEventArgs e)
        {
            if (AllStatesListBox_Attr.SelectedItem == null || ConfidentialListBox.SelectedItem == null || AllProcessListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("进程类型 、状态机状态 和 右侧性质 都要选中");
                return;
            }
            // todo 判重
            Process process = (Process)AllProcessListBox_Attr.SelectedItem;
            State state = (State)AllStatesListBox_Attr.SelectedItem;

            // 右侧选中的Attribute的下标
            int idx = ConfidentialListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Confidentials.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySecurityPropertyVM.SecurityProperty.Confidentials[idx] = new Confidential(process, state);
        }

        // [按钮]Confidential删除
        private void Button_Click_DeleteConfidential(object sender, RoutedEventArgs e)
        {
            if (ConfidentialListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的性质");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = ConfidentialListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Confidentials.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySecurityPropertyVM.SecurityProperty.Confidentials.RemoveAt(idx);
        }

        // [按钮]Authenticity添加
        private void Button_Click_AddAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AllProcessComboBox_Attr1.SelectedItem == null || AllStatesComboBox_Attr1.SelectedItem == null || AllProcessComboBox_Attr1.SelectedItem == null || AllStatesComboBox_Attr2.SelectedItem == null || AllProcessComboBox_Attr2.SelectedItem == null)
            {
                MessageBox.Show("需要选中 进程类型 和 状态机 ");
                return;
            }
            // todo 判重
            Process process1 = (Process)AllProcessComboBox_Attr1.SelectedItem;
            State state1 = (State)AllStatesComboBox_Attr1.SelectedItem;
            Process process2 = (Process)AllProcessComboBox_Attr2.SelectedItem;
            State state2 = (State)AllStatesComboBox_Attr2.SelectedItem;
            // 添加
            MySecurityPropertyVM.SecurityProperty.Authenticities.Add(new Authenticity(process1, state1, process2, state2));
        }

        // [按钮]Authenticity更新
        private void Button_Click_UpdateAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AllStatesComboBox_Attr1.SelectedItem == null || AuthenticityListBox.SelectedItem == null || AllStatesComboBox_Attr2.SelectedItem == null || AllProcessComboBox_Attr2.SelectedItem == null || AllProcessComboBox_Attr1.SelectedItem == null)
            {
                MessageBox.Show("进程类型 状态机状态 和 右侧性质 都要选中");
                return;
            }
            // todo 判重
            Process process1 = (Process)AllProcessComboBox_Attr1.SelectedItem;
            State state1 = (State)AllStatesComboBox_Attr1.SelectedItem;
            Process process2 = (Process)AllProcessComboBox_Attr2.SelectedItem;
            State state2 = (State)AllStatesComboBox_Attr2.SelectedItem;

            // 右侧选中的Attribute的下标
            int idx = AuthenticityListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Authenticities.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySecurityPropertyVM.SecurityProperty.Authenticities[idx] = new Authenticity(process1, state1, process2, state2);
        }

        // [按钮]Authenticity删除
        private void Button_Click_DeleteAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AuthenticityListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的性质");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AuthenticityListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Authenticities.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySecurityPropertyVM.SecurityProperty.Authenticities.RemoveAt(idx);
        }

        #endregion 按钮控制
    }
}
