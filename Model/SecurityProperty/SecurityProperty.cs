using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace sbid.Model
{
    public class SecurityProperty
    {
        private static int _id = 1;
        private string name;
        private ObservableCollection<AuthenticityAttribute> auattributes = new ObservableCollection<AuthenticityAttribute>();
        private ObservableCollection<Attribute> conattributes = new ObservableCollection<Attribute>();


        public string Name { get => name; set => name = value; }
        public ObservableCollection<Attribute> Conattributes { get => conattributes; set => conattributes = value; }
        public ObservableCollection<AuthenticityAttribute> Auattributes { get => auattributes; set => auattributes = value; }

        public SecurityProperty()
        {
            this.name = "未命名" + _id;
            _id++;
            //Test_Init();
        }
        
    }
}
