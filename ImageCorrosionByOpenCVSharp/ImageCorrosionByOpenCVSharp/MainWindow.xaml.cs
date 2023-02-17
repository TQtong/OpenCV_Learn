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

namespace ImageCorrosionByOpenCVSharp
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
            Mat srcImage = Cv2.ImRead("1.jpg");

            // 在窗口中显示原画
            Cv2.ImShow("原图", srcImage);

            // 进行腐蚀操作
            Mat element = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size() { Width = 15, Height = 15 });
            Mat targetImage = new Mat();
            Cv2.Erode(srcImage, targetImage, element);

            // 输入图像到image控件
            image.Source = BitmapToImageSource(BitmapConverter.ToBitmap(targetImage));

            // 弹窗显示图像
            using (new OpenCvSharp.Window("效果", targetImage))
            {
                Cv2.WaitKey();
            }
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
