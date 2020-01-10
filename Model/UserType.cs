using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace sbid.Model
{
    public class UserType : Type
    {
        public int id;
        private String name;
        private Dictionary<String, Attribute> dictionary = new Dictionary<string, Attribute>();

        public UserType()
        {

        }

        public UserType(int userTypeId)
        {
            name = "UserType " + userTypeId.ToString();
        }
        public String Name
        {
            get;
            set;
        }
      

        public Dictionary<String, Attribute> Dictionary
        {
            get;
            set;
        }

        public bool addAttribute(Attribute attribute, String attributeName)
        {
            if (dictionary.ContainsKey(attributeName))
            {
                MessageBox.Show("变量名不可重复，请检查后添加！");
                return false;
            }
            dictionary.Add(attributeName, attribute);
            return true;
        }

        public  bool deleteAttribute(String attributeName)
        {
            if (dictionary.ContainsKey(attributeName))
            {
                dictionary.Remove(attributeName);
                return true;
            }
            MessageBox.Show("要删除的属性不存在，请检查后删除！");
            return false;
        }
    }
}
