using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sbid.ViewModel;
using NetworkModel;
using NetworkUI;

namespace sbid.UserControl
{
    /// <summary>
    /// AttackTreePanel.xaml 的交互逻辑
    /// </summary>
    public partial class AttackTreePanel : System.Windows.Controls.UserControl
    {
        // 攻击树的ViewModel
        public AttackTreePanelVM ViewModel
        {
            get
            {
                return (AttackTreePanelVM)this.scrollViewer.DataContext;
            }
        }

        public AttackTreePanel()
        {
            InitializeComponent();
        }

        //【工具栏按钮】攻击结点
        private void Button_Click_Attack(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatAttackNode("todo结点描述", new Point(100, 100));
        }

        //【工具栏按钮】关系NEG
        private void Button_Click_NEG(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatRelationNode(RelationType.NEG, new Point(100, 100));
        }

        //【工具栏按钮】关系AND
        private void Button_Click_AND(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatRelationNode(RelationType.AND, new Point(100, 100));
        }

        //【工具栏按钮】关系OR
        private void Button_Click_OR(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatRelationNode(RelationType.OR, new Point(100, 100));
        }

        // 攻击树UserControl加载时触发此事件
        private void AttackTree_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// 当用户[开始]拖动锚点连线时触发此事件
        private void networkControl_ConnectionDragStarted(object sender, ConnectionDragStartedEventArgs e)
        {
            // 创建锚点的ViewModel
            var draggedOutConnector = (ConnectorViewModel)e.ConnectorDraggedOut;
            // 获取拖动的起始位置
            var curDragPoint = Mouse.GetPosition(networkControl);

            // "开始拖动"调用的方法，传入锚点和起始位置，获取到连线Connection
            var connection = this.ViewModel.ConnectionDragStarted(draggedOutConnector, curDragPoint);

            // 将连线写入到事件对象中去，这样才能在鼠标松开前一直保持这个连线
            e.Connection = connection;
        }

        /// 当用户[正在]拖动锚点连线时触发此事件
        private void networkControl_ConnectionDragging(object sender, ConnectionDraggingEventArgs e)
        {
            // 当前鼠标位置
            var curDragPoint = Mouse.GetPosition(networkControl);
            // 获取起始的锚点
            var connection = (ConnectionViewModel)e.Connection;
            // "正在拖动"调用的方法
            this.ViewModel.ConnectionDragging(connection, curDragPoint);
        }

        /// 当用户[完成]拖动锚点连线时触发此事件
        private void networkControl_ConnectionDragCompleted(object sender, ConnectionDragCompletedEventArgs e)
        {
            // ???
            var connectorDraggedOut = (ConnectorViewModel)e.ConnectorDraggedOut;
            var connectorDraggedOver = (ConnectorViewModel)e.ConnectorDraggedOver;
            // 前面存的连线对象
            var newConnection = (ConnectionViewModel)e.Connection;
            this.ViewModel.ConnectionDragCompleted(newConnection, connectorDraggedOut, connectorDraggedOver);
        }

        /// 尝试删除选中的结点时触发此事件
        private void DeleteSelectedNodes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 删除选中的结点
            this.ViewModel.DeleteSelectedNodes();
        }

        /// 尝试创建新结点时触发这个事件
        private void CreateNode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 获取鼠标位置作为新结点位置
            Point newNodeLocation = Mouse.GetPosition(networkControl);
            // 创建新结点
            this.ViewModel.CreateNode("New Node!", newNodeLocation);
        }

        private void SetNodeFalse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.SetNodeFalse();
        }

        private void SetNodeTrue_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.SetNodeTrue();
        }

        private void SetNodeActive_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.SetNodeActive();
        }

        private void Check_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.Check();
        }

        private void Calculate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.Calculate();
        }
    }
}
