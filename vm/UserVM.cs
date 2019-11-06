using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace sbid
{
    class UserVM
    {
        UserM _user;

        public UserVM()
        {
            _user = new UserM() { UserName = "myname", CompanyName = "mycompany" };
        }

        public string UserName
        {
            get { return _user.UserName; }
            set { _user.UserName = value; RaisePropertyChanged("UserNmae"); }
        }

        public string CompanyName
        {
            get { return _user.CompanyName; }
            set { _user.CompanyName = value; RaisePropertyChanged("CompanyNmae"); }
        }

        //当属性发生改变时通知所有监听者

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
