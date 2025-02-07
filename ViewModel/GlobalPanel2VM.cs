﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NetworkModel;
using Utils;
using sbid.Model;

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
            //
            // Create a network, the root of the view-model.
            //
            this.Network = new NetworkViewModel();
            // Add some test data to the view-model.
            //PopulateWithTestData();
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
                    // 判断类型,将其从全局资源中也删除
                    if (node is ProcessVM)
                    {
                        ResourceManager.currentProtocol.processes.Remove(((ProcessVM)node).Process);
                    }
                    else if (node is UserType2VM)
                    {
                        ResourceManager.currentProtocol.userType2.Remove(((UserType2VM)node).UserType2);
                        // todo 警示用户此操作影响到其它类图
                        // todo 将其它类图也删除,或者考虑禁止删除UserType2,除非没有其它类图在使用它
                    }
                    // todo 其它类图资源的删除
                    // 在界面上删除图形
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
            //var node = new ProcessVM();
            var node = new SafetyPropertyVM("test");
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            //
            // Create the default set of four connectors.
            // 创建默认的connector集合
            //

            // todo 类图还要对这里开多大的锚点数组做考虑
            //for (int i = 0; i < 6; i++)
            //{
            //    node.Connectors.Add(new ConnectorViewModel());
            //}

            //
           //Add the new node to the view-model.
            
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
            // Create some nodes and add them to the view-model.
            //
            var node1 = CreateNode("Node1", new Point(30, 30));
            var node2 = CreateNode("Node2", new Point(250, 30));


            //
            // Create a connection between the nodes.
            //
            //var connection = new ConnectionViewModel();
            //connection.SourceConnector = node1.Connectors[1];
            //connection.DestConnector = node2.Connectors[3];

            //
            // Add the connection to the view-model.
            //
            //this.Network.Connections.Add(connection);
        }

        #endregion Private Methods

        #region 创建结点

        // 创建Process
        public NodeViewModel CreatProcessVM(Point nodeLocation)
        {
            ProcessVM node = new ProcessVM();
            // 放到全局资源里
            ResourceManager.currentProtocol.processes.Add(node.Process);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        // 创建UserType(指定名称)
        public NodeViewModel CreateUserType2VM(string _name, Point nodeLocation)
        {
            UserType2VM node = new UserType2VM(_name);
            // 放到全局资源里
            ResourceManager.currentProtocol.userType2.Add(node.UserType2);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        // 创建UserType(不指定名称)
        public NodeViewModel CreateUserType2VM(Point nodeLocation)
        {
            UserType2VM node = new UserType2VM();
            // 放到全局资源里
            ResourceManager.currentProtocol.userType2.Add(node.UserType2);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        // 创建SecurityProperty
        public NodeViewModel CreateSecurityPropertyVM(Point nodeLocation)
        {
            SecurityPropertyVM node = new SecurityPropertyVM();
            // 放到全局资源里
            ResourceManager.currentProtocol.securityProperties.Add(node.SecurityProperty);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        // 创建SafetyProperty
        public NodeViewModel CreateSafetyPropertyVM(Point nodeLocation)
        {
            SafetyPropertyVM node = new SafetyPropertyVM();
            // 放到全局资源里
            ResourceManager.currentProtocol.safetyProperties.Add(node.SafetyProperty);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        // 创建Axiom
        public NodeViewModel CreateAxiomVM(Point nodeLocation)
        {
            AxiomVM node = new AxiomVM();
            // 放到全局资源里
            ResourceManager.currentProtocol.axioms.Add(node.Axiom);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;
            this.Network.Nodes.Add(node);
            return node;
        }

        public void Test_Node()
        {
            NodeViewModel node1 = CreatProcessVM(new Point(30, 30));
            NodeViewModel node2 = CreatProcessVM(new Point(250, 30));
            NodeViewModel node3 = CreateUserType2VM("Msg", new Point(300, 30));
            NodeViewModel node4 = CreateUserType2VM("Key", new Point(350, 30));
        }
        #endregion 创建结点
    }
}
