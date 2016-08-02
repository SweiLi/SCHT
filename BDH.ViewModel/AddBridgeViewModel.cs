using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Manage;
using BDH.IView;
using System.Windows.Input;

namespace BDH.ViewModel
{
    public class AddBridgeViewModel : ViewModelBase
    {
        private IAddBridgeView m_View;
        public ObservableCollection<BridgeType> BridgeTypeCollection { get; private set; }
        /// <summary>
        /// 显示桥梁数据
        /// </summary>
        /// <param name="view"></param>
        public AddBridgeViewModel(IAddBridgeView view)
        {
            this.m_View = view;
            this.BridgeTypeCollection = new ObservableCollection<BridgeType>();
            var bt_list = BridgeSystemManage.GetBridgeTypes();
            bt_list.ForEach((p) => this.BridgeTypeCollection.Add(p));

            this.BridgeId = BridgeSystemManage.CreateChildBridgeId();
        }

        public AddBridgeViewModel(IAddBridgeView view, ChildBridge child) : this(view)
        {
            this.WindowTitle = "编辑桥梁";
            this.BridgeId = child.Id;
            this.BridgeName = child.Name;
            this.BridgePile = child.PileId;
            this.BridgeType = this.BridgeTypeCollection.First((p) => p.Id.Equals(child.TypeId));
        }

        private string m_WinTitle;
        public string WindowTitle
        {
            get { return this.m_WinTitle; }
            set { this.m_WinTitle = value; this.OnPropertyChanged(nameof(WindowTitle)); }
        }

        /// <summary>
        /// View返回桥梁名称和桥梁ID
        /// </summary>
        private String m_BridgeName;
        public string BridgeName
        {
            get { return this.m_BridgeName; }
            set { this.m_BridgeName = value; this.OnPropertyChanged(nameof(BridgeName)); }
        }

        private String m_BridgeId;
        public string BridgeId
        {
            get { return this.m_BridgeId; }
            set { this.m_BridgeId = value; this.OnPropertyChanged(nameof(BridgeId)); }
        }

        private String m_BridgePile;
        public string BridgePile
        {
            get { return this.m_BridgePile; }
            set { this.m_BridgePile = value; this.OnPropertyChanged(nameof(BridgePile)); }
        }

        private BridgeType m_BridgeType;
        public BridgeType BridgeType
        {
            get { return this.m_BridgeType; }
            set { this.m_BridgeType = value; this.OnPropertyChanged(nameof(BridgeType)); }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                   if (string.IsNullOrWhiteSpace(this.BridgeName) ||
                        string.IsNullOrWhiteSpace(this.BridgePile) ||
                        this.BridgeType == null)
                    {
                        this.m_View.PopupMessage.ShowErrorMessage("请将桥梁的信息补充完整", "新建桥梁");
                        return;
                    }

                    var bdg_id = BridgeSystemManage.CreateBridgeId();
                    this.m_View.NewChildBridge = new ChildBridge()
                    {
                        Id = this.BridgeId,
                        Name = this.BridgeName,
                        ParentId = bdg_id,
                        TypeId = this.BridgeType.Id,
                        PileId = this.BridgePile
                    };

                    this.m_View.SavedClose();
                });
            }
        }

    }
}
