using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutRightUp : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 2) throw new ArgumentException("参数数量不足");

            Grid img_grid = new Grid();

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var cv_0 = this.InitialImageCanvas(bmp_0);
            img_grid.Children.Add(cv_0);

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var cv_1 = this.InitialImageCanvas(bmp_1);
            cv_1.BorderBrush = Brushes.Gray;
            cv_1.BorderThickness = new Thickness(1, 0, 0, 1);
            cv_1.Width = this.LittleImageWidth;
            cv_1.Height = this.LittleImageHeight;
            img_grid.Children.Add(cv_1);
            cv_1.HorizontalAlignment = HorizontalAlignment.Right;
            cv_1.VerticalAlignment = VerticalAlignment.Top;

            return img_grid;
        }
    }
}
