using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;
using NetworkModel;

namespace sbid.ViewModel
{
    public class AxiomVM : NodeViewModel
    {
        Axiom axiom = null;

        public Axiom Axiom
        {
            get => axiom;
            set => axiom = value;
        }

        public AxiomVM(string name)
        {
            this.axiom = new Axiom(name);
            this.Color = "#FFCCCC"; // 浅粉红
        }

        public AxiomVM()
        {
            this.axiom = new Axiom();
            this.Color = "#FFCCCC"; // 浅粉红
        }
    }
}