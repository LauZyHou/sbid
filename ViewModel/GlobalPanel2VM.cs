using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NetworkModel;
using Utils;

namespace sbid.ViewModel
{
    public class GlobalPanel2VM : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// This is the network that is displayed in the window.
        /// It is the main part of the view-model.
        /// </summary>
        public NetworkViewModel network = null;
        #endregion Internal Data Members

        public GlobalPanel2VM()
        {
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
            //var node = new RelationNode(RelationType.AND);
            //var node = new BlockDemo();
            var node = new UserTypeVM();
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            //
            // Create the default set of four connectors.
            // 创建默认的connector集合
            //

            // todo 类图还要对这里开多大的锚点数组做考虑
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

        #region Private Methods

        /// <summary>
        /// A function to conveniently populate the view-model with test data.
        /// </summary>
        private void PopulateWithTestData()
        {
            //
            // Create a network, the root of the view-model.
            //
            this.Network = new NetworkViewModel();

            //
            // Create some nodes and add them to the view-model.
            //
            var node1 = CreateNode("Node1", new Point(10, 10));
            var node2 = CreateNode("Node2", new Point(200, 10));

            //
            // Create a connection between the nodes.
            //
            var connection = new ConnectionViewModel();
            connection.SourceConnector = node1.Connectors[1];
            connection.DestConnector = node2.Connectors[3];

            //
            // Add the connection to the view-model.
            //
            this.Network.Connections.Add(connection);
        }

        #endregion Private Methods

        #region 创建结点
        public NodeViewModel CreatProcessVM(int processIdx, Point nodeLocation)
        {
            var node = new ProcessVM(processIdx);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        public NodeViewModel CreateUserTypeVM(int userTypeIdx, Point nodeLocation)
        {
            var node = new UserTypeVM(userTypeIdx);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }
        public NodeViewModel getUserTypeVM(int userTypeIdx)
        {
            var node = new UserTypeVM(userTypeIdx);
            return node;
        }
        #endregion 创建结点
    }
}
