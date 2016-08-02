using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.IView;
using BDH.Manage;
using BDH.Config;
using BDH.Log;

namespace BDH.ViewModel
{
    public class SystemMgrViewModel : ViewModelBase
    {
        public AsyncObservableCollection<Role> RoleCollection { get; private set; }
        public AsyncObservableCollection<User> UserCollection { get; private set; }
        private ISystemMgrView m_View;

        public SystemMgrViewModel(ISystemMgrView view)
        {
            this.m_View = view;

            this.RoleCollection = new AsyncObservableCollection<Role>();
            this.UserCollection = new AsyncObservableCollection<User>();

            var role_list = RoleManager.GetRoleCollection();
            role_list.ForEach((p) => this.RoleCollection.Add(p));

            var user_list = UserManager.GetUserCollection();
            user_list.ForEach((p) => this.UserCollection.Add(p));
        }

        public ICommand AddUserCommand
        {
            get
            {
                return new RelayCommand((param) =>
                  {
                      //var new_user = this.m_View.CreateNewUser();
                      //if (new_user == null) return;

                      //this.UserCollection.Add(new_user);
                      this.m_View.GetNewProject();
                  });
            }
        }

        public ICommand AddRoleCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    SysSecurityConfig conf = new SysSecurityConfig();
                    //conf.LogTableName = "mdtbl_log";
                    //conf.LogTextFilePath = "log";
                    //conf.Save();
                    Console.WriteLine("log path=" + conf.LogTextFilePath);
                    Console.WriteLine("log table=" + conf.LogTableName);
                });
            }
        }
    }
}
