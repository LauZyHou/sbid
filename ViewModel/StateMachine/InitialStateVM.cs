using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    // 状态机的初始状态ViewModel
    public class InitialStateVM : StateVM
    {
        public InitialStateVM(string _name)
        {
            State = new State(_name);
            this.Color = "Black";
        }

        public InitialStateVM(State _state)
        {
            State = _state;
            this.Color = "Black";
        }
    }
}
