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
                OnPropertyChanged("ParaString");
            }
        }
        // 输入/输出类型
        public string InOut { get => inOut; set => inOut = value; }
        // 用于在前端展示形参列表的逗号分隔串
        public string ParaString
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Attribute attr in parameters)
                {
                    list.Add(attr.Type + " " + attr.Identifier);
                }
                return string.Join(", ", list.ToArray());
                //形如"int a, bool b, Msg c"
            }
        }

        public CommMethod(string _idt)
        {
            this.identifier = _idt;
        }
    }
}
