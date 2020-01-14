using NetworkModel;
using sbid.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.ViewModel
{
    public class SecurityPropertyVM : NodeViewModel
    {
        //private Process process = null;
        private SecurityProperty securityProperty = null;

        //public Process Process { get => process; set => process = value; }
        public SecurityProperty SecurityProperty { get => securityProperty; set => securityProperty = value; }

        //public SecurityPropertyVM()
        //{
        //    this.Color = "#FFEBCD";
        //}

        //public SecurityPropertyVM(string _name, Process _process)
        //{
        //    this.securityProperty.Name = _name;
        //    this.Color = "#FFEBCD";
        //    this.process = _process;
        //}

        public SecurityPropertyVM(string _name)
        {
            this.securityProperty = new SecurityProperty(_name);
            this.Color = "#FFEBCD";
        }
    }
}
