using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 攻击树关系结点的类型枚举
    public enum RelationType
    {
        OR, AND, NEG
    }

    // 攻击树上的关系结点
    class RelationNode : NodeViewModel
    {
        private RelationType type;

        // 结点类型
        public RelationType Type { get => type; set => type = value; }

        // 默认构造:或关系
        public RelationNode()
        {
            this.Type = RelationType.OR;
            this.Name = "OR";
            this.Color = "#CC9999"; // 灰红色
            this.IsActive = false;
        }

        // 传参构造:按枚举类型
        public RelationNode(RelationType _type)
        {
            this.IsActive = false;
            this.Type = _type;
            // 根据攻击树类型设置结点颜色
            switch (_type)
            {
                case RelationType.OR:
                    this.Color = "#CC9999"; // 灰红色
                    this.Name = "OR";
                    break;
                case RelationType.AND:
                    this.Color = "#CC9966"; // 灰黄色
                    this.Name = "AND";
                    break;
                case RelationType.NEG:
                    this.Color = "#AAAAAA"; // 灰色
                    this.Name = "NEG";
                    break;
                default:
                    this.Color = "White";
                    this.Name = "未知的关系枚举";
                    break;
            }
        }
    }
}
