namespace BDH.IView
{
    public interface IPopupMessage
    {
        void ShowMessage(string msg, string title);
        bool ShowQuestion(string msg, string title);
        void ShowErrorMessage(string msg, string title);
        void ShowWarningMessage(string msg, string title);
    }
}
