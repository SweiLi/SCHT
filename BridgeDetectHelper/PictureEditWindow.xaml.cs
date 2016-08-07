using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using BDH.IView;
using BDH.ViewModel;

namespace BridgeDetectHelper
{
    /// <summary>
    /// PictureEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PictureEditWindow : Window, IPictureEditView
    {
        protected int LittleImageWidth = 200;
        protected int LittleImageHeight = 200;
        protected bool m_IsMoving = false;
        protected bool m_CanFreeMove = false;
        protected Point m_LastPoint;
        private bool m_IsFreezed = false;

        public PictureEditWindow()
        {
            InitializeComponent();

            this.m_PopupMsg = new PopupMessage();

            this.DataContext = new PictureEditViewModel(this);

            string file_path = AppDomain.CurrentDomain.BaseDirectory + "sampleimages\\1.jpg";
            var cv = this.InitialImageCanvas(new BitmapImage(new Uri(file_path)));
            bdrPic.Child = cv;
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public string GetTestPicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择测试图片";
            ofd.Filter = "PNG JPG (*.png;*.jpg)|*.png;*.jpg";
            ofd.FilterIndex = 0;
            ofd.Multiselect = false;
            if (ofd.ShowDialog().Value)
            {
                return ofd.FileName;
            }

            return null;
        }

        public void SetEditorImage(MemoryStream ms)
        {
            var img_src = new BitmapImage();
            img_src.BeginInit();
            img_src.StreamSource = ms;
            img_src.EndInit();
        }

        private void chkSelect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void SaveToFile()
        {
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "sampleimages\\1.jpg";

            //FileStream fs = new FileStream(file_path, FileMode.Create);
            //RenderTargetBitmap bmp = new RenderTargetBitmap((int)DesignerCanvas.ActualWidth,
            //    (int)DesignerCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            //bmp.Render(DesignerCanvas);
            //BitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));
            //encoder.Save(fs);
            //fs.Close();
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            this.SaveToFile();
        }

        protected Canvas InitialImageCanvas(BitmapImage img_src)
        {
            var transform = new TranslateTransform();
            var scaleform = new ScaleTransform();
            var groupform = new TransformGroup();
            var rotateform = new RotateTransform();
            groupform.Children.Add(scaleform);
            groupform.Children.Add(transform);
            groupform.Children.Add(rotateform);

            Image img = new Image();
            img.Margin = new Thickness(1);
            img.MouseDown += img_MouseDown;
            //img.Drop += img_Drop;
            img.AllowDrop = true;
            img.Source = img_src;
            img.Width = img_src.PixelWidth;
            img.Height = img_src.PixelHeight;
            img.RenderTransform = groupform as Transform;
            //img.MouseWheel += Img_MouseWheel;
            img.Tag = new Point(0, 0);

            //this.m_ImgList.Add(img);

            var cc = new ContentControl();
            cc.Width = img_src.PixelWidth;
            cc.Height = img_src.PixelHeight;
            cc.Margin = new Thickness(2);
            cc.Content = img;
            cc.MouseRightButtonDown += Cc_MouseRightButtonDown;
            cc.MouseRightButtonUp += Cc_MouseRightButtonUp;
            cc.MouseMove += Cc_MouseMove;
            cc.MouseWheel += Cc_MouseWheel;
            cc.Tag = false;

            Canvas cv = new Canvas();
            cv.ClipToBounds = true;
            cv.Children.Add(cc);
            cv.SizeChanged += Cv_SizeChanged;

            //Border bdr = new Border();
            //bdr.Child = cv;

            return cv;
        }

        protected void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.m_IsFreezed) return;

            if (e.ChangedButton == MouseButton.Left)
            {
                Image img = sender as Image;
                //var ms = this.GetStream(img.Source);
                IDataObject ido = new DataObject();
                ido.SetData("image", img);
                DragDrop.DoDragDrop(img, ido, DragDropEffects.Move);
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                this.m_IsMoving = true;
                this.m_LastPoint = e.GetPosition(null);
            }
        }

        private void Cc_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_IsFreezed) return;

            var cc = sender as ContentControl;
            if (cc == null) return;

            if (cc.IsMouseCaptured)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    this.MoveImageShape(cc, e);
                }
            }
        }

        private void MoveImageShape(ContentControl cc, MouseEventArgs e)
        {
            var cv = cc.Parent as Canvas;
            var img = cc.Content as Image;

            var group = img.RenderTransform as TransformGroup;
            var scaleform = group.Children[0] as ScaleTransform;
            var transform = group.Children[1] as TranslateTransform;

            var mp = e.GetPosition(cv);
            double sc_x = cv.ActualWidth / img.Width;
            double sc_y = cv.ActualHeight / img.Height;

            var canFreeMoving = (bool)cc.Tag;

            if (canFreeMoving)
            {
                transform.X -= this.m_LastPoint.X - mp.X;
                transform.Y -= this.m_LastPoint.Y - mp.Y;
            }
            else
            {
                if (sc_x > sc_y)
                {
                    transform.Y -= this.m_LastPoint.Y - mp.Y;
                }
                else
                    transform.X -= this.m_LastPoint.X - mp.X;
            }

            img.Tag = new Point(transform.X, transform.Y);
            this.m_LastPoint = mp;
        }

        private void Cc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var cc = sender as ContentControl;
            if (cc == null) return;

            cc.Cursor = Cursors.Arrow;
            cc.ReleaseMouseCapture();

            var cv = cc.Parent as Canvas;
            var img = cc.Content as Image;
            var p = (Point)img.Tag;
            var group = img.RenderTransform as TransformGroup;
            var scaleform = group.Children[0] as ScaleTransform;
            var transform = group.Children[1] as TranslateTransform;
            var img_p = e.GetPosition(img);
            var img_point = new Point(img_p.X * scaleform.ScaleX, img_p.Y * scaleform.ScaleY);
            var cv_point = e.GetPosition(cv);

            double img_width = img.Width * scaleform.ScaleX;
            double img_height = img.Height * scaleform.ScaleY;

            double offset_top = cv_point.Y - img_point.Y;
            double offset_btm = (cv.ActualHeight - cv_point.Y) - (img_height - img_point.Y);
            if (img_height < cv.ActualHeight)
            {
                if (offset_top < 0 && offset_btm > 0) transform.Y -= offset_top;
                else if (offset_top > 0 && offset_btm < 0) transform.Y += offset_btm;
            }
            else
            {
                if (offset_top < 0 && offset_btm > 0) transform.Y += offset_btm;
                else if (offset_top > 0 && offset_btm < 0) transform.Y -= offset_top;
            }

            double offset_left = cv_point.X - img_point.X;
            double offset_right = (cv.ActualWidth - cv_point.X) - (img_width - img_point.X);
            if (img_width < cv.ActualWidth)
            {
                if (offset_left < 0 && offset_right > 0) transform.X -= offset_left;
                else if (offset_left > 0 && offset_right < 0) transform.X += offset_right;
            }
            else
            {
                if (offset_left < 0 && offset_right > 0) transform.X += offset_right;
                else if (offset_left > 0 && offset_right < 0) transform.X -= offset_left;
            }
        }

        private void Cc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.m_IsFreezed) return;

            var cc = sender as ContentControl;
            if (cc == null) return;

            var cv = cc.Parent as Canvas;
            cc.Cursor = Cursors.Hand;
            this.m_LastPoint = e.GetPosition(cv);
            cc.CaptureMouse();
        }

        private void Cc_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var cc = sender as ContentControl;
            var cv = cc.Parent as Canvas;
            var img = cc.Content as Image;

            var point = e.GetPosition(cv);
            var group = img.RenderTransform as TransformGroup;
            var delta = e.Delta * 0.001;
            var pointToContent = group.Inverse.Transform(point);
            var scaleform = group.Children[0] as ScaleTransform;
            if (scaleform.ScaleX + delta < 0.1) return;

            double val = scaleform.ScaleX + delta;
            if (val < 2 && val > 0.5)
            {
                scaleform.CenterX = point.X;
                scaleform.CenterY = point.Y;
                scaleform.ScaleX += delta;
                scaleform.ScaleY += delta;
            }

            cc.Tag = true;
        }

        private void Cv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var cv = sender as Canvas;
            if (cv != null && cv.Children.Count > 0)
            {
                var cc = cv.Children[0] as ContentControl;
                var img = cc.Content as Image;

                double sc_x = cv.ActualWidth / img.Width;
                double sc_y = cv.ActualHeight / img.Height;

                var group = img.RenderTransform as TransformGroup;
                var scaleform = group.Children[0] as ScaleTransform;
                var transform = group.Children[1] as TranslateTransform;

                if (sc_x > sc_y)
                {
                    scaleform.ScaleX = sc_x;
                    scaleform.ScaleY = sc_x;
                }
                else
                {
                    scaleform.ScaleX = sc_y;
                    scaleform.ScaleY = sc_y;
                }
            }
        }

        private void RotateImage(double angle)
        {
            var cv = bdrPic.Child as Canvas;
            var cc = cv.Children[0] as ContentControl;
            var img = cc.Content as Image;

            var group = img.RenderTransform as TransformGroup;
            var scaleform = group.Children[0] as ScaleTransform;
            var transform = group.Children[1] as TranslateTransform;
            var rotateform = group.Children[2] as RotateTransform;
            rotateform.CenterX = cv.ActualWidth / 2;
            rotateform.CenterY = cv.ActualHeight / 2;
            rotateform.Angle += angle;
        }

        private void btnLeftRotate_Click(object sender, RoutedEventArgs e)
        {
            this.RotateImage(-90);
        }

        private void btnRightRotate_Click(object sender, RoutedEventArgs e)
        {
            this.RotateImage(90);
        }

        private CroppingAdorner m_CropAdr;
        private FrameworkElement m_FrEl;
        private Brush m_BrOriginal;

        private void RemoveCropFromCur()
        {
            AdornerLayer adr = AdornerLayer.GetAdornerLayer(this.m_FrEl);
            adr.Remove(this.m_CropAdr);
        }

        private void AddCropToElement(FrameworkElement fel)
        {
            if (this.m_FrEl != null) this.RemoveCropFromCur();

            Rect rct = new Rect(fel.ActualWidth * 0.2, fel.ActualHeight * 0.2,
                fel.ActualWidth * 0.6, fel.ActualHeight * 0.6);
            AdornerLayer adr = AdornerLayer.GetAdornerLayer(fel);
            this.m_CropAdr = new CroppingAdorner(fel, rct);
            adr.Add(this.m_CropAdr);
            this.m_FrEl = fel;
            this.SetClipColorGrey();
            this.m_CropAdr.MouseMove += M_CropAdr_MouseMove;
        }

        private void M_CropAdr_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetTop(this.m_CropAdr, 0);
                Canvas.SetLeft(this.m_CropAdr, 0);
            }
        }

        private void SetClipColorGrey()
        {
            if (this.m_CropAdr != null)
            {
                Color clr = Colors.Gray;
                clr.A = 200;
                this.m_CropAdr.Fill = new SolidColorBrush(clr);
            }
        }
        private void btnCropImg_Click(object sender, RoutedEventArgs e)
        {
            var cv = bdrPic.Child as Canvas;
            var cc = cv.Children[0] as ContentControl;
            var img = cc.Content as Image;

            this.AddCropToElement(cv);
            this.m_BrOriginal = this.m_CropAdr.Fill;
            this.m_IsFreezed = true;
        }

        WriteableBitmap SetBrightness( BitmapSource bs, int brightness)
        {
            brightness = brightness * 255 / 100;

            WriteableBitmap wb = new WriteableBitmap(bs);
            uint[] PixelData = new uint[wb.PixelWidth * wb.PixelHeight];
            wb.CopyPixels(PixelData, 4 * wb.PixelWidth, 0);
            for (uint y = 0; y < wb.PixelHeight; y++)
            {
                for (uint x = 0; x < wb.PixelWidth; x++)
                {
                    uint pixel = PixelData[y * wb.PixelWidth + x];
                    byte[] dd = BitConverter.GetBytes(pixel);
                    int B = (int)dd[0] + brightness;
                    int G = (int)dd[1] + brightness;
                    int R = (int)dd[2] + brightness;
                    if (B < 0) B = 0;
                    if (B > 255) B = 255;
                    if (G < 0) G = 0;
                    if (G > 255) G = 255;
                    if (R < 0) R = 0;
                    if (R > 255) R = 255;
                    dd[0] = (byte)B;
                    dd[1] = (byte)G;
                    dd[2] = (byte)R;
                    PixelData[y * wb.PixelWidth + x] = BitConverter.ToUInt32(dd, 0);
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, wb.PixelWidth, wb.PixelHeight), PixelData, 4 * wb.PixelWidth, 0);

            return wb;
        }

        private void btnBrightImg_Click(object sender, RoutedEventArgs e)
        {
            var cv = bdrPic.Child as Canvas;
            var cc = cv.Children[0] as ContentControl;
            var img = cc.Content as Image;

            var wb = this.SetBrightness(img.Source as BitmapSource, 20);
            img.Source = wb;
        }
    }
}
