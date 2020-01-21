using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;
using NetworkModel;

namespace sbid.ViewModel
{
    public class AxiomVM : NodeViewModel
    {
        private Axiom axiom;

        public Axiom Axiom { get => axiom; set => axiom = value; }

        public AxiomVM()
        {
            this.axiom = new Axiom();
            this.Color = "#CCFFEE"; // 绿松石绿
        }
    }
}