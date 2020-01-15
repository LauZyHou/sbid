using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Confidential
    {
        private Process process = null;
        private Attribute attribute;

        public Process Process { get => process; set => process = value; }
        public Attribute Attribute { get => attribute; set => attribute = value; }

        public Confidential(Process process, Attribute attribute)
        {
            this.process = process;
            this.attribute = attribute;
        }
    }
}
