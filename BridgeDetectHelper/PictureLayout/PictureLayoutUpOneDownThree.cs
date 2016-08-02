using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutUpOneDownThree : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 4) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var cv_0 = this.InitialImageCanvas(bmp_0);
            img_grid.Children.Add(cv_0);

            Action<int, Thickness> CreateLittleImage = (m, n) =>
            {
                var bmp = imgSrcList[m] as BitmapImage;
                var cv = this.InitialImageCanvas(bmp);
                cv.Width = this.LittleImageWidth;
                cv.Height = this.LittleImageHeight;
                img_grid.Children.Add(cv);
                cv.HorizontalAlignment = HorizontalAlignment.Center;
                cv.VerticalAlignment = VerticalAlignment.Bottom;
                cv.Margin = n;
            };

            CreateLittleImage(1, new Thickness(0));
            CreateLittleImage(2, new Thickness(0, 0, 400, 0));
            CreateLittleImage(3, new Thickness(400, 0, 0, 0));

            return img_grid;
        }
    }
}
