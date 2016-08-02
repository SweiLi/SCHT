using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace BridgeDetectHelper.PictureLayout
{
    public abstract class AbstractPictureLayout : IPictureLayout
    {
        protected int LittleImageWidth = 200;
        protected int LittleImageHeight = 200;
        protected bool m_IsMoving = false;
        protected bool m_CanFreeMove = false;
        protected Point m_LastPoint;
        private List<Image> m_ImgList = new List<Image>();

        private MemoryStream GetStream(ImageSource src)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)src));
            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);

            return ms;
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

        protected void img_Drop(object sender, DragEventArgs e)
        {
            Image img = sender as Image;
            var tmp = img.Source;

            Image src_img = e.Data.GetData("image") as Image;
            img.Source = src_img.Source;
            if (e.Effects == DragDropEffects.Move)
                src_img.Source = tmp;

            var cv_src = src_img.Parent as Canvas;
            Canvas.SetLeft(src_img, 0);
            Canvas.SetTop(src_img, 0);

            var cv_dst = img.Parent as Canvas;
            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
        }

        protected Border InitialImageCanvas(BitmapImage img_src)
        {
            var transform = new TranslateTransform();
            var scaleform = new ScaleTransform();
            var groupform = new TransformGroup();
            groupform.Children.Add(scaleform);
            groupform.Children.Add(transform);

            Image img = new Image();
            img.Margin = new Thickness(1);
            img.MouseDown += img_MouseDown;
            img.Drop += img_Drop;
            img.AllowDrop = true;
            img.Source = img_src;
            img.Width = img_src.PixelWidth;
            img.Height = img_src.PixelHeight;
            img.RenderTransform = groupform as Transform;
            //img.MouseWheel += Img_MouseWheel;
            img.Tag = new Size(img_src.PixelWidth, img_src.PixelHeight);

            this.m_ImgList.Add(img);

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
            //cv.MouseMove += this.Canvas_MouseMove;
            //cv.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            //cv.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            cv.Children.Add(cc);
            cv.SizeChanged += Cv_SizeChanged;

            Border bdr = new Border();
            bdr.Child = cv;

            return bdr;
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

        //根据设置的填充方式，进行自动布局更新
        private void AutoLayoutUpdate(Canvas cv)
        {
            var img = cv.Children[0] as Image;
            var group = img.RenderTransform as TransformGroup;
            var scaleform = group.Children[0] as ScaleTransform;
            var transform = group.Children[1] as TranslateTransform;

            if (img.Stretch == Stretch.Uniform)
            {
                double sc_x = img.Width / cv.ActualWidth;
                double sc_y = img.Height / cv.ActualHeight;

                if (sc_x < sc_y)
                {
                    //scaleform.ScaleX = sc_x;
                    //scaleform.ScaleY = sc_x;
                    if (cv.ActualWidth > 0)
                        img.Width = cv.ActualWidth;
                }
                else
                {
                    //scaleform.ScaleX = sc_y;
                    //scaleform.ScaleY = sc_y;
                    if (cv.ActualHeight > 0)
                        img.Height = cv.ActualHeight;
                }

                transform.X = 0;
                transform.Y = 0;
                this.m_LastPoint = new Point(0, 0);
            }
            //else if (img.Stretch == Stretch.Fill)
            //{
            //    img.Width = cv.ActualWidth;
            //    img.Height = cv.ActualHeight;
            //    scaleform.ScaleX = 1;
            //    scaleform.ScaleY = 1;
            //    transform.X = 0;
            //    transform.Y = 0;
            //}
            //else if (img.Stretch == Stretch.None)
            //{
            //    var wh = (Size)img.Tag;
            //    img.Width = wh.Width;
            //    img.Height = wh.Height;
            //    scaleform.ScaleX = 1;
            //    scaleform.ScaleY = 1;
            //    transform.X = 0;
            //    transform.Y = 0;
            //}

            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
        }

        private Grid GetParentGrid(DependencyObject obj)
        {
            var o = VisualTreeHelper.GetParent(obj);
            var g = o as Grid;
            if (g == null) return this.GetParentGrid(o);

            return g;
        }

        protected void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_IsMoving)
            {
                Canvas cv = sender as Canvas;
                if (cv != null && cv.Children.Count > 0)
                {
                    Image img = cv.Children[0] as Image;

                    double x = e.GetPosition(null).X - this.m_LastPoint.X;
                    double y = e.GetPosition(null).Y - this.m_LastPoint.Y;
                    double nl = Canvas.GetLeft(img) + x;
                    double ny = Canvas.GetTop(img) + y;

                    double sc_x = img.Width / cv.ActualWidth;
                    double sc_y = img.Height / cv.ActualHeight;

                    if (this.m_CanFreeMove)
                    {
                        Canvas.SetTop(img, ny);
                        Canvas.SetLeft(img, nl);
                    }
                    else
                    {
                        if (img.Width == cv.ActualWidth) Canvas.SetTop(img, ny);
                        else Canvas.SetLeft(img, nl);
                    }

                    this.m_LastPoint = e.GetPosition(null);
                }
            }
        }

        protected void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.m_IsMoving = false;
        }

        protected void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.m_IsMoving = false;
        }

        public abstract Grid CreateLayout(List<ImageSource> imgSrcList);
    }
}
