using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
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
        // 窗体上的提示条对象的引用
        public static TextBlock tipTextBlock = null;

        #region 转换到XML

        // 协议
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
            foreach (SecurityProperty securityProperty in protocol.securityProperties)
            {
                SecurityProperty2Xml(securityProperty, xmlWriter);
            }
            foreach (SafetyProperty safetyProperty in protocol.safetyProperties)
            {
                SafetyProperty2Xml(safetyProperty, xmlWriter);
            }
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        // 进程
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
            foreach (KeyValuePair<string, StateMachine> stateMachine in process.stateMachineMap)
            {
                StateMachine2Xml(stateMachine.Value, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        // UserType
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

        // SafetyProperty
        private static void SafetyProperty2Xml(SafetyProperty safetyProperty, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("SafetyProperty");
            xmlWriter.WriteAttributeString("name", safetyProperty.Name);
            foreach (var ivar in safetyProperty.Invariants)
            {
                IVAR2Xml(ivar, xmlWriter);
            }
            foreach (var ctl in safetyProperty.CTLs)
            {
                CTL2Xml(ctl, xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        // Attribute
        private static void Attr2Xml(Attribute attr, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Attribute");
            xmlWriter.WriteAttributeString("name", attr.Identifier);
            xmlWriter.WriteAttributeString("type", attr.Type);
            xmlWriter.WriteEndElement();
        }

        private static void IVAR2Xml(string ivar, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("IVAR");
            xmlWriter.WriteAttributeString("name", ivar);
            xmlWriter.WriteEndElement();
        }

        private static void CTL2Xml(string ctl, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("CTL");
            xmlWriter.WriteAttributeString("name", ctl);
            xmlWriter.WriteEndElement();
        }

        // Method
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

        // Commmethod
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

        // 状态机
        private static void StateMachine2Xml(StateMachine stateMachine, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("StateMachine");
            xmlWriter.WriteAttributeString("name", stateMachine.Name);
            xmlWriter.WriteAttributeString("initial_state", stateMachine.Name);
            foreach (State state in stateMachine.States)
            {
                xmlWriter.WriteStartElement("State");
                xmlWriter.WriteAttributeString("name", state.Name);
                xmlWriter.WriteEndElement();
            }
            foreach (Transition transition in stateMachine.Transitions)
            {
                xmlWriter.WriteStartElement("Transition");
                xmlWriter.WriteAttributeString("from", transition.FromState.Name);
                xmlWriter.WriteAttributeString("to", transition.ToState.Name);
                {
                    xmlWriter.WriteStartElement("Guard");
                    xmlWriter.WriteAttributeString("content", transition.Guard);
                    xmlWriter.WriteEndElement();
                }
                foreach (string action in transition.Actions)
                {
                    xmlWriter.WriteStartElement("Action");
                    xmlWriter.WriteAttributeString("content", action);
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }

        // SecurityProperty
        private static void SecurityProperty2Xml(SecurityProperty securityProperty, XmlTextWriter xmlWriter)
        {
            foreach (Confidential confidential in securityProperty.Confidentials)
            {
                xmlWriter.WriteStartElement("ConfidentialProperty");
                xmlWriter.WriteAttributeString("process", confidential.Process.Name);
                xmlWriter.WriteAttributeString("attribute", confidential.Attribute.Identifier);
                xmlWriter.WriteEndElement();
            }
            foreach (Authenticity authenticity in securityProperty.Authenticities)
            {
                xmlWriter.WriteStartElement("AuthenticityProperty");
                {
                    // 1
                    xmlWriter.WriteStartElement("Value");
                    xmlWriter.WriteAttributeString("process", authenticity.Process1.Name);
                    xmlWriter.WriteAttributeString("state", authenticity.State1.Name);
                    xmlWriter.WriteAttributeString("attribute", authenticity.Attribute1.Identifier);
                    xmlWriter.WriteEndElement();
                    // 2
                    xmlWriter.WriteStartElement("Value");
                    xmlWriter.WriteAttributeString("process", authenticity.Process2.Name);
                    xmlWriter.WriteAttributeString("state", authenticity.State2.Name);
                    xmlWriter.WriteAttributeString("attribute", authenticity.Attribute2.Identifier);
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
        }

        #endregion 转换到XML
    }
}
