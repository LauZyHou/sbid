using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    // Process上的CommMethod
    public class CommMethod : AbstractModelBase
    {
        private string identifier = null;
        private ObservableCollection<Attribute> parameters = new ObservableCollection<Attribute>();
        private string inOut = "out";

        // 函数名
        public string Identifier { get => identifier; set => identifier = value; }
        // 形参列表
        public ObservableCollection<Attribute> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
                OnPropertyChanged("Parameters");
                OnPropertyChanged("ShowString");
            }
        }
        // 输入/输出类型
        public string InOut { get => inOut; set => inOut = value; }
        // 形如"send(Msg m);[out]"的展示串
        public string ShowString
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Attribute attr in parameters)
                {
                    list.Add(attr.Type + " " + attr.Identifier);
                }
                string paras = string.Join(", ", list.ToArray());
                return identifier + "(" + paras + ");[" + InOut + "]";
            }
        }

        public CommMethod(string _idt)
        {
            this.identifier = _idt;
        }
    }
}
