﻿using System.Windows;
using System.Windows.Controls;
namespace BridgeDetectHelper
{
    public class ResizeRotateChrome : Control
    {
        static ResizeRotateChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeRotateChrome), new FrameworkPropertyMetadata(typeof(ResizeRotateChrome)));
        }
    }
}
