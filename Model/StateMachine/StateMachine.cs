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
        private string guard;
        private ObservableCollection<string> actions = new ObservableCollection<string>();

        public string FromState { get => fromState; set => fromState = value; }
        public string ToState { get => toState; set => toState = value; }
        public string Guard { get => guard; set => guard = value; }
        public ObservableCollection<string> Actions { get => actions; set => actions = value; }

        #endregion 字段和属性

        public Transition()
        {
        }
    }

    public class StateMachine
    {
        #region 字段和属性

        private ObservableCollection<string> states;
        private ObservableCollection<Transition> transitions;

        public ObservableCollection<string> States { get => states; set => states = value; }
        public ObservableCollection<Transition> Transitions { get => transitions; set => transitions = value; }

        #endregion 字段和属性

        public StateMachine()
        {
        }
    }
}
