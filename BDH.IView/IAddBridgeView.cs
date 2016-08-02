using System.Text;
using System.Threading.Tasks;
using BDH.Manage;

namespace BDH.IView
{
    public interface IAddBridgeView : IViewBase
    {
        ChildBridge NewChildBridge { get; set; }

        void SavedClose();
    }
}
