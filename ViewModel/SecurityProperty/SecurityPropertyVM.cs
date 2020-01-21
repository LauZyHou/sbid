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

        public SecurityPropertyVM()
        {
            this.securityProperty = new SecurityProperty();
            this.Color = "#FFEBCD";
        }
    }
}
