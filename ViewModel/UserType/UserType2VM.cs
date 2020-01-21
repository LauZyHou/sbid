using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class UserType2VM : NodeViewModel
    {
        private UserType2 userType2 = null;

        public UserType2 UserType2 { get => userType2; set => userType2 = value; }

        // 不指定名称
        public UserType2VM()
        {
            this.userType2 = new UserType2();
            this.Color = "#AADDFF";
        }

        // 指定名称
        public UserType2VM(string _name)
        {
            this.userType2 = new UserType2(_name);
            this.Color = "#AADDFF";
        }
    }
}
