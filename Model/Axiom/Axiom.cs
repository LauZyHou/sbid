using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace sbid.Model
{
    public class Axiom
    {
        private int _id = 1;
        private string name;
        private ObservableCollection<Attribute> attributes = new ObservableCollection<Attribute>();
        private ObservableCollection<Method> methods = new ObservableCollection<Method>();
        private ObservableCollection<string> ax = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        public Axiom(string _name)
        {
            this.name = _name;
        }

        public Axiom()
        {
            this.Name = "未命名" + _id;
            _id++;
            //Test_Init();
        }
        public string Name
        {
            get 
            {
                return name;
            }
            set 
            {
                name = value;
                NotifyPropertyChange("Name");
            }
        }
        public ObservableCollection<Method> Methods
        {
            get
            {
                return methods;
            }
            set
            {
                methods = value;
            }
        }
        public ObservableCollection<Attribute> Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
            }
        }
        public ObservableCollection<string> Ax
        {
            get
            {
                return ax;
            }
            set
            {
                ax = value;
            }
        }
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Test_Init()
        {
            Method m1 = new Method("enc1");
            m1.ReturnType = "Msg";
            m1.Parameters.Add(new Attribute("Msg", "m"));
            m1.Parameters.Add(new Attribute("Key", "k"));

            Method m2 = new Method("dec1");
            m2.ReturnType = "Msg";
            m2.Parameters.Add(new Attribute("Msg", "m"));
            m2.Parameters.Add(new Attribute("Key", "k"));
            methods.Add(m1);
            methods.Add(m2);
        }
    }
}
