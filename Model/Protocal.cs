using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Protocal
    {
        public string name;
        public List<Process> processes = new List<Process>();
        public List<UserType> userTypes = new List<UserType>();

        public Protocal(string _name)
        {
            this.name = _name;
        }
    }
}
