using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BDH.IView;
using BDH.ViewModel;

namespace BridgeDetectHelper
{
    /// <summary>
    /// DiseaseCountPage.xaml 的交互逻辑
    /// </summary>
    public partial class DiseaseCountPage : Page, IDiseaseCountView
    {
        public DiseaseCountPage()
        {
            InitializeComponent();

            this.m_PopupMsg = new PopupMessage();

            this.DataContext = new DiseaseCountViewModel(this);
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public void ShowPictureEditWindow()
        {
            PictureLayoutEditWindow win = new PictureLayoutEditWindow();
            win.Owner = Window.GetWindow(this);
            win.ShowDialog();
        }
    }
}
