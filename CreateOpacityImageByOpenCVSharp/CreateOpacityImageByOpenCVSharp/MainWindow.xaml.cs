using OpenCvSharp;
using System;
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

namespace CreateOpacityImageByOpenCVSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mat srcImage = CreateAlphaMat();
        }

        private Mat CreateAlphaMat()
        {
            Mat mat = new Mat(480, 640, MatType.CV_8UC4); // 四通道
            for (int i = 0; i < mat.Rows; i++) // 图像宽度
            {
                for (int j = 0; j < mat.Cols; j++) // 图像高度
                {

                }
            }
            return mat;
        }
    }
}
