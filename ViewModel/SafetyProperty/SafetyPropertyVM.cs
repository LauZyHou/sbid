using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;
using NetworkModel;

namespace sbid.ViewModel
{
    public class SafetyPropertyVM : NodeViewModel
    {
        SafetyProperty safetyProperty = new SafetyProperty();
        private int id;
        public SafetyProperty SafetyProperty
        {
            get => safetyProperty;
            set => safetyProperty = value;
        }
        public SafetyPropertyVM(string name)
        {
            this.safetyProperty.Name = name;
            this.Name = "SafetyProperty " + name;
            this.Name = name;
            this.Color = "#FFCCCC"; // 浅粉红
        }
        public SafetyPropertyVM(int safetyPropertyId)
        {
            this.id = safetyPropertyId;
            this.Name = "SafetyProperty " + safetyPropertyId;
            this.Color = "#FFCCCC";
        }
        public SafetyPropertyVM()
        {
            this.Name = "SafetyProperty";
            this.Color = "#FFCCCC";
        }
    }
}
