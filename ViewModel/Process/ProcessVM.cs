using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class ProcessVM : NodeViewModel
    {
        #region 引用Model
        Process process = new Process();

        #endregion 引用Model
        public ProcessVM()
        {
            this.Name = "Process";
            this.Color = "#FFDD99"; // 橙黄
        }

        public ProcessVM(int processName)
        {
            this.process.name = processName.ToString();
            this.Name = "Process " + processName;
            this.Color = "#FFDD99";
        }
    }
}
