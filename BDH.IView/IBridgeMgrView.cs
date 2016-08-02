using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Manage;

namespace BDH.IView
{
    public  interface IBridgeMgrView : IViewBase
    {
         ChildBridge GetNewChildBridge();
        ChildBridge GetEditedChildBridge(ChildBridge child);
    }
}
