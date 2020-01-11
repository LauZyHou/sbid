using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    // 转移关系
    public class Transition
    {
        public string fromState;
        public string toState;
        public string guard;
        public List<string> actions;

        //public 
    }

    public class StateMachine
    {
        // 状态的列表
        public List<string> states = new List<string>();
        // 转移关系
        public List<Transition> transitions = new List<Transition>();

        public StateMachine()
        {

        }
    }
}
