using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 状态机的初始状态ViewModel
    public class InitalStateVM : NodeViewModel
    {
        public InitalStateVM()
        {
            this.Name = "";
            this.Color = "Black";
        }
    }
}
