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
                OnPropertyChanged("ShowString");
                OnPropertyChanged("MethodHeadString");
            }
        }
        // 加解密方法名
        public string CryptoName { get => cryptoName; set => cryptoName = value; }
        // 形如"Msg enc(Msg m, Key k);[AES]"的展示串
        public string ShowString
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Attribute attr in parameters)
                {
                    list.Add(attr.Type + " " + attr.Identifier);
                }
                string paras =  string.Join(", ", list.ToArray());

                return returnType + " " + identifier + "(" + paras + ");[" + cryptoName + "]";
            }
        }
        // 函数头部分:形如形如"Msg enc(Msg m, Key k);的展示串
        public string MethodHeadString
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Attribute attr in parameters)
                {
                    list.Add(attr.Type + " " + attr.Identifier);
                }
                string paras = string.Join(", ", list.ToArray());

                return returnType + " " + identifier + "(" + paras + ");";
            }
        }

        public Method()
        {
        }

        public Method(string _idt)
        {
            this.identifier = _idt;
        }
    }
}
