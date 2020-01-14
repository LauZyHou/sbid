using NetworkModel;
using sbid.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.ViewModel
{
    public class SecurityPropertyVM: NodeViewModel
    {
        private SecurityProperty securityProperty = new SecurityProperty();

        public SecurityProperty SecurityProperty { get => securityProperty; set => securityProperty = value; }

        public SecurityPropertyVM()
        {
            this.Color = "#FFEBCD";
        }

        public SecurityPropertyVM(string _name)
        {
            this.securityProperty.Name = _name;
            this.Color = "#FFEBCD";
        }

    }
}
