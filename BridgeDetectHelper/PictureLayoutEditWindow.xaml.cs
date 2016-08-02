using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinInterop = System.Windows.Interop;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using BDH.IView;
using BDH.ViewModel;
using BridgeDetectHelper.PictureLayout;

namespace BridgeDetectHelper
{
    /// <summary>
    /// PictureLayoutEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PictureLayoutEditWindow : Window, IPictureLayoutEditView
    {
        private IPictureLayout pic_layout;
        private int m_DefaultFontSize = 12;
        private Color m_DefaultFontColor = Colors.Black;
        private ContentControl m_SelectedShape;

        public PictureLayoutEditWindow()
        {
            InitializeComponent();
            this.SourceInitialized += new EventHandler(win_SourceInitialized);
            cmbFontSize.ItemsSource = new List<int>() { 10, 12, 14, 16, 18, 20 };
            cmbFontSize.SelectedIndex = 1;
            cmbColor.SelectedIndex = 7;

            btnSaveImage.IsEnabled = false;
            this.InitialLayoutControl();
            this.m_PopupMsg = new PopupMessage();

            this.DataContext = new PictureLayoutEditViewModel(this);
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        private void InitialLayoutControl()
        {
            for (int i = 0; i < 15; i++)
            {
                Button btn = new Button();
                btn.Width = 40;
                btn.Height = 40;
                btn.Margin = new Thickness(10);
                btn.Tag = i;
                btn.Style = Application.Current.FindResource("btnPicLayoutStyle") as Style;
                Binding bdcmd = new Binding("LayoutButtonCommand");
                btn.SetBinding(Button.CommandProperty, bdcmd);
                RelativeSource rs = new RelativeSource();
                rs.Mode = RelativeSourceMode.Self;
                Binding bdcmdparam = new Binding("Tag") { RelativeSource = rs };
                btn.SetBinding(Button.CommandParameterProperty, bdcmdparam);

                Image img = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(string.Format("PicLayoutIcon/{0}.png", i), UriKind.Relative);
                bi.EndInit();
                img.Source = bi;
                btn.Content = img;

                wrapLayout.Children.Add(btn);
            }

            this.ShowLayoutMergeTool();
        }

        public void ShowLayoutMergeTool()
        {
            spAdorner.Visibility = Visibility.Collapsed;
            spLay.Visibility = Visibility.Visible;
            /*
            StackPanel sp = new StackPanel();
            sp.Height = 30;
            sp.VerticalAlignment = VerticalAlignment.Center;
            sp.Orientation = Orientation.Horizontal;

            Button btn_merge = new Button();
            btn_merge.Content = "排版合并";
            btn_merge.Width = 120;
            btn_merge.Height = 28;
            btn_merge.Margin = new Thickness(30, 0, 10, 0);
            btn_merge.Style = null;
            btn_merge.Click += Btn_merge_Click;

            Label lab_tip = new Label();
            lab_tip.Content = "选择显示模式：";
            lab_tip.Height = 28;
            lab_tip.Width = 120;
            lab_tip.Style = null;
            lab_tip.FontSize = 14;
            sp.Children.Add(lab_tip);

            this.m_CmbImgPattern = new ComboBox();
            this.m_CmbImgPattern.Height = 28;
            this.m_CmbImgPattern.Width = 100;
            this.m_CmbImgPattern.Margin = new Thickness(20, 0, 0, 0);
            this.m_CmbImgPattern.ItemsSource = new List<string>() { "最窄边", "全部填满" };
            this.m_CmbImgPattern.SelectionChanged += Cmb_SelectionChanged;
            sp.Children.Add(this.m_CmbImgPattern);

            sp.Children.Add(btn_merge);

            bdrPicTool.Child = sp;
            //bdrPicTool.Child = btn_merge;
            **/
        }

        private Canvas m_AdornCanvas;
        private void ShowAdorner(ImageSource img_src)
        {
            this.m_AdornCanvas = new Canvas();
            this.m_AdornCanvas.ClipToBounds = true;
            this.m_AdornCanvas.SnapsToDevicePixels = true;
            this.m_AdornCanvas.Background = new ImageBrush(img_src);
            this.m_AdornCanvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.m_AdornCanvas.VerticalAlignment = VerticalAlignment.Stretch;
            this.m_AdornCanvas.Focusable = true;

            bdrPicLay.Child = this.m_AdornCanvas;
            this.ShowAdornerTool();
        }

        private void Btn_merge_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ImageGrid == null) return;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.m_ImageGrid.ActualWidth,
                (int)this.m_ImageGrid.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            bmp.Render(this.m_ImageGrid);

            this.ShowAdorner(bmp);

            btnSaveImage.IsEnabled = true;
        }

        public void ShowAdornerTool()
        {
            spLay.Visibility = Visibility.Collapsed;
            spAdorner.Visibility = Visibility.Visible;

        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_DefaultFontSize = (int)e.AddedItems[0];
            if (this.m_SelectedShape != null)
            {
                TextBox txt = this.m_SelectedShape.Content as TextBox;
                if (txt != null)
                {
                    txt.FontSize = this.m_DefaultFontSize;
                }
            }

        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as System.Reflection.PropertyInfo;
            this.m_DefaultFontColor = (Color)ColorConverter.ConvertFromString(item.Name);
            if (this.m_SelectedShape != null)
            {
                TextBox txt = this.m_SelectedShape.Content as TextBox;
                if (txt != null)
                {
                    txt.Foreground = new SolidColorBrush(this.m_DefaultFontColor);
                }
            }

        }

        private void BtnAdorn_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            int ord_val = Convert.ToInt32(btn.Tag);

            ContentControl cc = new ContentControl() { Width = 100, Height = 100, Padding = new Thickness(1) };
            cc.Style = this.FindResource("DesignerItemStyle") as Style;
            Canvas.SetLeft(cc, 50);
            Canvas.SetTop(cc, 50);
            cc.MinHeight = 30;
            cc.GotMouseCapture += Cc_GotMouseCapture;
            cc.LostFocus += Cc_LostFocus;
            cc.KeyDown += Cc_KeyDown;
            cc.MouseDoubleClick += Cc_MouseDoubleClick;
            cc.SizeChanged += Cc_SizeChanged;
            cc.Focusable = true;

            if (ord_val == 0)
            {
                Ellipse elli = new Ellipse() { IsHitTestVisible = false, StrokeThickness = 1, Stroke = Brushes.Red };
                cc.Content = elli;
            }
            else if (ord_val == 1)
            {
                Rectangle rect = new Rectangle() { IsHitTestVisible = false, Stroke = Brushes.Red, StrokeThickness = 1 };
                cc.Content = rect;
            }
            else if (ord_val == 2)
            {
                cc.Width = 200;
                cc.Height = 50;
                Arrow line = new Arrow()
                {
                    IsHitTestVisible = false,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                    HeadHeight = 5,
                    HeadWidth = 15,
                    X1 = 0,
                    Y1 = cc.Height / 2,
                    X2 = cc.Width,
                    Y2 = cc.Height / 2
                };
                //Rectangle line = new Rectangle() { IsHitTestVisible = false, Stroke = Brushes.Red, StrokeThickness = 1, Height = 2 };
                cc.Content = line;

            }
            else if (ord_val == 3)
            {
                TextBox txt = new TextBox() { IsHitTestVisible = false, Text = "请输入文字..." };
                txt.FontSize = this.m_DefaultFontSize;
                txt.Foreground = new SolidColorBrush(this.m_DefaultFontColor);
                cc.Content = txt;
                cc.Width = 200;
                cc.Height = 30;
            }

            foreach (Control child in this.m_AdornCanvas.Children)
            {
                Selector.SetIsSelected(child, false);
            }

            Selector.SetIsSelected(cc, true);

            this.m_AdornCanvas.Children.Add(cc);
            this.m_AdornCanvas.Focus();
        }

        private void Cc_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ContentControl cc = sender as ContentControl;
            Arrow line = cc.Content as Arrow;
            if (line != null)
            {
                line.X2 = cc.Width;
            }
        }

        private bool m_IsEditing = false;
        private void Cc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ContentControl cc = sender as ContentControl;
            TextBox txt = cc.Content as TextBox;
            if (txt != null)
            {
                this.m_IsEditing = true;
                txt.IsHitTestVisible = true;
                txt.Background = new SolidColorBrush(Colors.White);
                txt.BorderThickness = new Thickness(1);
                txt.Focus();
            }
        }

        private void Cc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ContentControl cc = sender as ContentControl;
                this.m_AdornCanvas.Children.Remove(cc);
            }
        }

        private void Cc_LostFocus(object sender, RoutedEventArgs e)
        {
            ContentControl cc = sender as ContentControl;
            Selector.SetIsSelected(cc, false);
            TextBox txt = cc.Content as TextBox;
            if (txt != null)
            {
                if (!this.m_IsEditing)
                {
                    txt.Background = null;
                    txt.BorderThickness = new Thickness(0);
                    txt.IsHitTestVisible = false;
                }
            }
        }

        private void Cc_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ContentControl cc = sender as ContentControl;
            this.m_SelectedShape = cc;
            Selector.SetIsSelected(cc, true);
            TextBox txt = cc.Content as TextBox;
            if (txt != null)
            {
                txt.Background = new SolidColorBrush(Colors.White);
                txt.BorderThickness = new Thickness(1);
            }
            else
            {
                foreach (Control child in this.m_AdornCanvas.Children)
                {
                    var ccc = child as ContentControl;
                    TextBox txt2 = ccc.Content as TextBox;
                    if (txt2 != null)
                    {
                        txt2.IsHitTestVisible = false;
                        this.m_IsEditing = false;
                        txt2.Background = null;
                        txt2.BorderThickness = new Thickness(0);
                    }
                }
            }
            cc.Focus();
        }

        private Grid m_ImageGrid;
        public void ShowPictureLayout(PictureLayouts pl, List<string> img_files)
        {
            bdrPicLay.Child = null;
            if (pl == PictureLayouts.JustOne)
            {
                //bdrPicTool.Visibility = Visibility.Collapsed;
                dpPicInfo.Visibility = Visibility.Visible;
                spJustOneTool.Visibility = Visibility.Visible;
            }
            else
            {
                //bdrPicTool.Visibility = Visibility.Visible;
                dpPicInfo.Visibility = Visibility.Collapsed;
                spJustOneTool.Visibility = Visibility.Collapsed;
            }

            pic_layout = PictureLayoutFactory.GetCreator(pl);
            if (pic_layout == null) return;

            List<ImageSource> imgSrc_list = new List<ImageSource>();
            foreach (string fp in img_files)
            {
                var img_src = this.GetImageSource(fp);

                imgSrc_list.Add(img_src);
            }

            try
            {
                this.m_ImageGrid = pic_layout.CreateLayout(imgSrc_list);

            }
            catch (ArgumentException ex)
            {
                this.PopupMessage.ShowErrorMessage(ex.Message, "图片编辑");
                return;
            }
            catch (Exception ex)
            {
                this.PopupMessage.ShowErrorMessage(ex.Message, "异常编辑");
                return;
            }

            bdrPicLay.Child = this.m_ImageGrid;

            this.ShowLayoutMergeTool();
            btnSaveImage.IsEnabled = false;
        }

        public void ShowImageListBox(List<string> img_files)
        {
            lbImages.Items.Clear();
            foreach (string fp in img_files)
            {
                var img_src = this.GetImageSource(fp);

                Image img = new Image();
                img.Width = 60;
                img.Height = 60;
                img.AllowDrop = true;
                img.MouseDown += Img_MouseDown;
                img.Source = img_src;
                lbImages.Items.Add(img);
            }
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            IDataObject ido = new DataObject();
            ido.SetData("image", img);
            DragDrop.DoDragDrop(img, ido, DragDropEffects.Copy);
        }

        public void ShowNoExistsNumImages(List<string> fp_list)
        {
            NoExistsNumImageWindow win = new NoExistsNumImageWindow(fp_list);
            win.Owner = Window.GetWindow(this);
            win.ShowDialog();
        }

        public string GetSaveFilePath()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存图片";
            sfd.Filter = "JPG Format(*.jpg)|*.jpg";
            sfd.FilterIndex = 0;
            if (sfd.ShowDialog().Value)
            {
                return sfd.FileName;
            }

            return null;
        }

        public MemoryStream GetSavingImageStream()
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.m_AdornCanvas.ActualWidth,
                (int)this.m_AdornCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            bmp.Render(this.m_AdornCanvas);

            var ms = new MemoryStream();
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(ms);

            return ms;
        }

        private ImageSource GetImageSource(string fp)
        {
            byte[] buff = File.ReadAllBytes(fp);
            MemoryStream ms = new MemoryStream(buff);

            var img_src = new BitmapImage();
            img_src.BeginInit();
            img_src.StreamSource = ms;
            img_src.EndInit();

            //Console.WriteLine(img_src.PixelWidth.ToString() + "--" + img_src.PixelHeight.ToString());
            return img_src;
        }

        private ImageSource GetImageSource(MemoryStream ms)
        {
            var img_src = new BitmapImage();
            img_src.BeginInit();
            img_src.StreamSource = ms;
            img_src.EndInit();

            return img_src;
        }

        private MemoryStream GetStream(ImageSource src)
        {
            //BitmapEncoder encoder = BitmapEncoder.Create(GUID_ContainerFormatPng);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)src));
            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);

            return ms;
        }

        #region 处理窗体最大化问题

        void win_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowProc));
        }

        private static System.IntPtr WindowProc(
              System.IntPtr hwnd,
              int msg,
              System.IntPtr wParam,
              System.IntPtr lParam,
              ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (System.IntPtr)0;
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {

                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };



        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }


        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc)
            {
                this.left = rcSrc.left;
                this.top = rcSrc.top;
                this.right = rcSrc.right;
                this.bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }
            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == RECT.Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }


            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }


        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #endregion
    }
}
