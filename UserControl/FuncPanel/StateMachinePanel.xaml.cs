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
using System.Windows.Threading;
using sbid.Model;
using sbid.UI;
using System.Collections.ObjectModel;

namespace sbid.UserControl
{
    /// <summary>
    /// StateMachinePanel.xaml 的交互逻辑
    /// </summary>
    public partial class StateMachinePanel : System.Windows.Controls.UserControl
    {
        // 在创建面板时需要传入状态机的VM,是新建的还是拿旧的由调用者决定,而不在这里创建状态机的VM
        public StateMachinePanel(StateMachineVM stateMachineVM)
        {
            InitializeComponent();
            // 这里代替xaml中注释掉的DataContent
            this.scrollViewer.DataContext = new StateMachinePanelVM(stateMachineVM);
            // 提示条
            ResourceManager.tipTextBlock.Text = "双击转移边上的Action以进行编辑";
        }

        //状态机窗体的ViewModel
        public StateMachinePanelVM ViewModel
        {
            get
            {
                return (StateMachinePanelVM)this.scrollViewer.DataContext;
            }
        }

        #region 拖动锚点连线

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
            var connection = (TransitionVM)e.Connection;
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
            var newConnection = (TransitionVM)e.Connection;
            this.ViewModel.ConnectionDragCompleted(newConnection, connectorDraggedOut, connectorDraggedOver);
        }

        #endregion 拖动锚点连线

        #region 命令操作

        /// 尝试删除选中的结点时触发此事件
        private void DeleteSelectedNodes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 删除选中的结点
            this.ViewModel.DeleteSelectedNodes();
        }

        /// 尝试创建新状态时触发这个事件
        private void CreateState_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 获取鼠标位置作为新结点位置
            Point newNodeLocation = Mouse.GetPosition(networkControl);
            this.ViewModel.CreateState(newNodeLocation);
        }

        /// 尝试创建新终止状态时触发这个事件
        private void CreateFinalState_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 获取鼠标位置作为新结点位置
            Point newNodeLocation = Mouse.GetPosition(networkControl);
            this.ViewModel.CreateFinalState(newNodeLocation);
        }

        #endregion 命令操作

        #region 键盘事件处理

        private void Panel_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            // 要打开编辑边的窗口
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Enter) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
                // 获取当前的转移关系的ViewModel
                TransitionVM t = this.ViewModel.FindTransitionVM_ByCheckNode();
                if (t != null)
                {
                    // 打开编辑箭头(Gurad和Action)的窗口,传入当前的转移关系的ViewModel以对其进行编辑
                    new ArrowEditWindow(t).ShowDialog();
                }
            }
            */
        }

        #endregion 键盘事件处理

        #region 鼠标事件处理
        
        // 双击边附近显示Acitons的ListBox
        private void ActionListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            // 传入Actions以在窗体打开后对其进行修改
            new ActionsEditWindow((ObservableCollection<string>)listBox.ItemsSource).Show();
        }

        #endregion 鼠标事件处理
    }
}
