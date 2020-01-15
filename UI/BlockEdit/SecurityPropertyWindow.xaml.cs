using sbid.Model;
using sbid.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace sbid.UI
{
    /// <summary>
    /// SecurityPropertyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SecurityPropertyWindow : Window
    {
        #region 参数与属性

        private SecurityPropertyVM mySecurityPropertyVM = null;

        public SecurityPropertyVM MySecurityPropertyVM { get => mySecurityPropertyVM; set => mySecurityPropertyVM = value; }

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
        private void AllProcessListBox_Attr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessListBox_Attr.SelectedItem == null)
                return;
            // 获取当前选中的Process
            Process nowProcess = (Process)AllProcessListBox_Attr.SelectedItem;
            // 在属性box中显示选中进程的Attribute
            AllAttributesListBox_Attr.ItemsSource = nowProcess.Attributes;
        }

        // Authenticity的进程1改变选中
        private void AllProcessComboBox_Attr1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessComboBox_Attr1.SelectedItem == null)
                return;

            // 获取当前选中的Process
            Process nowProcess = (Process)AllProcessComboBox_Attr1.SelectedItem;

            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            ObservableCollection<State> processAllStates = new ObservableCollection<State>();
            foreach (var item in list)
            {
                foreach (State state in item.Value.States)
                {
                    if (!processAllStates.Contains(state))
                        processAllStates.Add(state);
                }
            }
            AllStatesComboBox_Attr1.ItemsSource = processAllStates;

            // Attribute
            AllAttributeComboBox_Attr1.ItemsSource = nowProcess.Attributes;
        }

        // Authenticity的进程2改变选中
        private void AllProcessComboBox_Attr2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点其它地方时可能导致这里未选中任何项
            if (AllProcessComboBox_Attr2.SelectedItem == null)
                return;

            // 获取当前选中的Process
            Process nowProcess = (Process)AllProcessComboBox_Attr2.SelectedItem;

            // 在状态机box中显示选中进程的状态
            Dictionary<string, StateMachine> list = nowProcess.stateMachineMap;
            ObservableCollection<State> processAllStates = new ObservableCollection<State>();
            foreach (var item in list)
            {
                foreach (State state in item.Value.States)
                {
                    if (!processAllStates.Contains(state))
                        processAllStates.Add(state);
                }
            }
            AllStatesComboBox_Attr2.ItemsSource = processAllStates;

            // Attribute
            AllAttributeComboBox_Attr2.ItemsSource = nowProcess.Attributes;
        }

        #endregion 条目改变选中的事件处理

        #region 按钮控制

        // [按钮]Confidential添加
        private void Button_Click_AddConfidential(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr.SelectedItem == null ||
                AllAttributesListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("需要选定 进程 和 Attribute");
                return;
            }

            // todo 判重
            Process process = (Process)AllProcessListBox_Attr.SelectedItem;
            Attribute attribute = (Attribute)AllAttributesListBox_Attr.SelectedItem;

            // 添加
            MySecurityPropertyVM.SecurityProperty.Confidentials.Add(new Confidential(process, attribute));
        }

        // [按钮]Confidential更新
        private void Button_Click_UpdateConfidential(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr.SelectedItem == null ||
                AllAttributesListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("需要选定 进程 和 Attribute");
                return;
            }

            if (ConfidentialListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定 右侧要更新的性质");
                return;
            }

            // todo 判重
            Process process = (Process)AllProcessListBox_Attr.SelectedItem;
            Attribute attribute = (Attribute)AllAttributesListBox_Attr.SelectedItem;

            // 右侧选中的Attribute的下标
            int idx = ConfidentialListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Confidentials.Count)
            {
                MessageBox.Show("越界!");
                return;
            }

            // 更新
            MySecurityPropertyVM.SecurityProperty.Confidentials[idx] = new Confidential(process, attribute);
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
            if (AllProcessComboBox_Attr1.SelectedItem == null ||
                AllStatesComboBox_Attr1.SelectedItem == null ||
                AllAttributeComboBox_Attr1.SelectedItem == null ||
                AllProcessComboBox_Attr2.SelectedItem == null ||
                AllStatesComboBox_Attr2.SelectedItem == null ||
                AllAttributeComboBox_Attr2.SelectedItem == null)
            {
                MessageBox.Show("需要选定 进程 和 状态 和 Attribute");
                return;
            }

            // todo 判重
            Process process1 = (Process)AllProcessComboBox_Attr1.SelectedItem;
            State state1 = (State)AllStatesComboBox_Attr1.SelectedItem;
            Attribute attribute1 = (Attribute)AllAttributeComboBox_Attr1.SelectedItem;
            Process process2 = (Process)AllProcessComboBox_Attr2.SelectedItem;
            State state2 = (State)AllStatesComboBox_Attr2.SelectedItem;
            Attribute attribute2 = (Attribute)AllAttributeComboBox_Attr2.SelectedItem;

            // 添加
            MySecurityPropertyVM.SecurityProperty.Authenticities.Add(
                new Authenticity(process1, state1, attribute1, process2, state2, attribute2)
            );
        }

        // [按钮]Authenticity更新
        private void Button_Click_UpdateAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AllProcessComboBox_Attr1.SelectedItem == null ||
                AllStatesComboBox_Attr1.SelectedItem == null ||
                AllAttributeComboBox_Attr1.SelectedItem == null ||
                AllProcessComboBox_Attr2.SelectedItem == null ||
                AllStatesComboBox_Attr2.SelectedItem == null ||
                AllAttributeComboBox_Attr2.SelectedItem == null)
            {
                MessageBox.Show("需要选定 进程 和 状态 和 Attribute");
                return;
            }
            if (AuthenticityListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定 右侧要更新的项");
                return;
            }

            // todo 判重
            Process process1 = (Process)AllProcessComboBox_Attr1.SelectedItem;
            State state1 = (State)AllStatesComboBox_Attr1.SelectedItem;
            Attribute attribute1 = (Attribute)AllAttributeComboBox_Attr1.SelectedItem;
            Process process2 = (Process)AllProcessComboBox_Attr2.SelectedItem;
            State state2 = (State)AllStatesComboBox_Attr2.SelectedItem;
            Attribute attribute2 = (Attribute)AllAttributeComboBox_Attr2.SelectedItem;

            // 右侧选中的Attribute的下标
            int idx = AuthenticityListBox.SelectedIndex;
            if (idx >= MySecurityPropertyVM.SecurityProperty.Authenticities.Count)
            {
                MessageBox.Show("越界!");
                return;
            }

            // 更新
            MySecurityPropertyVM.SecurityProperty.Authenticities[idx] =
                new Authenticity(process1, state1, attribute1, process2, state2, attribute2);
        }

        // [按钮]Authenticity删除
        private void Button_Click_DeleteAuthenticity(object sender, RoutedEventArgs e)
        {
            if (AuthenticityListBox.SelectedItem == null)
            {
                MessageBox.Show("需要选定 要删除的性质");
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
