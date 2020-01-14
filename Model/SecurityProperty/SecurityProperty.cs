using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace sbid.Model
{
    public class SecurityProperty
    {
        private string name;
        private ObservableCollection<AuthenticityAttribute> auattributes = new ObservableCollection<AuthenticityAttribute>();
        private ObservableCollection<Attribute> conattributes = new ObservableCollection<Attribute>();

        public string Name { get => name; set => name = value; }
        public ObservableCollection<Attribute> Conattributes { get => conattributes; set => conattributes = value; }
        public ObservableCollection<AuthenticityAttribute> Auattributes { get => auattributes; set => auattributes = value; }

        public SecurityProperty(string _name)
        {
            this.name = _name;
        }
    }
}
