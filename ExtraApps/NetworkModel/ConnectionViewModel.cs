using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace NetworkModel
{
    /// <summary>
    /// Defines a connection between two connectors (aka connection points) of two nodes.
    /// 两个锚点之间的连接
    /// </summary>
    public sealed class ConnectionViewModel : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel sourceConnector = null;

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel destConnector = null;

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// </summary>
        private Point sourceConnectorHotspot;
        private Point destConnectorHotspot;

        #endregion Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// 源锚点
        /// </summary>
        public ConnectorViewModel SourceConnector
        {
            get
            {
                return sourceConnector;
            }
            set
            {
                if (sourceConnector == value)
                {
                    return;
                }

                if (sourceConnector != null)
                {
                    Trace.Assert(sourceConnector.AttachedConnection == this);

                    sourceConnector.AttachedConnection = null;
                    sourceConnector.HotspotUpdated -= new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                }

                sourceConnector = value;

                if (sourceConnector != null)
                {
                    Trace.Assert(sourceConnector.AttachedConnection == null);

                    sourceConnector.AttachedConnection = this;
                    sourceConnector.HotspotUpdated += new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                    this.SourceConnectorHotspot = sourceConnector.Hotspot;
                }

                OnPropertyChanged("SourceConnector");
            }
        }

        /// <summary>
        /// The destination connector the connection is attached to.
        /// 目标锚点
        /// </summary>
        public ConnectorViewModel DestConnector
        {
            get
            {
                return destConnector;
            }
            set
            {
                if (destConnector == value)
                {
                    return;
                }

                if (destConnector != null)
                {
                    Trace.Assert(destConnector.AttachedConnection == this);

                    destConnector.AttachedConnection = null;
                    destConnector.HotspotUpdated += new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                }

                destConnector = value;

                if (destConnector != null)
                {
                    Trace.Assert(destConnector.AttachedConnection == null);

                    destConnector.AttachedConnection = this;
                    destConnector.HotspotUpdated += new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                    this.DestConnectorHotspot = destConnector.Hotspot;
                }

                OnPropertyChanged("DestConnector");
            }
        }

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// 源和目标的hotspot用于生成连线
        /// </summary>
        public Point SourceConnectorHotspot
        {
            get
            {
                return sourceConnectorHotspot;
            }
            set
            {
                sourceConnectorHotspot = value;

                OnPropertyChanged("SourceConnectorHotspot");
                OnPropertyChanged("MidConnectorPoint");// 还要通知重新计算中心点
            }
        }

        public Point DestConnectorHotspot
        {
            get
            {
                return destConnectorHotspot;
            }
            set
            {
                destConnectorHotspot = value;

                OnPropertyChanged("DestConnectorHotspot");
                OnPropertyChanged("MidConnectorPoint");
            }
        }

        // 额外添加一个计算出的中心点
        public Point MidConnectorPoint
        {
            get
            {
                return new Point(
                    (this.destConnectorHotspot.X + this.sourceConnectorHotspot.X) / 2,
                    (this.destConnectorHotspot.Y + this.sourceConnectorHotspot.Y) / 2
                    );
            }
        }

        #region Private Methods

        /// <summary>
        /// Event raised when the hotspot of the source connector has been updated.
        /// 源锚点hotspot更新
        /// </summary>
        private void sourceConnector_HotspotUpdated(object sender, EventArgs e)
        {
            this.SourceConnectorHotspot = this.SourceConnector.Hotspot;
        }

        /// <summary>
        /// Event raised when the hotspot of the dest connector has been updated.
        /// 目标锚点hotspot更新
        /// </summary>
        private void destConnector_HotspotUpdated(object sender, EventArgs e)
        {
            // [bugfix]当A到B结点的连线取消后,对B结点的拖动会报出this.DestConnector为空的异常
            if (this.DestConnector == null)
                return;
            this.DestConnectorHotspot = this.DestConnector.Hotspot;
        }

        #endregion Private Methods
    }
}
