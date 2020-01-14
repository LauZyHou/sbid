using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    // 状态机(初始状态名字总是init,终止状态名字总是final)
    public class StateMachine
    {
        #region 字段和属性

        private ObservableCollection<State> states = new ObservableCollection<State>();
        private ObservableCollection<Transition> transitions = new ObservableCollection<Transition>();
        private string name; // name即是initial state

        public ObservableCollection<State> States { get => states; set => states = value; }
        public ObservableCollection<Transition> Transitions { get => transitions; set => transitions = value; }
        public string Name { get => name; set => name = value; }

        #endregion 字段和属性

        public StateMachine(string _name)
        {
            this.name = _name;
        }
    }
}
