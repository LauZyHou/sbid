using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using NetworkModel;

namespace sbid.ViewModel
{
    // 状态机结点上的连线VM
    public class TransitionVM : ConnectionViewModel
    {
        // 比普通的锚点连线多了线上文字:gurard条件和action
        private string guard;
        private List<string> actions = new List<string>();
        
        public TransitionVM()
        {
            this.guard = "双击编辑";
            this.actions.Add("123");
            this.actions.Add("45");
        }

        public string Guard
        {
            get => guard;
            set
            {
                this.guard = value;
                OnPropertyChanged("Guard");
                OnPropertyChanged("Content");
            }
        }

        public List<string> Actions
        {
            get => actions;
            set
            {
                this.actions = value;
                OnPropertyChanged("Actions");
                OnPropertyChanged("Content");
            }
        }

        // 从guard和action计算出的字符串,用于显示给用户
        public string Content
        {
            get
            {
                string content = guard;
                foreach (string action in actions)
                {
                    content += "\n" + action;
                }
                return content;
            }
        }
    }
}
