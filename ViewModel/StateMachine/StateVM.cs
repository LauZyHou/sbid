using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 状态机上状态的ViewModel
    public class StateVM : NodeViewModel
    {
        public StateVM(string name)
        {
            this.Name = name;
            this.Color = "White";
        }
    }
}
