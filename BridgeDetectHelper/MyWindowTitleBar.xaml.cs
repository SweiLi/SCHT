using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BridgeDetectHelper
{
    /// <summary>
    /// MyWindowTitleBar.xaml 的交互逻辑
    /// </summary>
    public partial class MyWindowTitleBar : UserControl
    {
        public MyWindowTitleBar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.Close();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (win.WindowState == WindowState.Maximized)
                win.WindowState = WindowState.Normal;
            else
                win.WindowState = WindowState.Maximized;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Window.GetWindow(e.OriginalSource as DependencyObject).DragMove();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this != null)
            {
                var win = Window.GetWindow(this);
                if (win != null) labelTitle.Content = win.Title;
            }
        }

        private void labelTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            Window win = Window.GetWindow(this);
            if (win.WindowState == WindowState.Maximized)
                win.WindowState = WindowState.Normal;
            else
                win.WindowState = WindowState.Maximized;
        }
    }
}
