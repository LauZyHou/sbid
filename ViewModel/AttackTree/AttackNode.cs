using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;

namespace sbid.ViewModel
{
    // 攻击树上的攻击结点
    class AttackNode : NodeViewModel
    {
        // 用户对攻击的描述直接写在基类NodeViewModel的Name字段里

        // 默认构造:使用默认描述
        public AttackNode()
        {
            this.Name = "待描述的攻击结点";
            this.Color = "#99CC99"; // 灰绿色
            this.condition = ConditionType.ACTIVE;
            this.IsActive = true;

        }

        // 传参构造:按用户指定的描述
        public AttackNode(string _desc)
        {
            this.Name = _desc;
            this.Color = "#99CC99"; // 灰绿色
            this.condition = ConditionType.ACTIVE;
            this.IsActive = true;
        }
    }
}
