using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace sbid.Model
{
    // 转移关系
    public class Transition
    {
        #region 字段和属性

        private string fromState;
        private string toState;
        private string guard = "True";
        private ObservableCollection<string> actions = new ObservableCollection<string>();

        public string FromState { get => fromState; set => fromState = value; }
        public string ToState { get => toState; set => toState = value; }
        public string Guard { get => guard; set => guard = value; }
        public ObservableCollection<string> Actions { get => actions; set => actions = value; }

        #endregion 字段和属性

        public Transition()
        {
        }

        public Transition(string _from,string _to)
        {
            this.fromState = _from;
            this.toState = _to;
        }
    }

    // 状态机(初始状态名字总是init,终止状态名字总是final)
    public class StateMachine
    {
        #region 字段和属性

        private ObservableCollection<string> states = new ObservableCollection<string>();
        private ObservableCollection<Transition> transitions = new ObservableCollection<Transition>();
        private string name;

        public ObservableCollection<string> States { get => states; set => states = value; }
        public ObservableCollection<Transition> Transitions { get => transitions; set => transitions = value; }
        public string Name { get => name; set => name = value; }

        #endregion 字段和属性

        public StateMachine(string _name)
        {
            this.name = _name;
        }
    }
}
