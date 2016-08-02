using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.IView;
using BDH.Manage;
using BDH.Log;

namespace BDH.ViewModel
{
    public class NewUserViewModel : ViewModelBase
    {
        public AsyncObservableCollection<Department> DepartmentCollection { get; private set; }
        public AsyncObservableCollection<Role> RoleCollection { get; private set; }

        private INewUserView m_View;
        public NewUserViewModel(INewUserView view)
        {
            this.m_View = view;

            this.DepartmentCollection = new AsyncObservableCollection<Department>();
            var depart_list = DepartmentManager.GetDepartmentCollection();
            depart_list.ForEach((p) => this.DepartmentCollection.Add(p));

            this.RoleCollection = new AsyncObservableCollection<Role>();
            var role_list = RoleManager.GetRoleCollection();
            role_list.ForEach((p) => this.RoleCollection.Add(p));
        }

        private string m_LoginName;
        public string LoginName
        {
            get { return this.m_LoginName; }
            set { this.m_LoginName = value; this.OnPropertyChanged(nameof(LoginName)); }
        }

        private string m_Pwd;
        public string FirstPassword
        {
            get { return this.m_Pwd; }
            set { this.m_Pwd = value; this.OnPropertyChanged(nameof(FirstPassword)); }
        }

        private string m_CfmPwd;
        public string ConfirmPassword
        {
            get { return this.m_CfmPwd; }
            set { this.m_CfmPwd = value; this.OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        private Department m_SelectedDepart;
        public Department SelectedDepartment
        {
            get { return this.m_SelectedDepart; }
            set { this.m_SelectedDepart = value; this.OnPropertyChanged(nameof(SelectedDepartment)); }
        }

        private Role m_SelectedRole;
        public Role SelectedRole
        {
            get { return this.m_SelectedRole; }
            set { this.m_SelectedRole = value; this.OnPropertyChanged(nameof(SelectedRole)); }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand((param) =>
                  {
                      if (string.IsNullOrWhiteSpace(this.LoginName) ||
                      string.IsNullOrWhiteSpace(this.FirstPassword) ||
                      string.IsNullOrWhiteSpace(this.ConfirmPassword) ||
                      this.SelectedDepartment == null || this.SelectedRole == null)
                      {
                          this.m_View.PopupMessage.ShowErrorMessage("请将用户名称、密码、部门、角色完整填写和选择", "创建新用户");
                          return;
                      }

                      if (!this.FirstPassword.Equals(this.ConfirmPassword))
                      {
                          this.m_View.PopupMessage.ShowErrorMessage("两次输入的密码不一致，请重新输入", "创建新用户");
                          this.FirstPassword = this.ConfirmPassword = "";
                          return;
                      }

                      int count = UserManager.GetCount();

                      //User item = new User()
                      //{
                      //    ID = (count + 1).ToString(),
                      //    Name = this.LoginName,
                      //    LoginName = this.LoginName,
                      //    Password = this.FirstPassword,
                      //    Department = this.SelectedDepartment,
                      //    Role = this.SelectedRole,
                      //    State = UserState.OnDuty
                      //};

                      //bool suc = UserManager.AddUser(item);
                      //if (suc)
                      //{
                      //    this.m_View.SetReturn(true, item);
                      //    LoggerFactory.GetLogger().WriteInfo("",
                      //      string.Format("添加新用户[{0}]成功", item.LoginName), "添加新用户");
                      //}
                      //else
                      //{
                      //    string msg = string.Format("添加新用户[{0}]失败", item.LoginName);
                      //    LoggerFactory.GetLogger().WriteError("", msg, "添加新用户");
                      //    this.m_View.PopupMessage.ShowErrorMessage(msg, "添加新用户");
                      //}
                  });
            }
        }
    }
}
