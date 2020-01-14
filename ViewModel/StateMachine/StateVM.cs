using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    // 状态机上状态的ViewModel
    public class StateVM : NodeViewModel
    {
        private State state = null;
        public State State { get => state; set => state = value; }

        // 传入name时在内部构造
        public StateVM(string _name)
        {
            this.state = new State(_name);
            this.Color = "White";
        }

        // 传入状态对象时直接写入
        public StateVM(State _state)
        {
            this.state = _state;
            this.Color = "White";
        }

        public StateVM()
        {
        }
    }
}
