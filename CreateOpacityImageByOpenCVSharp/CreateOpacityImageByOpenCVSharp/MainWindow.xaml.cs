using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace CreateOpacityImageByOpenCVSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mat srcImage = CreateAlphaMat();
            image.Source = BitmapToImageSource(BitmapConverter.ToBitmap(srcImage));
            Cv2.ImWrite("透明Alpha.png", srcImage);
        }

        private Mat CreateAlphaMat()
        {
            Mat mat = new Mat(480, 640, MatType.CV_8UC4); // 四通道
            for (int i = 0; i < mat.Rows; i++) // 图像宽度
            {
                for (int j = 0; j < mat.Cols; j++) // 图像高度
                {
                    var rgba = new Vec4b();
                    // 蓝色
                    rgba.Item0 = 0xff;
                    // 绿色
                    rgba.Item1 = (byte)(((float)mat.Cols - j) / (float)mat.Cols * 0xff);
                    // 红色
                    rgba.Item2 = (byte)(((float)mat.Rows - i) / (float)mat.Rows * 0xff);
                    // 透明度
                    rgba.Item3 = (byte)((float)0.5 * (float)(rgba[1] + rgba[2]));
                    // 设置
                    mat.Set(i, j, rgba);
                }
            }
            return mat;
        }

        public ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return imageSource;
        }
    }
}
