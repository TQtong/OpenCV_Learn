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

namespace MixtureShowImageByOpenCVSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private const string path1 = @"1.png";
        private const string path2 = @"2.png";

        public MainWindow()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 读取图片
            Mat image1 = Cv2.ImRead(path1);
            Mat image2 = new Mat(path2);
            // 设置图像2需要显示的区域
            Mat imageROI = image1[new OpenCvSharp.Rect() { X=63, Y = 59, Height = image2.Rows, Width = image2.Cols }];
            // 重叠两张图片
            Cv2.AddWeighted(imageROI, 0.7, image2, 0.3, 0.0, imageROI);
            //显示图片到image控件
            image.Source = BitmapToImageSource(BitmapConverter.ToBitmap(image1));
            // 弹窗显示
            using (new OpenCvSharp.Window("合并", image1))
            {
                Cv2.WaitKey();
            }
        }


    }
}
