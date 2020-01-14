using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 状态机的初始状态ViewModel
    public class InitialStateVM : NodeViewModel
    {
        public InitialStateVM()
        {
            this.Name = "";
            this.Color = "Black";
        }
    }
}
