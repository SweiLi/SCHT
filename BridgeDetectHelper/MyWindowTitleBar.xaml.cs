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

        public Visibility CanMinimum
        {
            get { return (Visibility)GetValue(CanMinimumProperty); }
            set { SetValue(CanMinimumProperty, value); }
        }

        public static readonly DependencyProperty CanMinimumProperty =
            DependencyProperty.Register("CanMinimum", typeof(Visibility), typeof(MyWindowTitleBar),
                new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnCanMinimumValueChanged)));

        static void OnCanMinimumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as MyWindowTitleBar;
            sender.btnMin.Visibility = (Visibility)e.NewValue;
        }

        public Visibility CanMaximum
        {
            get { return (Visibility)GetValue(CanMaximumProperty); }
            set { SetValue(CanMaximumProperty, value); }
        }
        public static readonly DependencyProperty CanMaximumProperty =
            DependencyProperty.Register("CanMaximum", typeof(Visibility), typeof(MyWindowTitleBar),
                new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnCanMaximumValueChanged)));

        static void OnCanMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as MyWindowTitleBar;
            sender.btnMin.Visibility = (Visibility)e.NewValue;
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
