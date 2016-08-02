using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutJustOne : AbstractPictureLayout
    {
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count == 0) throw new ArgumentException("图片数量不足");

            Grid img_grid = new Grid();

            var bmp = imgSrcList[0] as BitmapImage;
            var cv = this.InitialImageCanvas(bmp);
            img_grid.Children.Add(cv);

            return img_grid;
        }
    }
}
