using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.Manage;
using BDH.IView;

namespace BDH.ViewModel
{
    public class AddProjectViewModel : ViewModelBase
    {
        private IAddProjectView m_View;
        public AddProjectViewModel(IAddProjectView view)
        {
            this.m_View = view;
        }
        //1.界面上有什么数据，text combox等
        //2.把每个数据定义成一个成员属性，并与界面进行绑定；
        private string m_name;
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; this.OnPropertyChanged(nameof(Name)); }
        }
        private string m_master;
        public String Master
        {
            get { return this.m_master; }
            set { this.m_master = value; this.OnPropertyChanged(nameof(Master)); }
        }

        private String m_beginTime;
        public String BeginTime
        {
            get { return this.m_beginTime; }
            set { this.m_beginTime = value; this.OnPropertyChanged(nameof(BeginTime)); }
        }

        private string m_EndTime;
        public string EndTime
        {
            get { return this.m_EndTime; }
            set { this.m_EndTime = value; this.OnPropertyChanged(nameof(EndTime)); }
        }

        private string m_ContractNum;
        public string ContractNumber
        {
            get { return this.m_ContractNum; }
            set { this.m_ContractNum = value; this.OnPropertyChanged(nameof(ContractNumber)); }
        }

        public ICommand AddCommand
        {
            get {
                return new RelayCommand((Param) =>{
                    ProjectManager mgr = new ProjectManager();
                    Project prj = new Project();
                    prj.Id = mgr.GetNewProjectId();
                    prj.Name = Name;
                    prj.BeginTime = DateTime.Parse(BeginTime);
                    prj.Creator = this.Master;
                    prj.EndTime = DateTime.Parse(this.EndTime);
                    prj.ContractNumber = this.ContractNumber;
                    prj.Status = 0;

                    try
                    {
                        mgr.AddProject(prj);
                        this.m_View.SavedClose();
                    }
                    catch
                    {
                        this.m_View.PopupMessage.ShowErrorMessage("保存新建项目时失败", "新建项目");
                    }
                  }
                ); }
        }
        
    }
}
