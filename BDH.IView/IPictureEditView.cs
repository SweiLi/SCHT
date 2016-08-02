using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.IView
{
    public interface IPictureEditView : IViewBase
    {
        string GetTestPicture();
        void SetEditorImage(MemoryStream ms);
    }
}
