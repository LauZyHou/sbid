using sbid.ViewModel;
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
using System.Windows.Shapes;

namespace sbid.UI
{
    /// <summary>
    /// ProcessWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessWindow : Window
    {
        private ProcessVM processVM = null;

        // 在构造时必须将ViewModel传入,以作修改和显示
        public ProcessWindow(ProcessVM _pvm)
        {
            InitializeComponent();
            this.processVM = _pvm;
            this.Title += _pvm.Process.Name;
        }
    }
}
