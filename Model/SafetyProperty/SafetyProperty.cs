using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class SafetyProperty
    {
        // CTL公式
        public List<string> CTLs { get; set; }
        // 不变性
        public List<string> Invariants { get; set; }

        public SafetyProperty()
        {
            this.CTLs = new List<string>();
            this.Invariants = new List<string>();
        }
    }
}
