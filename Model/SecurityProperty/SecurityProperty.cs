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
        private ObservableCollection<Confidential> confidentials = new ObservableCollection<Confidential>();
        private ObservableCollection<Authenticity> authenticities = new ObservableCollection<Authenticity>();

        public string Name { get => name; set => name = value; }
        public ObservableCollection<Confidential> Confidentials { get => confidentials; set => confidentials = value; }
        public ObservableCollection<Authenticity> Authenticities { get => authenticities; set => authenticities = value; }

        public SecurityProperty()
        {
            this.name = "未命名" + _id;
            _id++;
        }
    }
}
