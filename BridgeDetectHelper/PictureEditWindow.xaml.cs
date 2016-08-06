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
            img.Tag = new Size(img_src.PixelWidth, img_src.PixelHeight);

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

            this.m_LastPoint = mp;
        }

        private void Cc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var cc = sender as ContentControl;
            if (cc == null) return;

            cc.Cursor = Cursors.Arrow;
            cc.ReleaseMouseCapture();
        }

        private void Cc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
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

        private void btnLeftRotate_Click(object sender, RoutedEventArgs e)
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
            rotateform.Angle += -90;
            //rotateform = new RotateTransform(-90);

            img.RenderTransform = group;
        }
    }
}
