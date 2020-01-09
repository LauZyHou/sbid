using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;
using NetworkModel;

namespace sbid.ViewModel
{
    public class SafetyPropertyVM : NodeViewModel
    {
        #region 引用Model
        SafetyProperty safety = new SafetyProperty();

        public List<string> CTLs
        {
            get
            {
                return safety.CTLs;
            }
            set
            {
                safety.CTLs = value;
            }
        }

        public List<string> Invariants
        {
            get
            {
                return safety.Invariants;
            }
            set
            {
                safety.Invariants = value;
            }
        }
        #endregion 引用Model
    
        public SafetyPropertyVM(string name)
        {
            this.Name = name;
            this.Color = "#FFCCCC"; // 浅粉红
            // 测试
            this.safety.CTLs.Add("CTL公式1");
            this.safety.CTLs.Add("CTL公式2");
            this.safety.Invariants.Add("Invariants1");
            this.safety.Invariants.Add("Invariants2");
            this.safety.Invariants.Add("Invariants3");
        }
    }
}
