using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Attribute
    {
        private string type;
        private string identifier;

        public string Type { get => type; set => type = value; }
        public string Identifier { get => identifier; set => identifier = value; }

        public Attribute(string _type, string _idt)
        {
            this.type = _type;
            this.identifier = _idt;
        }
    }
}
