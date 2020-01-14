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
using sbid.Model;

namespace sbid.UserControl
{
    /// <summary>
    /// GlobalPanel2.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalPanel2 : System.Windows.Controls.UserControl
    {
        // 每次创建时自增
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

        // [按钮]测试
        private void Button_Click_Test(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Test_Node();
        }

        // [按钮]添加进程模板
        private void Button_Click_Process(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreatProcessVM(new Point(100+count*30, 100+count*30));
            this.count++;

        }
        // [按钮]添加UserType
        private void Button_Click_UserType(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreateUserType2VM("初始化"+count, new Point(100 + count * 30, 100 + count * 30));
            this.count++;
        }

        // [按钮]添加SecurityProperty
        private void Button_Click_SecurityProperty(object sender, RoutedEventArgs e)
        {
            this.ViewModel.CreateSecurityPropertyVM("初始化" + count, new Point(100 + count * 30, 100 + count * 30));
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
                        new ProcessWindow((ProcessVM)node).ShowDialog();
                    }
                    else if (node is UserType2VM)
                    {
                        new UserTypeWindow((UserType2VM)node).ShowDialog();
                    }
                    else if (node is SecurityPropertyVM)
                    {
                        new SecurityPropertyWindow((SecurityPropertyVM)node).ShowDialog();                     
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


        /// 编辑状态机第二版(创建并打开相应Tab)
        private void EditStateMachine2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 越过第一层选项卡
            CloseableTabItem closeableTabItem1= bigGrid.Parent as CloseableTabItem;
            TabControl tabControl1 = closeableTabItem1.Parent as TabControl;

            // 越过第二层选项卡
            CloseableTabItem closeableTabItem2 = tabControl1.Parent as CloseableTabItem;
            TabControl tabControl2 = closeableTabItem2.Parent as TabControl;

            // 越过两层Grid
            Grid grid1 = tabControl2.Parent as Grid;
            Grid grid2 = grid1.Parent as Grid;

            // 获取到MainWindow
            MainWindow mainWindow = grid2.Parent as MainWindow;

            // 判断选中的ProcessVM,从中取出Process的Name
            string pName = null;
            Process nowProcess = null;
            int selectedPNum = 0;
            var nodesCopy = this.ViewModel.Network.Nodes.ToArray();
            foreach (var node in nodesCopy)
            {
                if (node.IsSelected)
                {
                    if (node is ProcessVM)
                    {
                        pName = ((ProcessVM)node).Process.Name;
                        selectedPNum += 1;
                        nowProcess = ((ProcessVM)node).Process;
                    }
                }
            }
            if (pName == null)
            {
                MessageBox.Show("需要选中一个Process");
                return;
            }
            if (selectedPNum > 1)
            {
                MessageBox.Show("选中了过多的Process");
                return;
            }

            // todo: 新 / 旧ViewModel
            StateMachineVM stateMachineVM = new StateMachineVM("init", nowProcess);// 构造时写入nowProcess
            // todo:打开新面板 / 跳到旧面板
            mainWindow.add_new_panel("Process\""+pName+"\"的状态机", 
                new StateMachinePanel(stateMachineVM).Content as Grid);
        }

        #endregion 命令的执行函数

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

        private void attributeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
