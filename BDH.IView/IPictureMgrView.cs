using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.IView
{
    public interface IPictureMgrView : IViewBase
    {
        void ShowPicEditWindow();
        void ShowPicLayoutWindow();
    }
}
