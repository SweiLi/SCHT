using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutRightUpTwo : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 3) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var cv_0 = this.InitialImageCanvas(bmp_0);
            img_grid.Children.Add(cv_0);

            //StackPanel sp = new StackPanel();
            //sp.Orientation = Orientation.Vertical;
            //sp.Width = this.LittleImageWidth;
            //sp.HorizontalAlignment = HorizontalAlignment.Right;
            //sp.VerticalAlignment = VerticalAlignment.Top;
            //img_grid.Children.Add(sp);

            //for (int i = 1; i < 3; i++)
            //{
            //    var bmp = imgSrcList[i] as BitmapImage;
            //    var cv = this.InitialImageCanvas(bmp);
            //    sp.Children.Add(cv);
            //}

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var cv_1 = this.InitialImageCanvas(bmp_1);
            cv_1.Width = this.LittleImageWidth;
            cv_1.Height = this.LittleImageHeight;
            img_grid.Children.Add(cv_1);
            cv_1.HorizontalAlignment = HorizontalAlignment.Right;
            cv_1.VerticalAlignment = VerticalAlignment.Top;

            var bmp_2 = imgSrcList[2] as BitmapImage;
            var cv_2 = this.InitialImageCanvas(bmp_2);
            cv_2.Width = this.LittleImageWidth;
            cv_2.Height = this.LittleImageHeight;
            img_grid.Children.Add(cv_2);
            cv_2.HorizontalAlignment = HorizontalAlignment.Right;
            cv_2.VerticalAlignment = VerticalAlignment.Top;
            cv_2.Margin = new Thickness(0, this.LittleImageHeight, 0, 0);

            return img_grid;
        }
    }
}
