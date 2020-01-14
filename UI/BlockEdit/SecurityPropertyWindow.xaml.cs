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
        private SecurityPropertyVM mySecurityPropertyVM ;
        public SecurityPropertyVM MySecurityPropertyVM { get => mySecurityPropertyVM; set => mySecurityPropertyVM = value; }
        public List<Process> Processes { get => processes; set => processes = value; }

        private List<Process> processes = null;

        public SecurityPropertyWindow()
        {
            InitializeComponent();
        }
        public SecurityPropertyWindow(SecurityPropertyVM _spv)
        {
            InitializeComponent();
            this.mySecurityPropertyVM = _spv;
            this.Title += _spv.Name;
            this.DataContext = this;
            AllProcessListBox_Attr.ItemsSource = ResourceManager.currentProtocol.processes;
            AllProcessListBox_Attr1.ItemsSource = ResourceManager.currentProtocol.processes;
            AllProcessListBox_Attr2.ItemsSource = ResourceManager.currentProtocol.processes;
            AttrListBox.ItemsSource = mySecurityPropertyVM.SecurityProperty.Conattributes;
            AuthenticityListBox.ItemsSource = mySecurityPropertyVM.SecurityProperty.Auattributes;
        }


        private void Button_Click_AddConfidential(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr.SelectedItem == null || AllStatesListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("需要选中 进程类型 和 状态机 ");
                return;
            }
            // todo 判重
            string process = ((Process)AllProcessListBox_Attr.SelectedItems[0]).Name;
            string state = (string)AllStatesListBox_Attr.SelectedItems[0];
            // 添加
            MySecurityPropertyVM.SecurityProperty.Conattributes.Add(new Model.Attribute(process, state));
        }

        private void AllProcessListBox_Attr_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessListBox_Attr.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessListBox_Attr.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (var item in list)
            {
                AllStatesListBox_Attr.ItemsSource=item.Value.States;
            }
        }

        private void Button_Click_UpdateConfidential(object sender, RoutedEventArgs e)
        {
            if (AllStatesListBox_Attr.SelectedItem == null || AttrListBox.SelectedItem == null || AllProcessListBox_Attr.SelectedItem ==null)
            {
                MessageBox.Show("进程类型 、状态机状态 和 右侧性质 都要选中");
                return;
            }
            // todo 判重
            string process = ((Process)AllProcessListBox_Attr.SelectedItems[0]).Name;
            string state = (string)AllStatesListBox_Attr.SelectedItems[0];
            
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Conattributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySecurityPropertyVM.SecurityProperty.Conattributes[idx] = new Model.Attribute(process,state);
        }

        private void Button_Click_DeleteConfidential(object sender, RoutedEventArgs e)
        {
            if (AttrListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的性质");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AttrListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Conattributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySecurityPropertyVM.SecurityProperty.Conattributes.RemoveAt(idx);
        }


        private void AllProcessListBox_Attr1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessListBox_Attr1.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessListBox_Attr1.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (var item in list)
            {
                AllStatesListBox_Attr1.ItemsSource = item.Value.States;
            }
        }

        private void AllProcessListBox_Attr2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessListBox_Attr2.SelectedItem == null)
                return;
            // 获取当前选中的Attribute
            Process nowProcess = (Process)AllProcessListBox_Attr2.SelectedItem;
            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            foreach (var item in list)
            {
                AllStatesListBox_Attr2.ItemsSource = item.Value.States;
            }
        }

        private void Button_Click_AddAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr1.SelectedItem == null || AllStatesListBox_Attr1.SelectedItem == null || AllProcessListBox_Attr1.SelectedItem == null || AllStatesListBox_Attr2.SelectedItem == null|| AllProcessListBox_Attr2.SelectedItem ==null)
            {
                MessageBox.Show("需要选中 进程类型 和 状态机 ");
                return;
            }
            // todo 判重
            string process1 = ((Process)AllProcessListBox_Attr1.SelectedItems[0]).Name;
            string state1 = (string)AllStatesListBox_Attr1.SelectedItems[0];
            string process2 = ((Process)AllProcessListBox_Attr2.SelectedItems[0]).Name;
            string state2 = (string)AllStatesListBox_Attr2.SelectedItems[0];
            // 添加
            MySecurityPropertyVM.SecurityProperty.Auattributes.Add(new Model.AuthenticityAttribute(process1, state1,process2,state2));
        }

        private void Button_Click_UpdateAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AllStatesListBox_Attr1.SelectedItem == null || AuthenticityListBox.SelectedItem == null || AllStatesListBox_Attr2.SelectedItem == null || AllProcessListBox_Attr2.SelectedItem ==null || AllProcessListBox_Attr1.SelectedItem == null)
            {
                MessageBox.Show("进程类型 、状态机状态 和 右侧性质 都要选中");
                return;
            }
            // todo 判重
            string process1 = ((Process)AllProcessListBox_Attr1.SelectedItems[0]).Name;
            string state1 = (string)AllStatesListBox_Attr1.SelectedItems[0];
            string process2 = ((Process)AllProcessListBox_Attr2.SelectedItems[0]).Name;
            string state2 = (string)AllStatesListBox_Attr2.SelectedItems[0];

            // 右侧选中的Attribute的下标
            int idx = AuthenticityListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Auattributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 更新
            MySecurityPropertyVM.SecurityProperty.Auattributes[idx] = new Model.AuthenticityAttribute(process1, state1, process2, state2);
        }

        private void Button_Click_DeleteAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AuthenticityListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选中 要删除的性质");
                return;
            }
            // 右侧选中的Attribute的下标
            int idx = AuthenticityListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Auattributes.Count)
            {
                MessageBox.Show("越界!");
                return;
            }
            // 删除
            MySecurityPropertyVM.SecurityProperty.Auattributes.RemoveAt(idx);

        }
    }
}
