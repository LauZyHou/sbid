using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;
using NetworkModel;

namespace sbid.ViewModel
{
    public class SafetyPropertyVM : NodeViewModel
    {
        SafetyProperty safetyProperty = null;

        public SafetyProperty SafetyProperty
        {
            get => safetyProperty;
            set => safetyProperty = value;
        }

        public SafetyPropertyVM(string name)
        {
            this.safetyProperty = new SafetyProperty(name);
            this.Color = "#FFCCCC"; // 浅粉红
        }

        public SafetyPropertyVM()
        {
            this.safetyProperty = new SafetyProperty();
            this.Color = "#FFCCCC"; // 浅粉红
        }
    }
}
