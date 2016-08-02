using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdhConfEditor
{
    public class MainViewModel : ViewModelBase
    {
        private IMainView m_view;
        public MainViewModel(IMainView view)
        {
            this.m_view = view;
        }

        private string m_Conf;
        public string ConfXml
        {
            get { return this.m_Conf; }
            set { this.m_Conf = value; this.OnPropertyChanged(nameof(ConfXml)); }
        }

        public ICommand OpenConfCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    string fp = this.m_view.GetFilePath();
                    if (string.IsNullOrWhiteSpace(fp)) return;

                    FileEncryptHelper encrypt = new FileEncryptHelper();
                    byte[] buff = encrypt.DecryptFile(fp);
                    this.ConfXml = Encoding.UTF8.GetString(buff);
                });
            }
        }
    }
}
