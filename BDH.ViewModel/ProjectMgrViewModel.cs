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
    public class ProjectMgrViewModel : ViewModelBase
    {
        public AsyncObservableCollection<Project> ProjectCollection { get; private set; }

        private IProjectMgrView m_View;

        public ProjectMgrViewModel(IProjectMgrView view)
        {
            this.m_View = view;
            this.ProjectCollection = new AsyncObservableCollection<Project>();
            var project_list = ProjectManager.GetProjectsByCon();
            //把返回来的每个Project加到ProjectCollection中
            project_list.ForEach((p)=>this.ProjectCollection.Add(p));

        }

        private String m_PrjName;
        public string PrjName
        {
            get { return this.m_PrjName; }
            set { this.m_PrjName = value; this.OnPropertyChanged(nameof(PrjName)); }
        }

        private DateTime m_BeginTime;

        public DateTime BeginTime
        {
            get { return this.m_BeginTime; }
            set { this.m_BeginTime = value; this.OnPropertyChanged(nameof(BeginTime)); }
        }

        private String m_PriContract;
        public String PriContract
        {
            get { return this.m_PriContract; }
            set { this.m_PriContract = value;  this.OnPropertyChanged(nameof(PriContract)); }

        }

        private int m_PrjStatus;
        public int PrjStatus
        {
            get { return this.m_PrjStatus; }
            set { this.m_PrjStatus = value; this.OnPropertyChanged(nameof(PrjStatus)); }
        }

        public ICommand AddProjectCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    var prj = this.m_View.GetNewProject();
                    if (prj != null)
                    {
                        this.ProjectCollection.Add(prj);
                    }
                });
            }
        }

        public ICommand QueryProjectCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    var tp = (Tuple<string, DateTime?, string, int>)param;

                    this.ProjectCollection.Clear();

                    var items = ProjectManager.GetProjects(tp.Item1, tp.Item2, tp.Item3, tp.Item4);
                    items.ForEach((p) => this.ProjectCollection.Add(p));
                });
            }
        }
    }
}
