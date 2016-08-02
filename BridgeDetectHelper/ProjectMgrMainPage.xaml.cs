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
using BDH.MdHelper;


namespace BridgeDetectHelper
{
    /// <summary>
    /// ProjectMgrMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectMgrMainPage : Page, IProjectMgrView
    {
        public ProjectMgrMainPage()
        {
            InitializeComponent();

            this.m_PopupMsg = new PopupMessage(); //初始化弹出对话框
            cmbPrjState.ItemsSource = new List<string>() { "全部", "在建", "已完结", "审核未通过" };
            cmbPrjState.SelectedIndex = 0;

            this.DataContext = new ProjectMgrViewModel(this);
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public Project GetNewProject()
        {
            AddProjectWindow apw = new AddProjectWindow();
            apw.Owner = Window.GetWindow(this);
            if (apw.ShowDialog().Value)
            {
                return apw.NewProject;
            }

            return null;
        }
    }
}
