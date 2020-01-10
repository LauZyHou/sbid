using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using sbid.Model;

namespace sbid.ViewModel
{
    public class UserTypeVM: NodeViewModel
    {
        UserType userType = new UserType();
        private int id;
        public UserTypeVM()
        {
            this.Name = "UserType";
            this.Color = "#00FFFF"; // 橙黄
        }

        public UserTypeVM(string userTypeName)
        {
            this.Name = "UserType " + userTypeName;
            this.Color = "#00FFFF";
        }
        public UserTypeVM(int userTypeId)
        {
            this.id = userTypeId;
            this.Name = "UserType " + userTypeId;
            this.Color = "#00FFFF";
        }

    }
}
