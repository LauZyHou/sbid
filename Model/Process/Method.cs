using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utils;

namespace sbid.Model
{
    // Process上的Method
    public class Method : AbstractModelBase
    {
        private string returnType = "void";
        private string identifier = null;
        private ObservableCollection<Attribute> parameters = new ObservableCollection<Attribute>();
        private string cryptoName = "";

        // 返回值类型
        public string ReturnType { get => returnType; set => returnType = value; }
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
        // 加解密方法名
        public string CryptoName { get => cryptoName; set => cryptoName = value; }
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

        public Method(string _idt)
        {
            this.identifier = _idt;
        }
    }
}
