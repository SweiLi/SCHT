using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
namespace BridgeDetectHelper
{
    /// <summary>
    /// BdhPictureEditor.xaml 的交互逻辑
    /// </summary>
    public partial class BdhPictureEditor : UserControl
    {
        public BdhPictureEditor()
        {
            InitializeComponent();

            foreach (Control child in DesignerCanvas.Children)
            {
                Selector.SetIsSelected(child, true);
            }
        }

        public void LoadImageFrom(MemoryStream ms)
        {
            var img_src = new BitmapImage();
            img_src.BeginInit();
            img_src.StreamSource = ms;
            img_src.EndInit();

            DesignerCanvas.Background = new ImageBrush(img_src);
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
            //this.SaveToFile();

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
    }
}
