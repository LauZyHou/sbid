using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    public class Protocol : AbstractModelBase
    {
        private string name;
        public List<Process> processes = new List<Process>();
        // 临时
        public List<UserType2> userType2 = new List<UserType2>();

        public List<SecurityProperty> securityProperties = new List<SecurityProperty>();

        public string Name { get => name; set => name = value; }

        // 用于获取Ptotocal中所有的类型(即"int","bool"加上所有UserType2的Name)
        public ObservableCollection<string> AllTypes
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>();
                ret.Add("int");
                ret.Add("bool");
                foreach (UserType2 user in userType2)
                {
                    ret.Add(user.Name);
                }
                return ret;
            }
        }

        public Protocol(string _name)
        {
            this.name = _name;
        }
    }
}
