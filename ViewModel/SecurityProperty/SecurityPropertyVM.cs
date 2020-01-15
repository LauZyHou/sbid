using NetworkModel;
using sbid.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.ViewModel
{
    public class SecurityPropertyVM : NodeViewModel
    {
        private SecurityProperty securityProperty = null;

        public SecurityProperty SecurityProperty { get => securityProperty; set => securityProperty = value; }

        public SecurityPropertyVM(string _name)
        {
            this.securityProperty = new SecurityProperty(_name);
            this.Color = "#FFEBCD";
        }
    }
}
