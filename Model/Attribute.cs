using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace sbid.Model
{
    public class Attribute : AbstractModelBase
    {
        private string type;
        private string identifier;

        public string Type { get => type; set => type = value; }
        public string Identifier { get => identifier; set => identifier = value; }
        // 形如"int a;"的展示串
        public string ShowString
        {
            get
            {
                return type + " " + identifier + ";";
            }
        }

        public Attribute(string _type, string _idt)
        {
            this.type = _type;
            this.identifier = _idt;
        }
    }
}
