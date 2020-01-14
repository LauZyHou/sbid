using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    // 状态机结点上的连线VM
    public class TransitionVM : ConnectionViewModel
    {
        // 集成转移关系类
        private Transition transition = new Transition();

        public Transition Transition { get => transition; set => transition = value; }

        public TransitionVM()
        {
        }
    }
}
