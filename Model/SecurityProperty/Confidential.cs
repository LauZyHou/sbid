using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Confidential
    {
        private Process process = null;
        private State state = null;

        public Process Process { get => process; set => process = value; }
        public State State { get => state; set => state = value; }

        public Confidential(Process process, State state)
        {
            this.process = process;
            this.state = state;
        }
    }
}
