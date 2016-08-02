using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using BDH.IView;
using BDH.ViewModel;

namespace BridgeDetectHelper
{
    /// <summary>
    /// PictureEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PictureEditWindow : Window, IPictureEditView
    {
        public PictureEditWindow()
        {
            InitializeComponent();

            this.m_PopupMsg = new PopupMessage();

            this.DataContext = new PictureEditViewModel(this);

            foreach (Control child in DesignerCanvas.Children)
            {
                Selector.SetIsSelected(child, true);
            }
        }

        private IPopupMessage m_PopupMsg;
        public IPopupMessage PopupMessage
        {
            get
            {
                return this.m_PopupMsg;
            }
        }

        public string GetTestPicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择测试图片";
            ofd.Filter = "PNG JPG (*.png;*.jpg)|*.png;*.jpg";
            ofd.FilterIndex = 0;
            ofd.Multiselect = false;
            if (ofd.ShowDialog().Value)
            {
                return ofd.FileName;
            }

            return null;
        }

        public void SetEditorImage(MemoryStream ms)
        {
            var img_src = new BitmapImage();
            img_src.BeginInit();
            img_src.StreamSource = ms;
            img_src.EndInit();

            DesignerCanvas.Background = new ImageBrush(img_src);
        }

        private void chkSelect_Click(object sender, RoutedEventArgs e)
        {
            if (chkSelect.IsChecked.Value)
            {
                foreach (Control child in DesignerCanvas.Children)
                {
                    Selector.SetIsSelected(child, true);
                }
            }
            else
            {
                foreach (Control child in DesignerCanvas.Children)
                {
                    Selector.SetIsSelected(child, false);
                }
            }
        }

        public void SaveToFile()
        {
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "ss.png";

            FileStream fs = new FileStream(file_path, FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)DesignerCanvas.ActualWidth,
                (int)DesignerCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            bmp.Render(DesignerCanvas);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            this.SaveToFile();
        }

        private void btnAddEllipse_Click(object sender, RoutedEventArgs e)
        {
            ContentControl cc = new ContentControl(){ Width=100, Height=100, Padding= new Thickness(1) };
            Canvas.SetLeft(cc, 50);
            Canvas.SetTop(cc, 50);
            

            var rsc_style = this.FindResource("DesignerItemStyle");
            if (rsc_style != null)
            {
                Style cc_style = rsc_style as Style;
                cc.Style = cc_style;
            }

            Ellipse elli = new Ellipse() { IsHitTestVisible = false, StrokeThickness = 1, Stroke = Brushes.Red };
            cc.Content = elli;

            if (chkSelect.IsChecked.Value) Selector.SetIsSelected(cc, true);

            DesignerCanvas.Children.Add(cc);
        }
    }
}
