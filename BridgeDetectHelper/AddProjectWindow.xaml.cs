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
using BDH.ViewModel;
using BDH.IView;
using BDH.Manage;

namespace BridgeDetectHelper
{
    /// <summary>
    /// AddProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddProjectWindow : Window , IAddProjectView
    {
        public AddProjectWindow()
        {
            InitializeComponent();

            this.m_PopupMsg = new PopupMessage();
            this.DataContext = new AddProjectViewModel(this);
        }

        public Project NewProject { get; set; }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public void SavedClose()
        {
            this.DialogResult = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
