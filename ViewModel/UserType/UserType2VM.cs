using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class UserType2VM : NodeViewModel
    {
        private UserType2 userType2 = new UserType2();

        public UserType2 UserType2 { get => userType2; set => userType2 = value; }

        public UserType2VM()
        {
            this.Color = "#AADDFF";
        }

        public UserType2VM(string _name)
        {
            this.userType2.Name = _name;
            this.Color = "#AADDFF";
        }
    }
}
