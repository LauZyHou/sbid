using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Authenticity
    {
        private Process process1;
        private State state1;
        private Process process2;
        private State state2;

        public Process Process1 { get => process1; set => process1 = value; }
        public State State1 { get => state1; set => state1 = value; }
        public Process Process2 { get => process2; set => process2 = value; }
        public State State2 { get => state2; set => state2 = value; }

        public Authenticity(Process process1, State state1, Process process2, State state2)
        {
            this.process1 = process1;
            this.state1 = state1;
            this.process2 = process2;
            this.state2 = state2;
        }
    }
}
