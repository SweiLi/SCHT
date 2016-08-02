using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BDH.Manage;
using BDH.IView;
using BDH.ViewModel;

namespace BridgeDetectHelper
{
    /// <summary>
    /// BridgeMgrMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class BridgeMgrMainPage :Page, IBridgeMgrView
    {
        public BridgeMgrMainPage()
        {
            InitializeComponent();
            this.m_PopupMsg = new PopupMessage(); //初始化弹出对话框
            this.DataContext = new BridgeMgrViewModel(this);
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public ChildBridge GetNewChildBridge()
        {
            AddBridgeWindow abw = new AddBridgeWindow();
            abw.Owner = Window.GetWindow(this);
            if (abw.ShowDialog().Value)
            {
                return abw.NewChildBridge;
            }

            return null;
        }

        public ChildBridge GetEditedChildBridge(ChildBridge child)
        {
            AddBridgeWindow abw = new AddBridgeWindow(child);
            abw.Owner = Window.GetWindow(this);
            if (abw.ShowDialog().Value)
            {
                return abw.NewChildBridge;
            }

            return null;
        }
    }
}
