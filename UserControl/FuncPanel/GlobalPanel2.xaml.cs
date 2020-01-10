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
using sbid.UI;
using NetworkModel;
using NetworkUI;

namespace sbid.UserControl
{
    /// <summary>
    /// GlobalPanel2.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalPanel2 : System.Windows.Controls.UserControl
    {
        // 每次创建时自增
        private int processId = 1;
        private int userTypeId = 1;
        private int count = 1;
        public GlobalPanel2()
        {
            InitializeComponent();
        }

        // 全局面板2的ViewModel
        public GlobalPanel2VM ViewModel
        {
            get
            {
                return (GlobalPanel2VM)this.scrollViewer.DataContext;
            }
        }

        #region 按钮控制

        //[按钮]添加进程模板
        private void Button_Click_Process(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatProcessVM(processId, new Point(100+count*30, 100+count*30));
            this.processId += 1;
            this.count++;

        }

        //[按钮]添加自定义类型
        private void Button_Click_UserType(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreateUserTypeVM(userTypeId, new Point(100+count*30, 100+count*30));
            this.userTypeId += 1;
            this.count++;
        }

        #endregion

        #region 命令的执行函数
        /// 删除选中的结点
        private void DeleteSelectedNodes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 删除选中的结点
            this.ViewModel.DeleteSelectedNodes();
        }

        /// 创建新的结点(类图)
        private void CreateNode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 获取鼠标位置作为新结点位置
            Point newNodeLocation = Mouse.GetPosition(networkControl);
            // 创建新结点
            this.ViewModel.CreateNode("New Node!", newNodeLocation);
        }

        /// 编辑类图(创建并打开相应对话框)
        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 判断具体是在哪种类图上点击的右键编辑，即判断选中的类图
            var nodesCopy = this.ViewModel.Network.Nodes.ToArray(); 
            foreach (var node in nodesCopy)
            {
                if (node.IsSelected)
                {
                    if (node is ProcessVM)
                    {
                        new ProcessWindow(node.Name).ShowDialog();
                    }
                    else if (node is UserTypeVM)
                    {
                        new UserTypeWindow(node.Name).ShowDialog();
                    }
                    else 
                    {
                        DemoWindow demoWindow = new DemoWindow();
                        demoWindow.ShowDialog();
                    }
                    break;
                }
            }
        }

        /// 编辑状态机(创建并打开相应对话框)
        private void EditStateMachine_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 判断具体是在哪个上点击的右键编辑，即判断选中的类图
            var nodesCopy = this.ViewModel.Network.Nodes.ToArray();
            foreach (var node in nodesCopy)
            {
                if (node.IsSelected)
                {
                    if (node is ProcessVM)
                    {
                        new StateMachineWindow(node.Name).ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("错误!");
                    }
                    break;
                }
            }
        }
        #endregion

        #region 用于NewworkView的事件回调
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

        #endregion
    }
}
