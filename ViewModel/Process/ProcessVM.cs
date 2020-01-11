using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class ProcessVM : NodeViewModel
    {
        #region 引用Model
        private Process process = new Process();
        
        private ObservableCollection<string> test = new ObservableCollection<string>();
        

        
        public Process Process { get => process; set => process = value; }

        #endregion 引用Model
        public ProcessVM()
        {
            this.Name = "Process";
            this.Color = "#FFDD99"; // 橙黄
            
        }

        public ProcessVM(int processName)
        {
            this.process.Name = processName.ToString();
            this.Name = "Process " + processName;
            this.Color = "#FFDD99";
        }
    }
}
