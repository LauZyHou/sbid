using System;
using System.Collections.Generic;
using System.Text;

namespace sbid
{
    class UserM //这个是模型
    {
        string _userName;
        string _companyName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

    }
}
