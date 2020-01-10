using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    public class MethodInfo
    {
        // 函数的返回类型
        public string returnType = "void";
        // 函数的参数列表<类型,形参名>
        public List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
        // 加解密方法名(可选)
        public string cryptoName = null;
    }

    public class CommMethodInfo
    {
        // 函数的参数列表<类型,形参名>
        public List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
        // 指示是输入或输出
        public string inout = "out";
    }

    public class Process
    {
        // Process的名称
        public string name;
        // Attribute名->类型
        public Dictionary<string, string> attributeMap = new Dictionary<string, string>();
        // Method名->返回值,形参列表等函数信息
        public Dictionary<string, MethodInfo> methodMap = new Dictionary<string, MethodInfo>();
        // CommMethod名->形参列表,inout等CommMethod信息
        public Dictionary<string, CommMethodInfo> CommMethodMap = new Dictionary<string, CommMethodInfo>();
        // 对应的状态机(多个嵌套成一个)
        public Dictionary<string, StateMachine> StateMachineMap = new Dictionary<string, StateMachine>();

        public Process()
        {
        }

        public Process(string _name)
        {
            this.name = _name;
        }
    }
}
