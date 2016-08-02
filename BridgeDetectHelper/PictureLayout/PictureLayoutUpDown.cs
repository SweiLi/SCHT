using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutUpDown : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 2) throw new ArgumentException("参数数量不足");

            Grid img_grid = new Grid();
            var row_up = new RowDefinition();
            row_up.Height = new GridLength(1, GridUnitType.Star);
            img_grid.RowDefinitions.Add(row_up);

            var row_down = new RowDefinition();
            row_down.Height = new GridLength(1, GridUnitType.Star);
            img_grid.RowDefinitions.Add(row_down);

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var cv_0 = this.InitialImageCanvas(bmp_0);
            img_grid.Children.Add(cv_0);
            Grid.SetRow(cv_0, 0);

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var cv_1 = this.InitialImageCanvas(bmp_1);
            img_grid.Children.Add(cv_1);
            Grid.SetRow(cv_1, 1);

            return img_grid;
        }
    }
}
