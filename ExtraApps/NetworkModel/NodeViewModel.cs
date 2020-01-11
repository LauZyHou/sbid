using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Utils;

namespace NetworkModel
{
    /// <summary>
    /// Defines a node in the view-model.
    /// 定义ViewModel中的结点Node
    /// Nodes are connected to other nodes through attached connectors (aka connection points).
    /// Node根据Connector连接到其它结点
    /// </summary>
    public class NodeViewModel : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// The name of the node.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// The X coordinate for the position of the node.
        /// </summary>
        private double x = 0;

        /// <summary>
        /// The Y coordinate for the position of the node.
        /// </summary>
        private double y = 0;

        /// <summary>
        /// Set to 'true' when the node is selected.
        /// </summary>
        private bool isSelected = false;

        /// <summary>
        /// List of input connectors (connections points) attached to the node.
        /// </summary>
        private ImpObservableCollection<ConnectorViewModel> connectors = null;
        private string color;
        public ConditionType condition = ConditionType.OTHERS;
        public bool IsActive = true;
        public List<NodeViewModel> ChildNodes = new List<NodeViewModel>();
        public List<NodeViewModel> ParentNodes = new List<NodeViewModel>();
 

        public enum ConditionType
        {
            TRUE, FALSE, ACTIVE, OTHERS
        }

        public string Condition
        {
            get
            {
                if (this.condition == ConditionType.OTHERS)
                {
                    return "";
                }
                else if (this.condition == ConditionType.ACTIVE)
                {
                    return "活动";
                }
                else return this.condition.ToString();
            }

            set
            {
                if (value.Equals("False"))
                {
                    this.condition = ConditionType.FALSE;
                }
                else if (value.Equals("True"))
                {
                    this.condition = ConditionType.TRUE;
                }
                else if (value.Equals("Active"))
                {
                    this.condition = ConditionType.ACTIVE;
                }
                else
                {
                    this.condition = ConditionType.OTHERS;
                }
                OnPropertyChanged("Condition");
            }
        }



        #endregion Internal Data Members

        public NodeViewModel()
        {

        }

        public NodeViewModel(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// The name of the node.
        /// Node的名字
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;

                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// The X coordinate for the position of the node.
        /// Node的横坐标位置
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// The Y coordinate for the position of the node.
        /// Node的纵坐标位置
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// List of connectors (connection anchor points) attached to the node.
        /// Connector的列表，连接附属在Node上的锚点(anchor point)
        /// </summary>
        public ImpObservableCollection<ConnectorViewModel> Connectors
        {
            get
            {
                if (connectors == null)
                {
                    connectors = new ImpObservableCollection<ConnectorViewModel>();
                    connectors.ItemsAdded += new EventHandler<CollectionItemsChangedEventArgs>(connectors_ItemsAdded);
                    connectors.ItemsRemoved += new EventHandler<CollectionItemsChangedEventArgs>(connectors_ItemsRemoved);
                }

                return connectors;
            }
        }

        /// <summary>
        /// A helper property that retrieves a list (a new list each time) of all connections attached to the node. 
        /// 一个辅助属性，用于获取Connectors列表中当前有连接的那些ConnectionViewModel
        /// </summary>
        public ICollection<ConnectionViewModel> AttachedConnections
        {
            get
            {
                List<ConnectionViewModel> attachedConnections = new List<ConnectionViewModel>();

                foreach (var connector in this.Connectors)
                {
                    if (connector.AttachedConnection != null)
                    {
                        attachedConnections.Add(connector.AttachedConnection);
                    }
                }

                return attachedConnections;
            }
        }

        /// <summary>
        /// Set to 'true' when the node is selected.
        /// 当结点被选中时，这个属性设置为true
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected == value)
                {
                    return;
                }

                isSelected = value;

                OnPropertyChanged("IsSelected");
            }
        }

        // 结点的颜色
        public string Color { get => color; set => color = value; }

        #region Private Methods

        /// <summary>
        /// Event raised when connectors are added to the node.
        /// 当要连线时触发这个事件，connector添加到这个结点
        /// </summary>
        private void connectors_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (ConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = this;
            }
        }

        /// <summary>
        /// Event raised when connectors are removed from the node.
        /// 当要取消连线时，将connector设置为null，即认为移除
        /// </summary>
        private void connectors_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (ConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = null;
            }
        }

        #endregion Private Methods
    }
}
