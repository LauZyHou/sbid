using sbid.Model;
using sbid.ViewModel;

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
            AttrListBox.ItemsSource = mySecurityPropertyVM.SecurityProperty.Attributes;
        }


        private void Button_Click_AddSecurity(object sender, RoutedEventArgs e)
        {
            if (AllProcessListBox_Attr.SelectedItem == null || AllStatesListBox_Attr.SelectedItem == null)
            {
                MessageBox.Show("需要选中 进程类型 和 状态机 ");
                return;
            }
            // todo 判重
            string process = (string)AllProcessListBox_Attr.SelectedItems[0];
            string state = (string)AllStatesListBox_Attr.SelectedItems[0];
            // 添加
            MySecurityPropertyVM.SecurityProperty.Attributes.Add(new Model.Attribute(process, state));
        }
    }
}
