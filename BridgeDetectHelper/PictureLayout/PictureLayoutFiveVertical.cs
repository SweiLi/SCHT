using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutFiveVertical : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 5) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            for (int i = 0; i < 5; i++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                img_grid.RowDefinitions.Add(row);

                var bmp = imgSrcList[i] as BitmapImage;
                var bdr = this.InitialImageCanvas(bmp);
                img_grid.Children.Add(bdr);
                Grid.SetRow(bdr, i);
            }

            return img_grid;
        }
    }
}
