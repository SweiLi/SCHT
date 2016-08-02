using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutLeftOneRightTwo : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 3) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            var col_left = new ColumnDefinition();
            col_left.Width = new GridLength(1, GridUnitType.Star);
            img_grid.ColumnDefinitions.Add(col_left);

            var col_right = new ColumnDefinition();
            col_right.Width = new GridLength(1, GridUnitType.Star);
            img_grid.ColumnDefinitions.Add(col_right);

            var row_up = new RowDefinition();
            row_up.Height = new GridLength(1, GridUnitType.Star);
            img_grid.RowDefinitions.Add(row_up);

            var row_down = new RowDefinition();
            row_down.Height = new GridLength(1, GridUnitType.Star);
            img_grid.RowDefinitions.Add(row_down);

            var bmp_0 = imgSrcList[0] as BitmapImage;
            var bdr_0 = this.InitialImageCanvas(bmp_0);
            bdr_0.BorderBrush = Brushes.Gray;
            bdr_0.BorderThickness = new Thickness(0, 0, 1, 0);
            img_grid.Children.Add(bdr_0);
            Grid.SetRow(bdr_0, 0);
            Grid.SetColumn(bdr_0, 0);
            Grid.SetRowSpan(bdr_0, 2);

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var bdr_1 = this.InitialImageCanvas(bmp_1);
            bdr_1.BorderBrush = Brushes.Gray;
            bdr_1.BorderThickness = new Thickness(0, 0, 0, 1);
            img_grid.Children.Add(bdr_1);
            Grid.SetRow(bdr_1, 0);
            Grid.SetColumn(bdr_1, 1);

            var bmp_2 = imgSrcList[2] as BitmapImage;
            var bdr_3 = this.InitialImageCanvas(bmp_2);
            img_grid.Children.Add(bdr_3);
            Grid.SetRow(bdr_3, 1);
            Grid.SetColumn(bdr_3, 1);

            return img_grid;
        }
    }
}
