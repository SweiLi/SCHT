using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BridgeDetectHelper.PictureLayout
{
    public class PictureLayoutFourSquare : AbstractPictureLayout
    {
        
        public override Grid CreateLayout(List<ImageSource> imgSrcList)
        {
            if (imgSrcList.Count < 4) throw new ArgumentException("图片数量不足");

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
            bdr_0.BorderThickness = new Thickness(0, 0, 1, 1);
            img_grid.Children.Add(bdr_0);
            Grid.SetRow(bdr_0, 0);
            Grid.SetColumn(bdr_0, 0);
            
            //cv_0.SizeChanged += Cv_0_SizeChanged;
            //cv_0.SizeChanged += Cv_SizeChanged;
            //cv_0.LayoutUpdated += Cv_0_LayoutUpdated;

            var bmp_1 = imgSrcList[1] as BitmapImage;
            var cv_1 = this.InitialImageCanvas(bmp_1);
            cv_1.BorderBrush = Brushes.Gray;
            cv_1.BorderThickness = new Thickness(0, 0, 0, 1);
            img_grid.Children.Add(cv_1);
            Grid.SetRow(cv_1, 0);
            Grid.SetColumn(cv_1, 1);

            var bmp_2 = imgSrcList[2] as BitmapImage;
            var cv_2 = this.InitialImageCanvas(bmp_2);
            cv_2.BorderBrush = Brushes.Gray;
            cv_2.BorderThickness = new Thickness(0, 0, 1, 0);
            img_grid.Children.Add(cv_2);
            Grid.SetRow(cv_2, 1);
            Grid.SetColumn(cv_2, 0);

            var bmp_3 = imgSrcList[3] as BitmapImage;
            var cv_3 = this.InitialImageCanvas(bmp_3);
            img_grid.Children.Add(cv_3);
            Grid.SetRow(cv_3, 1);
            Grid.SetColumn(cv_3, 1);
            //cv_3.SizeChanged += Cv_SizeChanged;
            
            

            return img_grid;
        }

        private void Cv_0_LayoutUpdated(object sender, EventArgs e)
        {
            var cv = sender as Canvas;
            if (cv != null && cv.Children.Count > 0)
            {
                //cv.HorizontalAlignment = HorizontalAlignment.Stretch;
                //cv.VerticalAlignment = VerticalAlignment.Stretch;

                var img = cv.Children[0] as Image;
                if (img.Height > img.Width)
                    img.Width = cv.ActualWidth;
                else
                    img.Height = cv.ActualHeight;

                //img.ClearValue(Canvas.LeftProperty);
                //img.ClearValue(Canvas.TopProperty);

                //Canvas.SetLeft(img, 0);
                //Canvas.SetTop(img, 0);

                //Canvas.SetLeft(img, 0);
                //Canvas.SetTop(img, 0);
                //grid.Children.Remove(cv);
                //grid.Children.Add(cv);
                //Grid.SetRow(cv, 1);
                //Grid.SetColumn(cv, 1);
                Console.WriteLine("cv left=" + Canvas.GetLeft(img).ToString() +
                    "; top=" + Canvas.GetTop(img).ToString());
            }
        }

        private void Cv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var cv = sender as Canvas;
            var bdr = cv.Parent as Border;
            
            if (cv != null && cv.Children.Count > 0)
            {
                //cv.HorizontalAlignment = HorizontalAlignment.Stretch;
                //cv.VerticalAlignment = VerticalAlignment.Stretch;

                var img = cv.Children[0] as Image;
                if (img.Height > img.Width)
                    img.Width = cv.ActualWidth;
                else
                    img.Height = cv.ActualHeight;
                
                img.ClearValue(Canvas.LeftProperty);
                img.ClearValue(Canvas.TopProperty);
                
                Canvas.SetLeft(img, 0);
                Canvas.SetTop(img, 0);
                
                //Canvas.SetLeft(img, 0);
                //Canvas.SetTop(img, 0);
                //grid.Children.Remove(cv);
                //grid.Children.Add(cv);
                //Grid.SetRow(cv, 1);
                //Grid.SetColumn(cv, 1);
                Console.WriteLine("cv left=" + Canvas.GetLeft(img).ToString() +
                    "; top=" + Canvas.GetTop(img).ToString());
            }

            bdr.UpdateLayout();
        }
    }
}
