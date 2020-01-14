using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace sbid.Model
{
    public class UserType2 : INotifyPropertyChanged
    {
        private static int _id = 1;
        private string name;
        private ObservableCollection<Attribute> attributes = new ObservableCollection<Attribute>();

        public event PropertyChangedEventHandler PropertyChanged;

        // UserType2的名称
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
        // UserType2包含的Attribute(这里key不仅是int,bool还可以是UserType2的name)
        public ObservableCollection<Attribute> Attributes { get => attributes; set => attributes = value; }

        public UserType2(string _name)
        {
            this.name = _name;
        }

        public UserType2()
        {
            this.name = "未命名" + _id;
            _id++;
            //Test_Init();
        }

        private void Test_Init()
        {
            this.Attributes.Add(new Attribute("int", "m"));
            this.Attributes.Add(new Attribute("int", "n"));
        }
    }
}
