using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    public class Axiom : AbstractModelBase
    {
        private static int _id = 1;
        private string name;
        private ObservableCollection<Method> methods = new ObservableCollection<Method>();
        private ObservableCollection<string> ax = new ObservableCollection<string>();

        public ObservableCollection<Method> Methods { get => methods; set => methods = value; }
        public ObservableCollection<string> Ax { get => ax; set => ax = value; }

        // Axiom的名称
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public Axiom()
        {
            this.Name = "未命名" + _id;
            _id++;
            //Test_Init();
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
