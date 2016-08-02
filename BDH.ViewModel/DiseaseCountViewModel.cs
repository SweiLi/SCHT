using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.IView;
using BDH.Manage;

namespace BDH.ViewModel
{
    public class DiseaseCountViewModel : ViewModelBase
    {
        private IDiseaseCountView m_View;

        public DiseaseCountViewModel(IDiseaseCountView view)
        {
            this.m_View = view;
        }

        public ICommand ShowPicEditCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    this.m_View.ShowPictureEditWindow();
                });
            }
        }

        public ICommand TestSqlCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    BriComDisInfoManager.Read();
                });
            }
        }
    }
}
