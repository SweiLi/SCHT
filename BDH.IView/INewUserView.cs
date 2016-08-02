using BDH.Manage;

namespace BDH.IView
{
    public interface INewUserView : IViewBase
    {
        User GetUser();
        void SetReturn(bool ret_val, User new_user);
    }
}
