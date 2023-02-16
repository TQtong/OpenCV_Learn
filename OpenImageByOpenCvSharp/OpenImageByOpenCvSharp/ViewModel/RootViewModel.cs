using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenImageByOpenCvSharp.Model.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenImageByOpenCvSharp.ViewModel
{
    public class RootViewModel : BindableBase
    {
        private ImageSource myVar;

        public ImageSource MyProperty
        {
            get => myVar;
            set 
            { 
                myVar = value;
                OnPropertyChanged();
            }
        }


        private const string path = @"C:\Users\TQ\Desktop\save\测试1232\1.矿产类，正常柱状图，有分析数据\image\1.矿产类，正常柱状图，有分析数据_1.png";
        public void OpenCommand()
        {
            // 读取图片
            Mat mat = new Mat(path);

            #region 第一种显示
            //// 显示图片
            //Cv2.ImShow("image", mat);
            //// 检查按键
            //Cv2.WaitKey(0);
            #endregion

            MyProperty = BitmapToImageSource(BitmapConverter.ToBitmap(mat));

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
