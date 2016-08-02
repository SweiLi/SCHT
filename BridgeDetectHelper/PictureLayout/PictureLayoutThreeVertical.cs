using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutThreeVertical : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 3) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            for (int i=0; i<3; i++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                img_grid.RowDefinitions.Add(row);

                var bmp = imgSrcList[i] as BitmapImage;
                var cv = this.InitialImageCanvas(bmp);
                img_grid.Children.Add(cv);
                Grid.SetRow(cv, i);
            }

            return img_grid;
        }
    }
}
