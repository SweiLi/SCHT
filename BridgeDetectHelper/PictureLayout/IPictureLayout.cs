using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace BridgeDetectHelper.PictureLayout
{
    public interface IPictureLayout
    {
        Grid CreateLayout(List<ImageSource> imgSrcList);
    }
}
