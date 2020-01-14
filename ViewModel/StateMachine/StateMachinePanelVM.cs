using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using Utils;
using System.Windows;

namespace sbid.ViewModel
{
    public class StateMachinePanelVM : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// This is the network that is displayed in the window.
        /// It is the main part of the view-model.
        /// </summary>
        public NetworkViewModel network = null;
        // 集成StateMachine的ViewModel
        private StateMachineVM stateMachineVM = null;

        public StateMachineVM StateMachineVM { get => stateMachineVM; set => stateMachineVM = value; }

        #endregion Internal Data Members

        /*
        public StateMachinePanelVM()
        {
            this.Network = new NetworkViewModel();
            // Add some test data to the view-model.
            PopulateWithTestData();
        }*/

        // 在构造时传入要集成的StateMachineVM
        public StateMachinePanelVM(StateMachineVM stateMachineVM)
        {
            this.stateMachineVM = stateMachineVM;
            this.Network = new NetworkViewModel();
            PopulateWithTestData();
        }

        /// <summary>
        /// This is the network that is displayed in the window.
        /// 这里NetworkViewModel，其中放的是窗体上显示的结点连线等
        /// It is the main part of the view-model.
        /// </summary>
        public NetworkViewModel Network
        {
            get
            {
                return network;
            }
            set
            {
                network = value;

                OnPropertyChanged("Network");
            }
        }

        /// <summary>
        /// Called when the user has started to drag out a connector, thus creating a new connection.
        /// 当用户开始拖拽时调用此方法
        /// </summary>
        public TransitionVM ConnectionDragStarted(ConnectorViewModel draggedOutConnector, Point curDragPoint)
        {
            if (draggedOutConnector.AttachedConnection != null)
            {
                //
                // There is an existing connection attached to the connector that has been dragged out.
                // Remove the existing connection from the view-model.
                // 这个点上已经有连线了,将这条线删除
                //
                // [数据维护]先在状态机中删除这个Transition
                TransitionVM oldTransition = (TransitionVM)draggedOutConnector.AttachedConnection;
                this.stateMachineVM.StateMachine.Transitions.Remove(oldTransition.Transition);
                // 再将线删除
                this.Network.Connections.Remove(draggedOutConnector.AttachedConnection);
            }

            //
            // Create a new connection to add to the view-model.
            // 创建一个新的连线类对象
            //
            var connection = new TransitionVM();

            //
            // Link the source connector to the connector that was dragged out.
            // 源点
            //
            connection.SourceConnector = draggedOutConnector;
            // [数据维护]源点结点名
            connection.Transition.FromState = draggedOutConnector.ParentNode.Name;

            //
            // Set the position of destination connector to the current position of the mouse cursor.
            // 箭头的目标位置是鼠标当前位置
            //
            connection.DestConnectorHotspot = curDragPoint;

            //
            // Add the new connection to the view-model.
            // 在网络中暂时添加这条线,这样能显示整个拖拽过程
            //
            this.Network.Connections.Add(connection);

            return connection;
        }

        /// <summary>
        /// Called as the user continues to drag the connection.
        /// 当用户正在拖拽时调用此方法
        /// </summary>
        public void ConnectionDragging(TransitionVM connection, Point curDragPoint)
        {
            //
            // Update the destination connection hotspot while the user is dragging the connection.
            // 更新箭头的目标位置
            //
            connection.DestConnectorHotspot = curDragPoint;
        }

        /// <summary>
        /// Called when the user has finished dragging out the new connection.
        /// 当用户完成拖拽时调用此方法
        /// </summary>
        public void ConnectionDragCompleted(TransitionVM newConnection, ConnectorViewModel connectorDraggedOut, ConnectorViewModel connectorDraggedOver)
        {
            if (connectorDraggedOver == null)
            {
                //
                // The connection was unsuccessful.
                // Maybe the user dragged it out and dropped it in empty space.
                // 拖拽到空白位置就会失败,自动放弃其中的Transition
                //
                this.Network.Connections.Remove(newConnection);
                return;
            }

            //
            // The user has dragged the connection on top of another valid connector.
            // 用户拖动连线到其它可用的锚点上去
            //

            TransitionVM existingConnection = (TransitionVM)connectorDraggedOver.AttachedConnection;
            // 获取到目标位置的锚点本来是有连线的
            if (existingConnection != null)
            {
                //
                // There is already a connection attached to the connector that was dragged over.
                // Remove the existing connection from the view-model.
                // 将之前的这个连线移除
                //
                // [数据维护]先在状态机的Transitions中删除这个Transition
                this.stateMachineVM.StateMachine.Transitions.Remove(existingConnection.Transition);
                // 再将线删除
                this.Network.Connections.Remove(existingConnection);
            }

            //
            // Finalize the connection by attaching it to the connector
            // that the user dropped the connection on.
            // 完成连线
            //
            // [数据维护]先将其中的Transition写到状态机的Transitions里
            newConnection.Transition.ToState = connectorDraggedOver.ParentNode.Name;
            this.stateMachineVM.StateMachine.Transitions.Add(newConnection.Transition);
            // 再将锚点连上
            newConnection.DestConnector = connectorDraggedOver;
        }

        /// <summary>
        /// Delete the currently selected nodes from the view-model.
        /// </summary>
        public void DeleteSelectedNodes()
        {
            // Take a copy of the nodes list so we can delete nodes while iterating.
            var nodesCopy = this.Network.Nodes.ToArray();

            foreach (var node in nodesCopy)
            {
                if (node.IsSelected)
                {
                    DeleteNode(node);
                }
            }
        }

        // [临时]判断选中的两个结点之间是否有边,如果有边就返回TransitionVM
        public TransitionVM FindTransitionVM_ByCheckNode()
        {
            List<NodeViewModel> selectedNodes = new List<NodeViewModel>();
            var nodesCopy = this.Network.Nodes.ToArray();
            foreach (var node in nodesCopy)
            {
                if (node.IsSelected)
                {
                    selectedNodes.Add(node);
                }
            }
            if (selectedNodes.Count != 2)
            {
                MessageBox.Show("请选择两个状态以编辑边");
                return null;
            }
            // 检查两个边之间的连线,即检查两个边被占用的锚点集合是否存在dest关系
            ICollection<ConnectionViewModel> attachedConnections_0 = selectedNodes[0].AttachedConnections;
            //ICollection<ConnectionViewModel> attachedConnections_1 = selectedNodes[1].AttachedConnections;
            foreach (ConnectionViewModel cn0 in attachedConnections_0)
            {
                foreach (ConnectorViewModel ctr1 in selectedNodes[1].Connectors)
                {
                    if (cn0.DestConnector == ctr1 || cn0.SourceConnector == ctr1) // 找到时返回
                    {
                        return (TransitionVM)cn0;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Delete the node from the view-model.
        /// Also deletes any connections to or from the node.
        /// 删除结点
        /// </summary>
        public void DeleteNode(NodeViewModel node)
        {
            //
            // Remove all connections attached to the node.
            // 删除结点的所有连线
            //
            this.Network.Connections.RemoveRange(node.AttachedConnections);

            //
            // Remove the node from the network.
            // 从网络中删除
            //
            this.Network.Nodes.Remove(node);
        }

        #region 创建状态结点

        // 创建初始状态结点
        public InitialStateVM CreateInitialState(Point nodeLocation)
        {
            //var node = new NodeViewModel(name);
            InitialStateVM node = new InitialStateVM();
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 初始状态有唯一的锚点
            node.Connectors.Add(new ConnectorViewModel());

            this.Network.Nodes.Add(node);

            return node;
        }

        // 创建普通状态结点(todo检查状态名不能重复)
        public StateVM CreateState(string _name, Point nodeLocation)
        {
            /* 创建状态(数据) */
            this.stateMachineVM.StateMachine.States.Add(_name);
            /* 创建状态(图形的VM) */
            StateVM node = new StateVM(_name);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 普通状态的锚点数组
            for (int i = 0; i < 6; i++)
            {
                node.Connectors.Add(new ConnectorViewModel());
            }

            this.Network.Nodes.Add(node);

            return node;
        }

        // 创建普通状态结点,没有提供状态名时使用自增的状态名
        public StateVM CreateState(Point nodeLocation)
        {
            string prefix = "未命名状态";
            string nowName = null;
            // 获取到当前状态机的状态自增id
            int sid = this.stateMachineVM.Sid;
            int i = 1;
            for (; i <= 10; i++) // 只搜索10个名字
            {
                nowName = prefix + (sid + i);
                if (!this.stateMachineVM.StateMachine.States.Contains(nowName))
                {
                    break;
                }
            }
            if (i > 10)
            {
                // 例如当前sid=5,用户把10个之前的状态改成"未命名状态6"~"未命名状态15",再创建就会执行到这里
                MessageBox.Show("找不到合适的名字!\n请将那些自己添加的\"未命名状态\"更名");
                return null; // 创建状态失败
            }
            // 维护当前状态机的状态自增id
            this.stateMachineVM.Sid = sid + i;

            return CreateState(nowName, nodeLocation);
        }

        // 创建终止状态结点
        public FinalStateVM CreateFinalState(Point nodeLocation)
        {
            FinalStateVM node = new FinalStateVM();
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 终止状态有唯一的锚点
            node.Connectors.Add(new ConnectorViewModel());

            this.Network.Nodes.Add(node);

            return node;
        }

        #endregion 创建状态结点

        #region Private Methods

        // 状态机建模的初始化:创建初始状态和第一个默认状态,然后将它们连接起来
        private void PopulateWithTestData()
        {
            InitialStateVM initialState = CreateInitialState(new Point(180, 50));
            // 取"Process名_init"作为默认给的一个普通状态的Name
            StateVM defaultState = CreateState(this.stateMachineVM.Process.Name + "_init", new Point(150, 190));

            //var connection = new TransitionVM();
            TransitionVM connection = new TransitionVM();
            connection.SourceConnector = initialState.Connectors[0];
            connection.DestConnector = defaultState.Connectors[1];
            //[数据维护]写入到状态机列表里
            connection.Transition.FromState = "init";
            connection.Transition.ToState = defaultState.Name;
            this.stateMachineVM.StateMachine.Transitions.Add(connection.Transition);

            this.Network.Connections.Add(connection);
        }

        #endregion Private Methods
    }
}
