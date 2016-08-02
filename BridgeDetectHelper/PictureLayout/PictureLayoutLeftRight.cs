using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutLeftRight : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 2) throw new ArgumentException("参数数量不足");

            Grid img_grid = new Grid();
            var col_left = new ColumnDefinition();
            col_left.Width = new GridLength(1, GridUnitType.Star);
            img_grid.ColumnDefinitions.Add(col_left);

            var col_right = new ColumnDefinition();
            col_right.Width = new GridLength(1, GridUnitType.Star);
            img_grid.ColumnDefinitions.Add(col_right);

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var cv_0 = this.InitialImageCanvas(bmp_0);
            cv_0.BorderBrush = Brushes.Gray;
            cv_0.BorderThickness = new Thickness(0, 0, 1, 0);
            img_grid.Children.Add(cv_0);
            Grid.SetColumn(cv_0, 0);

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var cv_1 = this.InitialImageCanvas(bmp_1);
            img_grid.Children.Add(cv_1);
            Grid.SetColumn(cv_1, 1);

            return img_grid;
        }
    }
}
