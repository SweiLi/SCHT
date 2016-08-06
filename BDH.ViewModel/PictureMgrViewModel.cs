using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.IView;

namespace BDH.ViewModel
{
    public class PictureMgrViewModel : ViewModelBase
    {
        private IPictureMgrView m_View;
        public PictureMgrViewModel(IPictureMgrView view)
        {
            this.m_View = view;
        }

        public ICommand ShowPicEditCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    this.m_View.ShowPicEditWindow();
                });
            }
        }
    }
}
