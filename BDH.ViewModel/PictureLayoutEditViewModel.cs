using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BDH.IView;

namespace BDH.ViewModel
{
    public class PictureLayoutEditViewModel : ViewModelBase
    {
        private IPictureLayoutEditView m_view;
        public PictureLayoutEditViewModel(IPictureLayoutEditView view)
        {
            this.m_view = view;
            this.m_DefaultLayout = PictureLayouts.JustOne;
            this.m_ImageFileList = new List<string>();
            this.CanSaveImage = false;
        }

        private PictureLayouts m_DefaultLayout;
        private List<string> m_ImageFileList;

        public ICommand LayoutButtonCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    int val = (int)param;
                    PictureLayouts pl = (PictureLayouts)val;
                    this.m_DefaultLayout = pl;

                    this.m_view.ShowPictureLayout(this.m_DefaultLayout, this.m_ImageFileList);
                });
            }
        }

        public ICommand LoadPictureCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    string dir = @"D:\开发用图标\Pngs\512x512\扁平化512x512\";
                    this.m_ImageFileList.Clear();

                    for (int i=0; i<5; i++)
                    {
                        this.m_ImageFileList.Add(string.Format("{0}{1}.png", dir, i+1));
                    }

                    this.m_view.ShowImageListBox(this.m_ImageFileList);
                    this.m_view.ShowPictureLayout(this.m_DefaultLayout, this.m_ImageFileList);
                });
            }
        }

        private string m_ImgSelectStr;
        public string ImageSelectString
        {
            get { return this.m_ImgSelectStr; }
            set { this.m_ImgSelectStr = value; this.OnPropertyChanged(nameof(ImageSelectString)); }
        }

        private bool m_CanSaveImage;
        public bool CanSaveImage
        {
            get { return this.m_CanSaveImage; }
            set { this.m_CanSaveImage = value; this.OnPropertyChanged(nameof(CanSaveImage)); }
        }

        public ICommand ImageSelectCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    if (string.IsNullOrWhiteSpace(this.ImageSelectString)) return;

                    string dir = AppDomain.CurrentDomain.BaseDirectory + "sampleimages\\";
                    //解析图片编号的输入格式，以半角逗号(,)分隔为单个编号，以半角横杠(-)分隔为连续编号
                    string[] img_arr = this.ImageSelectString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> imgpath_list = new List<string>();
                    foreach (string str in img_arr)
                    {
                        if (str.Contains("-"))
                        {
                            string[] tmp_arr = str.Split(new char[] { '-' });
                            if (tmp_arr.Length != 2)
                            {
                                this.m_view.PopupMessage.ShowErrorMessage("图片编号格式输入有误，请修改正确", "图片编辑");
                                return;
                            }

                            int min = -1;
                            int max = -1;
                            bool suc = Int32.TryParse(tmp_arr[0], out min);
                            if (!suc)
                            {
                                this.m_view.PopupMessage.ShowErrorMessage("图片编号格式输入有误，请修改正确", "图片编辑");
                                return;
                            }
                            suc = Int32.TryParse(tmp_arr[1], out max);
                            if (!suc)
                            {
                                this.m_view.PopupMessage.ShowErrorMessage("图片编号格式输入有误，请修改正确", "图片编辑");
                                return;
                            }

                            for (int i=min; i<= max; i++)
                            {
                                imgpath_list.Add(string.Format("{0}{1}.jpg", dir, i));
                            }
                        }
                        else
                        {
                            int num = -1;
                            bool suc = Int32.TryParse(str, out num);
                            if (!suc)
                            {
                                this.m_view.PopupMessage.ShowErrorMessage("图片编号格式输入有误，请修改正确", "图片编辑");
                                return;
                            }
                            imgpath_list.Add(string.Format("{0}{1}.jpg", dir, num));
                        }
                    }

                    //检查图片是否存在
                    List<string> noFile_list = new List<string>();
                    foreach (string fp in imgpath_list)
                    {
                        if (!File.Exists(fp)) noFile_list.Add(fp);
                    }
                    if (noFile_list.Count > 0)
                    {
                        this.m_view.ShowNoExistsNumImages(noFile_list);
                        return;
                    }
                    else
                    {
                        this.m_ImageFileList.Clear();
                        this.m_ImageFileList.AddRange(imgpath_list);
                        this.m_view.ShowImageListBox(this.m_ImageFileList);
                        this.m_view.ShowPictureLayout(this.m_DefaultLayout, this.m_ImageFileList);
                        this.ImageSelectString = "";
                    }

                });
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return new RelayCommand((param) =>
                {
                    string fp = this.m_view.GetSaveFilePath();
                    if (string.IsNullOrWhiteSpace(fp)) return;

                    var ms = this.m_view.GetSavingImageStream();
                    using (FileStream fs = new FileStream(fp, FileMode.Create))
                    {
                        byte[] buff = ms.ToArray();
                        fs.Write(buff, 0, buff.Length);
                        fs.Close();
                    }
                });
            }
        }
    }
}
