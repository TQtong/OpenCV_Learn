
using OpenCvSharp.Extensions;
using OpenCvSharp;
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

namespace ImageCannyByOpenCVSharp
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
            // 【1】读取图像
            Mat srcImage = Cv2.ImRead("1.jpg");
            Mat srcImage1 = srcImage.Clone();

            // 【2】显示原图
            Cv2.ImShow("原图", srcImage);

            // 【3】创建与src同类型和大小的矩阵(dst)
            Mat dstImage = new Mat(srcImage.Cols, srcImage.Rows, srcImage.Type());

            // 【4】将原图像转换为灰度图像
            Mat grayImage = new Mat();
            Cv2.CvtColor(srcImage1, grayImage, ColorConversionCodes.BGR2GRAY);

            // 【5】先用使用 3x3内核来降噪
            Mat edge = new Mat();
            Cv2.Blur(grayImage, edge, new OpenCvSharp.Size() { Width = 3, Height = 3 });

            // 【6】运行Canny算子
            Cv2.Canny(edge, edge, 3, 9, 3);

            // 【7】使用Canny算子输出的边缘图g_cannyDetectedEdges作为掩码，来将原图g_srcImage拷到目标图g_dstImage中
            srcImage1.CopyTo(dstImage, edge);

            // 【8】显示效果图 
            Cv2.ImShow("【效果图】整体方向Sobel", dstImage);

            // 【9】在pictureBox1中显示效果图
            image.Source = BitmapToImageSource(BitmapConverter.ToBitmap(dstImage));
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
