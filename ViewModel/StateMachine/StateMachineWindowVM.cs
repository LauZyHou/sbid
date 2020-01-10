using System;
using System.Collections.Generic;
using System.Text;
using NetworkModel;
using Utils;
using System.Windows;

namespace sbid.ViewModel
{
    public class StateMachineWindowVM : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// This is the network that is displayed in the window.
        /// It is the main part of the view-model.
        /// </summary>
        public NetworkViewModel network = null;

        #endregion Internal Data Members

        public StateMachineWindowVM()
        {
            this.Network = new NetworkViewModel();
            // Add some test data to the view-model.
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
        /// </summary>
        public ConnectionViewModel ConnectionDragStarted(ConnectorViewModel draggedOutConnector, Point curDragPoint)
        {
            if (draggedOutConnector.AttachedConnection != null)
            {
                //
                // There is an existing connection attached to the connector that has been dragged out.
                // Remove the existing connection from the view-model.
                //
                this.Network.Connections.Remove(draggedOutConnector.AttachedConnection);
            }

            //
            // Create a new connection to add to the view-model.
            //
            var connection = new ConnectionViewModel();

            //
            // Link the source connector to the connector that was dragged out.
            //
            connection.SourceConnector = draggedOutConnector;

            //
            // Set the position of destination connector to the current position of the mouse cursor.
            //
            connection.DestConnectorHotspot = curDragPoint;

            //
            // Add the new connection to the view-model.
            //
            this.Network.Connections.Add(connection);

            return connection;
        }

        /// <summary>
        /// Called as the user continues to drag the connection.
        /// </summary>
        public void ConnectionDragging(ConnectionViewModel connection, Point curDragPoint)
        {
            //
            // Update the destination connection hotspot while the user is dragging the connection.
            //
            connection.DestConnectorHotspot = curDragPoint;
        }

        /// <summary>
        /// Called when the user has finished dragging out the new connection.
        /// </summary>
        public void ConnectionDragCompleted(ConnectionViewModel newConnection, ConnectorViewModel connectorDraggedOut, ConnectorViewModel connectorDraggedOver)
        {
            if (connectorDraggedOver == null)
            {
                //
                // The connection was unsuccessful.
                // Maybe the user dragged it out and dropped it in empty space.
                //
                this.Network.Connections.Remove(newConnection);
                return;
            }

            //
            // The user has dragged the connection on top of another valid connector.
            //

            var existingConnection = connectorDraggedOver.AttachedConnection;
            if (existingConnection != null)
            {
                //
                // There is already a connection attached to the connector that was dragged over.
                // Remove the existing connection from the view-model.
                //
                this.Network.Connections.Remove(existingConnection);
            }

            //
            // Finalize the connection by attaching it to the connector
            // that the user dropped the connection on.
            //
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

        /// <summary>
        /// Delete the node from the view-model.
        /// Also deletes any connections to or from the node.
        /// </summary>
        public void DeleteNode(NodeViewModel node)
        {
            //
            // Remove all connections attached to the node.
            //
            this.Network.Connections.RemoveRange(node.AttachedConnections);

            //
            // Remove the node from the network.
            //
            this.Network.Nodes.Remove(node);
        }

        /// <summary>
        /// Create a node and add it to the view-model.
        /// </summary>
        public NodeViewModel CreateNode(string name, Point nodeLocation)
        {
            //var node = new NodeViewModel(name);
            var node = new RelationNode(RelationType.AND);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            //
            // Create the default set of four connectors.
            // 创建默认的connector集合，这里对攻击树的结点总是添加六个锚点
            //
            for (int i = 0; i < 6; i++)
            {
                node.Connectors.Add(new ConnectorViewModel());
            }

            //
            // Add the new node to the view-model.
            //
            this.Network.Nodes.Add(node);

            return node;
        }

        // 创建初始状态结点
        public NodeViewModel CreateInitialState(Point nodeLocation)
        {
            //var node = new NodeViewModel(name);
            var node = new InitalStateVM();
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 初始状态有唯一的锚点
            node.Connectors.Add(new ConnectorViewModel());

            this.Network.Nodes.Add(node);

            return node;
        }

        // 创建普通状态结点
        public NodeViewModel CreateState(string name, Point nodeLocation)
        {
            var node = new StateVM(name);
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
        public NodeViewModel CreateFinalState(Point nodeLocation)
        {
            var node = new FinalStateVM();
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            // 终止状态有唯一的锚点
            node.Connectors.Add(new ConnectorViewModel());

            this.Network.Nodes.Add(node);

            return node;
        }

        #region Private Methods

        // 状态机建模的初始化:创建初始状态和第一个默认状态,然后将它们连接起来
        private void PopulateWithTestData()
        {
            var initialState = CreateInitialState(new Point(80, 50));
            // todo 进程名"p1_"拼接到最前面
            var defaultState = CreateState("init", new Point(50, 140));

            var connection = new ConnectionViewModel();
            connection.SourceConnector = initialState.Connectors[0];
            connection.DestConnector = defaultState.Connectors[1];

            this.Network.Connections.Add(connection);
        }

        #endregion Private Methods
    }
}
