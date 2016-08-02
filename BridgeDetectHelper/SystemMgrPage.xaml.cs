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
using BDH.IView;
using BDH.ViewModel;
using BDH.Manage;

namespace BridgeDetectHelper
{
    /// <summary>
    /// SystemMgrPage.xaml 的交互逻辑
    /// </summary>
    public partial class SystemMgrPage : Page, ISystemMgrView
    {
        public SystemMgrPage()
        {
            InitializeComponent();
            this.m_PopupMsg = new PopupMessage();
            this.DataContext = new SystemMgrViewModel(this);
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public User CreateNewUser()
        {
            NewUserWindow win = new NewUserWindow();
            win.Owner = Window.GetWindow(this);
            if (win.ShowDialog().Value)
                return win.GetUser();
            else
                return null;
        }

        public void GetNewProject()
        {
            AddProjectWindow win = new AddProjectWindow();
            win.Owner = Window.GetWindow(this);
            win.ShowDialog();
        }
    }
}
