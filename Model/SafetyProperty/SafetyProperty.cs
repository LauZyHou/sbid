using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace sbid.Model
{
    public class SafetyProperty
    {
        private int _id = 1;
        private string name;
        private ObservableCollection<string> cTLs = new ObservableCollection<string>();
        private ObservableCollection<string> invariants = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChange("Name");
            }
        }
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        // CTL公式
        public ObservableCollection<string> CTLs { get => cTLs; set => cTLs=value; }
        // 不变性
        public ObservableCollection<string> Invariants { get => invariants; set => invariants = value ; }

        public SafetyProperty(string _name)
        {
            this.name = _name;
        }

        public SafetyProperty()
        {
            this.Name = "未命名" + _id;
            _id++;
            //Test_Init();
        }

        private void Test_Init()
        {
            this.cTLs.Add("CTL公式1");
            this.cTLs.Add("CTL公式2");
            this.invariants.Add("Invariants1");
            this.invariants.Add("Invariants2");
            this.invariants.Add("Invariants3");
        }
    }
}
