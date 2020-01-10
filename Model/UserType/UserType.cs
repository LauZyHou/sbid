using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace sbid.Model
{
    public class UserType
    {
        // 变量名->类型
        private Dictionary<string, string> attributeMap = new Dictionary<string, string>();

        public string name;

        public UserType()
        {
        }

        public UserType(string _name)
        {
            this.name = _name;
        }


        public Dictionary<string, string> Dictionary
        {
            get;
            set;
        }

        public bool addAttribute(string _identifier, string _type)
        {
            if (attributeMap.ContainsKey(_identifier))
            {
                MessageBox.Show("变量名不可重复");
                return false;
            }
            attributeMap.Add(_identifier, _type);
            return true;
        }

        public bool deleteAttribute(string _identifier)
        {
            if (attributeMap.ContainsKey(_identifier))
            {
                attributeMap.Remove(_identifier);
                return true;
            }
            MessageBox.Show("要删除的属性不存在");
            return false;
        }
    }
}
