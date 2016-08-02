using BDH.Manage;

namespace BDH.IView
{
    public interface ISystemMgrView : IViewBase
    {
        User CreateNewUser();
        void GetNewProject();
    }
}
