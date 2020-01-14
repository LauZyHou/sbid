using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using Utils;
using System.Windows;
using sbid.Model;

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
            // [数据维护]源点结点状态
            connection.Transition.FromState = ((StateVM)draggedOutConnector.ParentNode).State;

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
            newConnection.Transition.ToState = ((StateVM)connectorDraggedOver.ParentNode).State;
            this.stateMachineVM.StateMachine.Transitions.Add(newConnection.Transition);
            // 再将锚点连上
            newConnection.DestConnector = connectorDraggedOver;
        }

        /// <summary>
        /// Delete the node from the view-model.
        /// Also deletes any connections to or from the node.
        /// 删除结点
        /// </summary>
        public void DeleteNode(StateVM node)
        {
            // [数据维护]将结点对应的状态的使用次数-1
            this.stateMachineVM.Process.StateUsing[node.State.Name]--;

            //
            // Remove all connections attached to the node.
            // 删除结点的所有连线
            //
            // [数据维护]先将相连的Transition从状态机的Transitions里删除
            foreach (TransitionVM tvm in node.AttachedConnections)
            {
                this.stateMachineVM.StateMachine.Transitions.Remove(tvm.Transition);
            }
            // 再删除连线
            this.Network.Connections.RemoveRange(node.AttachedConnections);

            //
            // Remove the node from the network.
            // 从网络中删除
            //
            this.Network.Nodes.Remove(node);
        }

        /// <summary>
        /// Delete the currently selected nodes from the view-model.
        /// </summary>
        public void DeleteSelectedNodes()
        {
            // Take a copy of the nodes list so we can delete nodes while iterating.
            var nodesCopy = this.Network.Nodes.ToArray();

            foreach (StateVM node in nodesCopy)
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

        #region 创建状态结点

        // 创建初始状态结点
        public InitialStateVM CreateInitialState(Point nodeLocation)
        {
            // 检查是否之前创建过,创建过就取出引用
            InitialStateVM node = null;
            if (this.stateMachineVM.Process.StateQuote.ContainsKey("init"))
            {
                node = new InitialStateVM(this.stateMachineVM.Process.StateQuote["init"]);
                this.stateMachineVM.Process.StateUsing["init"]++;
            }
            else
            {
                node = new InitialStateVM("init");
                this.stateMachineVM.Process.StateQuote.Add("init", node.State);
                this.stateMachineVM.Process.StateUsing.Add("init", 1);
            }
            // 加入到当前状态机状态列表中
            this.stateMachineVM.StateMachine.States.Add(node.State);

            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 初始状态有唯一的锚点
            node.Connectors.Add(new ConnectorViewModel());

            this.Network.Nodes.Add(node);

            return node;
        }

        // 创建普通状态结点
        public StateVM CreateState(Point nodeLocation, string _name=null)
        {
            // 没提供名字时,要自动找一个没有使用过的名字
            if (_name == null)
            {
                while(this.stateMachineVM.Process.StateQuote.ContainsKey("未命名状态" + this.stateMachineVM.Process.Sid))
                {
                    this.stateMachineVM.Process.Sid++;
                }
                _name = "未命名状态" + this.stateMachineVM.Process.Sid;
            }
            // 检查是否之前创建过,创建过就取出引用
            StateVM node = null;
            if (this.stateMachineVM.Process.StateQuote.ContainsKey(_name))
            {
                node = new StateVM(this.stateMachineVM.Process.StateQuote[_name]);
                this.stateMachineVM.Process.StateUsing[_name]++;
            }
            else
            {
                node = new StateVM(_name);
                this.stateMachineVM.Process.StateQuote.Add(_name, node.State);
                this.stateMachineVM.Process.StateUsing.Add(_name, 1);
            }
            // 加入到当前状态机状态列表中
            this.stateMachineVM.StateMachine.States.Add(node.State);

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

        // 创建终止状态结点
        public FinalStateVM CreateFinalState(Point nodeLocation)
        {
            // 检查是否之前创建过,创建过就取出引用
            FinalStateVM node = null;
            if (this.stateMachineVM.Process.StateQuote.ContainsKey("final"))
            {
                node = new FinalStateVM(this.stateMachineVM.Process.StateQuote["final"]);
                this.stateMachineVM.Process.StateUsing["final"]++;
            }
            else
            {
                node = new FinalStateVM("final");
                this.stateMachineVM.Process.StateQuote.Add("final", node.State);
                this.stateMachineVM.Process.StateUsing.Add("final", 1);
            }
            // 加入到当前状态机状态列表中
            this.stateMachineVM.StateMachine.States.Add(node.State);

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
            StateVM defaultState = CreateState(new Point(150, 190), this.stateMachineVM.Process.Name + "_init");

            //var connection = new TransitionVM();
            TransitionVM connection = new TransitionVM();
            connection.SourceConnector = initialState.Connectors[0];
            connection.DestConnector = defaultState.Connectors[1];
            //[数据维护]写入到状态机Transition列表里
            connection.Transition.FromState = initialState.State;
            connection.Transition.ToState = defaultState.State;
            this.stateMachineVM.StateMachine.Transitions.Add(connection.Transition);

            this.Network.Connections.Add(connection);
        }

        #endregion Private Methods
    }
}
