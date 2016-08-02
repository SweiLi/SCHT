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
using System.Windows.Shapes;
using BDH.IView;
using BDH.ViewModel;
using BDH.Manage;

namespace BridgeDetectHelper
{
    /// <summary>
    /// NewUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewUserWindow : Window, INewUserView
    {
        public NewUserWindow()
        {
            InitializeComponent();
            this.m_PopupMsg = new PopupMessage();
            this.DataContext = new NewUserViewModel(this);
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public User GetUser()
        {
            return this.m_NewUser;
        }

        private User m_NewUser;
        public void SetReturn(bool ret_val, User new_user)
        {
            this.m_NewUser = new_user;
            this.DialogResult = ret_val;
        }
    }
}
