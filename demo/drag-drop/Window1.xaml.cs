using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sbid.demo.drag_drop
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool _isDown;
        private bool _isDragging;
        private Canvas _myCanvas;
        private UIElement _originalElement;
        private double _originalLeft;
        private double _originalTop;
        //选中时边角效果
        private SimpleCircleAdorner _overlayElement;
        private Point _startPoint;

        //页面加载时触发此方法
        public void OnPageLoad(object sender, RoutedEventArgs e)
        {
            //创建一个canvas对象
            _myCanvas = new Canvas();

            //创建一矩形，设置宽高和填充色
            var rect1 = new Rectangle();
            rect1.Height = rect1.Width = 32;
            rect1.Fill = Brushes.Blue;

            //通过设置到顶部和左侧的距离设置矩形的初始位置
            Canvas.SetTop(rect1, 8);
            Canvas.SetLeft(rect1, 8);

            //创建一个textbox，设置其内容，初始位置
            var tb = new TextBox { Text = "This is a TextBox. Drag and drop me" };
            Canvas.SetTop(tb, 100);
            Canvas.SetLeft(tb, 100);

            //在canvas绘图对象中添加刚刚创建好的矩形和textbox
            //这部分可以用于实现点击按钮就在画图上添加一个物体
            _myCanvas.Children.Add(rect1);
            _myCanvas.Children.Add(tb);

            //为画布设置鼠标左键按下、鼠标移动、鼠标左键放开调用的方法
            _myCanvas.PreviewMouseLeftButtonDown += MyCanvas_PreviewMouseLeftButtonDown;
            _myCanvas.PreviewMouseMove += MyCanvas_PreviewMouseMove;
            _myCanvas.PreviewMouseLeftButtonUp += MyCanvas_PreviewMouseLeftButtonUp;
            //这里是this.，即为当前窗体添加键盘按下的事件
            PreviewKeyDown += window1_PreviewKeyDown;

            //在栈布局中加入canvas
            myStackPanel.Children.Add(_myCanvas);
        }

        //当前窗体键盘按下的事件
        private void window1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //如果正拖拽中，且按下ESC键
            if (e.Key == Key.Escape && _isDragging)
            {
                //结束拖拽，传入参数为true表示取消拖拽，图形会保持在拖拽前的位置
                DragFinished(true);
            }
        }

        //在canvas上左键松开时
        private void MyCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //如果左键本来是按下状态的
            if (_isDown)
            {
                //结束拖拽，传入参数为false表示不是取消拖拽，即图形会留在拖拽后的位置
                DragFinished(false);
                //将事件标记为已处理的值，防止其它地方再处理这个事件
                e.Handled = true;
            }
        }

        //用于结束拖拽的函数，传入是否取消拖拽的参数
        private void DragFinished(bool cancelled)
        {
            //将鼠标输入捕获到指定元素，这里给个null也就是取消焦点的作用
            Mouse.Capture(null);
            //如果正在拖拽中
            if (_isDragging)
            {
                //移除控件身上的变角效果
                AdornerLayer.GetAdornerLayer(_overlayElement.AdornedElement).Remove(_overlayElement);

                //如果不是取消拖拽
                if (cancelled == false)
                {
                    //设置位置为新位置，也即原来的位置加上偏移量
                    Canvas.SetTop(_originalElement, _originalTop + _overlayElement.TopOffset);
                    Canvas.SetLeft(_originalElement, _originalLeft + _overlayElement.LeftOffset);
                }
                //这个属性不再记录当前要操作的元素
                _overlayElement = null;
            }
            //拖拽状态和鼠标按下状态取消
            _isDragging = false;
            _isDown = false;
        }

        //在canvas上鼠标移动时
        private void MyCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //仅当在按下状态时才能拖动图形
            if (_isDown)
            {
                //如果本来没在拖拽状态，且上下或左右可拖拽范围不小于其最小值
                if ((_isDragging == false) &&
                    ((Math.Abs(e.GetPosition(_myCanvas).X - _startPoint.X) >
                      SystemParameters.MinimumHorizontalDragDistance) ||
                     (Math.Abs(e.GetPosition(_myCanvas).Y - _startPoint.Y) >
                      SystemParameters.MinimumVerticalDragDistance)))
                {
                    //进入拖拽状态
                    DragStarted();
                }
                //如果在拖拽状态
                if (_isDragging)
                {
                    //拖拽中移动
                    DragMoved();
                }
            }
        }

        //进入拖拽状态
        private void DragStarted()
        {
            //拖拽状态设置为true
            _isDragging = true;
            //记录控件的起始位置
            _originalLeft = Canvas.GetLeft(_originalElement);
            _originalTop = Canvas.GetTop(_originalElement);

            //用这个控件构造一个外围的圆角效果
            _overlayElement = new SimpleCircleAdorner(_originalElement);
            //获取到这个控件的外围效果的层
            var layer = AdornerLayer.GetAdornerLayer(_originalElement);
            //在层中添加这个效果
            layer.Add(_overlayElement);
        }

        //在拖拽状态移动
        private void DragMoved()
        {
            var currentPosition = Mouse.GetPosition(_myCanvas);

            _overlayElement.LeftOffset = currentPosition.X - _startPoint.X;
            _overlayElement.TopOffset = currentPosition.Y - _startPoint.Y;
        }

        //在canvas上按下鼠标时
        private void MyCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //如果按下的源是canvas本身，也即点击了空白处，什么都不做
            if (e.Source == _myCanvas)
            {
            }
            //如果按下的源是在控件上
            else
            {
                //仅在这时才设置当前是按下鼠标的状态，之后鼠标移动时靠这个量知道有没有在移动对象
                _isDown = true;
                //记录起始点位置，也可以Mouse.GetPosition(_myCanvas);
                _startPoint = e.GetPosition(_myCanvas);
                //记录选中的控件
                _originalElement = e.Source as UIElement;
                //canvas获取焦点
                _myCanvas.CaptureMouse();
                //将事件标记为已处理的值，防止其它地方再处理这个事件
                e.Handled = true;
            }
        }
    }

    // Adorners must subclass the abstract base class Adorner.
}
