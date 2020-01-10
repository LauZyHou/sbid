using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class Attribute
    {
        private String identifier;
        private Type type;

        public Attribute(String identifier, Type type)
        {
            this.identifier = identifier;
            this.type = type;
        }
    }
}
