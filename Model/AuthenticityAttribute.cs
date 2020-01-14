using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace sbid.Model
{
    public class AuthenticityAttribute : AbstractModelBase
    {
        private string type1;
        private string type2;
        private string identifier1;
        private string identifier2;

  
        // 形如"int a;"的展示串
        public string ShowString
        {
            get
            {
                return type1 + " " + identifier1 + " "+ type2 +" "+identifier2;
            }
        }

        public string Type1 { get => type1; set => type1 = value; }
        public string Type2 { get => type2; set => type2 = value; }
        public string Identifier1 { get => identifier1; set => identifier1 = value; }
        public string Identifier2 { get => identifier2; set => identifier2 = value; }

        public AuthenticityAttribute()
        {
        }

        public AuthenticityAttribute(string _type1, string _idt1, string _type2, string _idt2)
        {
            this.type1 = _type1;
            this.identifier1 = _idt1;
            this.type2 = _type2;
            this.identifier2 = _idt2;
        }
    }
}
