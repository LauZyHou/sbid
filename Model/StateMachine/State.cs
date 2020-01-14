using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    // 状态封装起来而不用字符串,是为了用传引用代替传值
    public class State
    {
        private string name;
        public string Name { get => name; set => name = value; }

        public State(string _name)
        {
            name = _name;
        }
    }
}
