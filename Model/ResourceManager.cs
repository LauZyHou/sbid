using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace sbid.Model
{
    // 管理资源
    public class ResourceManager
    {
        public static Protocol currentProtocol = null;// 在内存中记录当前用户所在的Protocal
        // 管理所有的Protocal
        public static List<Protocol> protocols = new List<Protocol>();
        // 管理所有内置函数
        public static List<Method> innerMethods = new List<Method>();
        // 管理所有加密算法
        public static List<string> cryptoNames = new List<string>();


        #region 转换到XML
        public static void Protocol2Xml(Protocol protocol, string fileName)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartElement("Protocol");
            xmlWriter.WriteAttributeString("name", protocol.Name);
            foreach (Process process in protocol.processes)
            {
                Process2Xml(process, xmlWriter);
            }
            foreach (UserType2 userType in protocol.userType2)
            {
                UserType22Xml(userType, xmlWriter);
            }
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        private static void Process2Xml(Process process, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Process");
            xmlWriter.WriteAttributeString("name", process.Name);
            foreach (var attr in process.Attributes)
            {
                Attr2Xml(attr, xmlWriter);
            }
            foreach (var method in process.Methods)
            {
                Method2Xml(method, xmlWriter);
            }
            foreach (var commMethod in process.CommMethods)
            {
                CommMethod2Xml(commMethod, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void UserType22Xml(UserType2 userType, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("UserType2");
            xmlWriter.WriteAttributeString("name", userType.Name);
            foreach (var attr in userType.Attributes)
            {
                Attr2Xml(attr, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void Attr2Xml(Attribute attr, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Attribute");
            xmlWriter.WriteAttributeString("name", attr.Identifier);
            xmlWriter.WriteAttributeString("type", attr.Type);
            xmlWriter.WriteEndElement();
        }

        private static void Method2Xml(Method method, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Method");
            xmlWriter.WriteAttributeString("name", method.Identifier);
            xmlWriter.WriteAttributeString("return_type", method.ReturnType);
            xmlWriter.WriteAttributeString("algorithm_id", method.CryptoName);
            foreach (var attr in method.Parameters)
            {
                Attr2Xml(attr, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void CommMethod2Xml(CommMethod commMethod, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("CommMethod");
            xmlWriter.WriteAttributeString("name", commMethod.Identifier);
            xmlWriter.WriteAttributeString("inout", commMethod.InOut);
            foreach (var attr in commMethod.Parameters)
            {
                Attr2Xml(attr, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }
        #endregion 转换到XML
    }
}
