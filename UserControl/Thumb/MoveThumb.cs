using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace sbid.UserControl
{
    //Thumb是用来处理拖放和调整尺寸的控件
    //这里继承它，作一个用于移动的控件
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            //添加处理移动事件的方法
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        //处理移动事件
        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //?获取要操作的图形控件
            Control designerItem = this.DataContext as Control;

            if (designerItem != null)
            {
                //获取在Canvas中的位置
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);

                //设置控件在Canvas中的新位置
                Canvas.SetLeft(designerItem, left + e.HorizontalChange);
                Canvas.SetTop(designerItem, top + e.VerticalChange);
            }
        }
    }
}
