using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 状态机上状态的ViewModel
    public class StateVM : NodeViewModel
    {
        // 可以集成对应的状态,实际就是一个字符串,这里直接用父类的Name
        public StateVM(string name)
        {
            this.Name = name; // 其实就是State
            this.Color = "White";
        }
    }
}
