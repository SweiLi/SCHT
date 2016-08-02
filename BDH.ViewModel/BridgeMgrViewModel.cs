using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Manage;
using BDH.IView;
using System.Windows.Input;

namespace BDH.ViewModel
{
    public class BridgeMgrViewModel : ViewModelBase
    {
        public AsyncObservableCollection<ChildBridge>  ChildBridgeCollection { get; private set; }

        private IBridgeMgrView m_View;

        public BridgeMgrViewModel(IBridgeMgrView view)
        {
            this.m_View = view;
            this.ChildBridgeCollection = new AsyncObservableCollection<ChildBridge>();
            var bcs = BridgeSystemManage.GetChildBridges();
            bcs.ForEach((p)=>this.ChildBridgeCollection.Add(p));
        }

        public ICommand AddChildBridgeCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    var child = this.m_View.GetNewChildBridge();
                    if (child == null) return;

                    //保存桥梁和子桥梁信息
                    var bdg = new Bridge() { Id = child.ParentId, IsCancel = 1 };
                    if (!BridgeSystemManage.ExistsBridge(bdg.Id))
                        BridgeSystemManage.AddBridge(bdg);
                    BridgeSystemManage.AddChildBridge(child);

                    this.ChildBridgeCollection.Add(child);
                });
            }
        }

        public ICommand QueryBridgeCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    var tp = (Tuple<string, string>)param;

                    this.ChildBridgeCollection.Clear();

                    var items = BridgeSystemManage.GetChildBridges(tp.Item1, tp.Item2);
                    items.ForEach((p) => this.ChildBridgeCollection.Add(p));
                });
            }
        }

        private ChildBridge m_SltBdg;
        public ChildBridge SelectedChildBridge
        {
            get { return this.m_SltBdg; }
            set { this.m_SltBdg = value; this.OnPropertyChanged(nameof(SelectedChildBridge)); }
        }

        public ICommand EditBridgeCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    if (param == null) return;

                    var child = param as ChildBridge;
                    var new_child = this.m_View.GetEditedChildBridge(child);
                    if (new_child != null)
                        BridgeSystemManage.UpdateChildBridge(new_child);
                });
            }
        }

        public ICommand RemoveBridgeCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    if (param == null) return;

                    var child = param as ChildBridge;
                    string msg = string.Format("确定要删除桥梁[{0}]？", child.Name);
                    if (this.m_View.PopupMessage.ShowQuestion(msg, "删除桥梁"))
                    {
                        bool suc = BridgeSystemManage.DeleteChildBridge(child.Id);
                        if (suc) this.ChildBridgeCollection.Remove(child);
                        else this.m_View.PopupMessage.ShowErrorMessage("删除桥梁时发生错误", "删除桥梁");
                    }
                });
            }
        }
    }
}
