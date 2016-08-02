using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.IView
{
    public interface IPictureLayoutEditView : IViewBase
    {
        void ShowPictureLayout(PictureLayouts pl, List<string> img_files);
        void ShowLayoutMergeTool();
        void ShowImageListBox(List<string> img_files);
        void ShowNoExistsNumImages(List<string> fp_list);
        string GetSaveFilePath();
        MemoryStream GetSavingImageStream();
    }
}
