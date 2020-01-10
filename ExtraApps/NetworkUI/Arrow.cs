using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace NetworkUI
{
    /// <summary>
    /// 简单直线箭头类
    /// </summary>
    public class Arrow : Shape
    {
        #region Dependency Property/Event Definitions

        public static readonly DependencyProperty ArrowHeadLengthProperty =
            DependencyProperty.Register("ArrowHeadLength", typeof(double), typeof(Arrow),
                new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ArrowHeadWidthProperty =
            DependencyProperty.Register("ArrowHeadWidth", typeof(double), typeof(Arrow),
                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty DotSizeProperty =
            DependencyProperty.Register("DotSize", typeof(double), typeof(Arrow),
                new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(Point), typeof(Arrow),
                new FrameworkPropertyMetadata(new Point(0.0, 0.0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(Point), typeof(Arrow),
                new FrameworkPropertyMetadata(new Point(0.0, 0.0), FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion Dependency Property/Event Definitions

        /// <summary>
        /// 箭头头部的长度
        /// </summary>
        public double ArrowHeadLength
        {
            get
            {
                return (double)GetValue(ArrowHeadLengthProperty);
            }
            set
            {
                SetValue(ArrowHeadLengthProperty, value);
            }
        }

        /// <summary>
        /// 箭头头部的宽度
        /// </summary>
        public double ArrowHeadWidth
        {
            get
            {
                return (double)GetValue(ArrowHeadWidthProperty);
            }
            set
            {
                SetValue(ArrowHeadWidthProperty, value);
            }
        }

        /// <summary>
        /// 箭头起始点的尺寸
        /// </summary>
        public double DotSize
        {
            get
            {
                return (double)GetValue(DotSizeProperty);
            }
            set
            {
                SetValue(DotSizeProperty, value);
            }
        }

        /// <summary>
        /// 箭头起始点位置
        /// </summary>
        public Point Start
        {
            get
            {
                return (Point)GetValue(StartProperty);
            }
            set
            {
                SetValue(StartProperty, value);
            }
        }

        /// <summary>
        /// 箭头终点位置
        /// </summary>
        public Point End
        {
            get
            {
                return (Point)GetValue(EndProperty);
            }
            set
            {
                SetValue(EndProperty, value);
            }
        }

        #region Private Methods

        /// <summary>
        /// Return the shape's geometry.
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                //
                // Geometry has not yet been generated.
                // Generate geometry and cache it.
                //
                LineGeometry geometry = new LineGeometry();
                geometry.StartPoint = this.Start;
                geometry.EndPoint = this.End;

                GeometryGroup group = new GeometryGroup();
                group.Children.Add(geometry);

                GenerateArrowHeadGeometry(group);

                //
                // Return cached geometry.
                //
                return group;
            }
        }

        /// <summary>
        /// Generate the geometry for the three optional arrow symbols at the start, middle and end of the arrow.
        /// 在箭头的开始、中间和结束处为三个可选的箭头符号生成几何图形。
        /// </summary>
        private void GenerateArrowHeadGeometry(GeometryGroup geometryGroup)
        {
            // 【1】在起始点this.Start位置创建一个DotSize大小的圆形
            EllipseGeometry ellipse = new EllipseGeometry(this.Start, DotSize, DotSize);
            // 添加到图形组中
            geometryGroup.Children.Add(ellipse);

            // 从开始位置指向结束为止的向量
            Vector startDir = this.End - this.Start;
            startDir.Normalize();
            // 终点位置-标准化向量*箭头头部的长度=箭头头部开始的位置坐标
            Point basePoint = this.End - (startDir * ArrowHeadLength);
            // 求"从开始位置指向结束为止的向量"的法向量,它和原向量是垂直的,并且也是一个标准向量
            Vector crossDir = new Vector(-startDir.Y, startDir.X);

            // 用于确定箭头头部等腰三角的三个点
            Point[] arrowHeadPoints = new Point[3];
            // 箭头头部的等腰三角的尖
            arrowHeadPoints[0] = this.End;
            // 沿着法向量方向及其反方向,走箭头头部宽度一半长度,也就得到了箭头头部小三角的底座的两个点位置
            arrowHeadPoints[1] = basePoint - (crossDir * (ArrowHeadWidth / 2));
            arrowHeadPoints[2] = basePoint + (crossDir * (ArrowHeadWidth / 2));

            // 【2】构建箭头头部的小等腰三角形的图形
            PathFigure arrowHeadFig = new PathFigure(); // 路径绘图
            arrowHeadFig.IsClosed = true; // 闭合
            arrowHeadFig.IsFilled = true; // 填充
            arrowHeadFig.StartPoint = arrowHeadPoints[0]; // 从三角形的尖开始绘制
            arrowHeadFig.Segments.Add(new LineSegment(arrowHeadPoints[1], true)); // 到1位置
            arrowHeadFig.Segments.Add(new LineSegment(arrowHeadPoints[2], true)); // 再到2位置
            // 构造到路径图形里
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(arrowHeadFig);
            // 添加到图形组中
            geometryGroup.Children.Add(pathGeometry);

            // 【3】在线段中间添加文字
            // 计算线段中点
            double midX = (this.Start.X + this.End.X) / 2;
            double midY = (this.Start.Y + this.End.Y) / 2;
            // 给定矩形的宽高
            int myW = 30;
            int myH = 20;
            //EllipseGeometry lzh = new EllipseGeometry(middlePoint, DotSize, DotSize);
            RectangleGeometry lzh = new RectangleGeometry(new Rect(midX - myW / 2, midY - myH / 2, myW, myH));
            // 添加到图形组中
            geometryGroup.Children.Add(lzh);


        }

        #endregion Private Methods
    }
}
