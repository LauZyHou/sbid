using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using System.Collections.ObjectModel;

namespace sbid.Model
{
    // 转移关系
    public class Transition : AbstractModelBase
    {
        #region 字段和属性

        private State fromState;
        private State toState;
        private string guard = "True";
        private ObservableCollection<string> actions = new ObservableCollection<string>();

        public State FromState { get => fromState; set => fromState = value; }
        public State ToState { get => toState; set => toState = value; }
        public string Guard
        {
            get => guard;
            set
            {
                this.guard = value;
                OnPropertyChanged("Guard");
                OnPropertyChanged("ContentString");
            }
        }

        public ObservableCollection<string> Actions
        {
            get => actions;
            set
            {
                this.actions = value;
                OnPropertyChanged("Actions");
                OnPropertyChanged("ContentString");
            }
        }
        // 从guard和action计算出的字符串,用于显示给用户
        public string ContentString
        {
            get
            {
                string content = this.guard;
                foreach (string action in this.actions)
                {
                    content += "\n" + action;
                }
                return content;
            }
        }

        #endregion 字段和属性

        public Transition()
        {
        }

        public Transition(State _from, State _to)
        {
            this.fromState = _from;
            this.toState = _to;
        }
    }
}
