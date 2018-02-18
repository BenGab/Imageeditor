using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Imageeditor.Extensions
{
    public static class BitmapExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap
             (
                 bitmap.GetHbitmap(),
                 IntPtr.Zero,
                 Int32Rect.Empty,
                 BitmapSizeOptions.FromEmptyOptions()
             );
            return bitmapSource;
        }
    }
}
