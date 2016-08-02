using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.IView;

namespace BDH.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMainView m_View;

        public MainViewModel(IMainView view)
        {
            this.m_View = view;
        }
    }
}
