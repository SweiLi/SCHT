using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDH.IView;

namespace BDH.ViewModel
{
    public class PictureEditViewModel : ViewModelBase
    {
        private IPictureEditView m_view;
        public PictureEditViewModel(IPictureEditView view)
        {
            this.m_view = view;
        }

        public ICommand LoadTestPicCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    string pic_path = this.m_view.GetTestPicture();
                    if (!string.IsNullOrWhiteSpace(pic_path))
                    {
                        byte[] bf = File.ReadAllBytes(pic_path);
                        var ms = new MemoryStream(bf);
                        this.m_view.SetEditorImage(ms);
                    }
                });
            }
        }
    }
}
