using System.Windows;
using BDH.IView;

namespace BridgeDetectHelper
{
    public class PopupMessage : IPopupMessage
    {
        public void ShowMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowQuestion(string msg, string title)
        {
            MessageBoxResult mbr = MessageBox.Show(msg, title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.OK) return true;

            return false;
        }

        public void ShowErrorMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowWarningMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
