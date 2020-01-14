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
        private ObservableCollection<Attribute> attributes = new ObservableCollection<Attribute>();

        public string Name { get => name; set => name = value; }
        public ObservableCollection<Attribute> Attributes { get => attributes; set => attributes = value; }

        public SecurityProperty()
        {
            this.name = "未命名" + _id;
            _id++;
            Test_Init();
        }
        private void Test_Init()
        {
            this.Attributes.Add(new Attribute("A", "run"));
            this.Attributes.Add(new Attribute("B", "stop"));
        }
    }
}
