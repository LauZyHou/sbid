using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    public class Process : AbstractModelBase
    {
        private static int _id = 1;
        private string name;
        private ObservableCollection<Attribute> attributes = new ObservableCollection<Attribute>();
        private ObservableCollection<Method> methods = new ObservableCollection<Method>();
        private ObservableCollection<CommMethod> commMethods = new ObservableCollection<CommMethod>();
        private Dictionary<string, int> stateUsing = new Dictionary<string, int>();
        private Dictionary<string, State> stateQuote = new Dictionary<string, State>();
        private int sid = 1;

        // Process对应的状态机(多个嵌套成一个)
        public Dictionary<string, StateMachine> stateMachineMap = new Dictionary<string, StateMachine>();
        // 存状态机中的所有状态的图形结点数目,用于[1]判定init和finial是否已经存在,[2]对无引用的状态删除
        public Dictionary<string, int> StateUsing { get => stateUsing; set => stateUsing = value; }
        // 存状态机中所有状态的引用,用以取出其引用对象,不重复创建状态对象
        public Dictionary<string, State> StateQuote { get => stateQuote; set => stateQuote = value; }
        // 状态机的状态命名用的自增id
        public int Sid { get => sid; set => sid = value; }

        // Process的名称
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        // Process包含的Attribute
        public ObservableCollection<Attribute> Attributes { get => attributes; set => attributes = value; }
        // Process包含的Method
        public ObservableCollection<Method> Methods { get => methods; set => methods = value; }
        // Process包含的CommMethod
        public ObservableCollection<CommMethod> CommMethods { get => commMethods; set => commMethods = value; }

        public Process()
        {
            this.name = "未命名" + _id;
            _id++;
            Test_Init();
        }

        private void Test_Init()
        {
            attributes.Add(new Attribute("int", "a"));
            attributes.Add(new Attribute("bool", "b"));
            attributes.Add(new Attribute("Msg", "c"));

            Method m1 = new Method("dec");
            m1.ReturnType = "Msg";
            m1.Parameters.Add(new Attribute("Msg", "m"));
            m1.Parameters.Add(new Attribute("Key", "k"));
            m1.CryptoName = "AES";
            Method m2 = new Method("enc");
            m2.ReturnType = "Msg";
            m2.Parameters.Add(new Attribute("Msg", "m"));
            m2.Parameters.Add(new Attribute("Key", "k"));
            m2.CryptoName = "AES";
            methods.Add(m1);
            methods.Add(m2);

            CommMethod cm1 = new CommMethod("send");
            cm1.Parameters.Add(new Attribute("Msg", "m"));
            CommMethod cm2 = new CommMethod("recv");
            cm2.Parameters.Add(new Attribute("Key", "k"));
            cm2.InOut = "in";
            commMethods.Add(cm1);
            commMethods.Add(cm2);
        }
    }
}
