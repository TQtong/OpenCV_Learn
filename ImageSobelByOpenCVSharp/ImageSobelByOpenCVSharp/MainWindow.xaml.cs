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

namespace ImageSobelByOpenCVSharp
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
            Mat grad_x = new Mat();
            Mat grad_y = new Mat();
            Mat abs_grad_x = new Mat();
            Mat abs_grad_y = new Mat();
            Mat targetImage = new Mat();

            // 读取图像
            Mat srcImage = Cv2.ImRead("1.jpg");

            // 显示原图
            Cv2.ImShow("原图", srcImage);

            // 求X方向梯度
            Cv2.Sobel(srcImage, grad_x, MatType.CV_16S, 1, 0, 3, 1, 1, BorderTypes.Default);
            Cv2.ConvertScaleAbs(grad_x, abs_grad_x);
            Cv2.ImShow("效果X方向Sobel", abs_grad_x);

            // 求Y方向梯度
            Cv2.Sobel(srcImage, grad_y, MatType.CV_16S, 0, 1, 3, 1, 1, BorderTypes.Default);
            Cv2.ConvertScaleAbs(grad_y, abs_grad_y);
            Cv2.ImShow("效果Y方向Sobel", abs_grad_y);

            // 合成梯度(近似)
            Cv2.AddWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, targetImage);
            Cv2.ImShow("效果图整体方向Sobel", targetImage);

            // 在image控件上显示
            image.Source = BitmapToImageSource(BitmapConverter.ToBitmap(targetImage));

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
