using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    // 终止状态的ViewModel
    public class FinalStateVM : StateVM
    {
        public FinalStateVM(string _name)
        {
            State = new State(_name);
            this.Color = "Black";
        }

        public FinalStateVM(State _state)
        {
            State = _state;
            this.Color = "Black";
        }
    }
}
