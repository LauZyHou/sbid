using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class ProcessVM : NodeViewModel
    {
        #region 字段和属性
        private Process process = new Process();

        public Process Process { get => process; set => process = value; }

        #endregion 字段和属性
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
