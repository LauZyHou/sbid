using System;
using System.Collections.Generic;
using System.Text;
using sbid.Model;

namespace sbid.ViewModel
{
    public class StateMachineVM
    {
        #region 字段和属性

        // 在创建状态机VM时创建状态机的数据对象(见StateMachineVM的构造器)
        private StateMachine stateMachine = null;
        // 反引Process的数据对象,以立刻将其写入Process中去,在改变状态机名时也要改Process中状态机表的key
        private Process process = null;
        // 状态机命名用的自增id
        private int sid;

        public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
        public Process Process { get => process; set => process = value; }
        public int Sid { get => sid; set => sid = value; }

        #endregion 字段和属性

        public StateMachineVM(string _name, Process _process)
        {
            this.stateMachine = new StateMachine(_name);
            this.process = _process;
            this.process.stateMachineMap.Add(_name, stateMachine);// _name == stateMachine.Name
        }
    }
}
