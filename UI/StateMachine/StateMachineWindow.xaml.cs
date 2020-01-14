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
using System.Windows.Shapes;
using sbid.ViewModel;
using NetworkModel;
using NetworkUI;
using System.Windows.Threading;
using sbid.Model;

namespace sbid.UI
{
    /// <summary>
    /// StateMachineWindow.xaml 的交互逻辑,同时作为状态机的ViewModel
    /// </summary>
    public partial class StateMachineWindow : Window
    {
        // 需集成状态机的数据对象
        public StateMachine stateMachine = new StateMachine("testlzhfst");
        // 记录鼠标点击的次数,用于判断双击事件
        private int clkNum = 0;

        public StateMachineWindow()
        {
            InitializeComponent();
        }

        public StateMachineWindow(string suffixName)
        {
            InitializeComponent();
            this.Title += suffixName;
        }

        //状态机窗体的ViewModel
        public StateMachineWindowVM ViewModel
        {
            get
            {
                return (StateMachineWindowVM)this.DataContext;
            }
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
            this.ViewModel.CreateState("New State!", newNodeLocation);
        }

        /// 尝试创建新终止状态时触发这个事件
        private void CreateFinalState_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 获取鼠标位置作为新结点位置
            Point newNodeLocation = Mouse.GetPosition(networkControl);
            this.ViewModel.CreateFinalState(newNodeLocation);
        }

        #endregion 命令操作

        // [作废]
        //【按下】边上的文本区域
        //private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    // 判断是否是双击，在双击时打开对边上Guard和Action的编辑
        //    clkNum += 1;
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
        //    timer.Tick += (s, e1) => { timer.IsEnabled = false; clkNum = 0; };
        //    timer.IsEnabled = true;
        //    if (clkNum % 2 == 0)
        //    {
        //        timer.IsEnabled = false;
        //        clkNum = 0;
        //        //// 获取当前的转移关系的ViewModel
        //        //TransitionVM t = this.ViewModel.FindTransitionVM_ByCheckNode();
        //        //// 打开编辑箭头(Gurad和Action)的窗口
        //        //new ArrowEditWindow().ShowDialog();
        //    }
        //}

        // 键盘事件处理
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
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
        }
    }
}
