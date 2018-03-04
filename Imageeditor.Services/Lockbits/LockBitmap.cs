using Imageditor.Contracts.Lockbits;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Imageeditor.Services.Lockbits
{
    public class LockBitmap : ILockBitmap
    {
        private Bitmap source;
        private IntPtr iptr = IntPtr.Zero;
        private BitmapData bitmapdata;

        public int Depth { get; private set; }

        public int Height { get; private set; }

        public byte[] Pixels { get; set; }

        public int Width { get; private set; }

        public LockBitmap(Bitmap bmp)
        {
            source = bmp;
            bitmapdata = null;
        }

        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;

        }

        public void LockBits()
        {
            Width = source.Width;
            Height = source.Height;

            int pixelCount = Width * Height;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            Depth = Bitmap.GetPixelFormatSize(source.PixelFormat);

            if (Depth != 8 && Depth != 24 && Depth != 32)
            {
                throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
            }

            bitmapdata = source.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);
            int step = Depth / 8;
            Pixels = new byte[pixelCount * step];
            iptr = bitmapdata.Scan0;
            Marshal.Copy(iptr, Pixels, 0, Pixels.Length);
        }

        public void SetPixel(int x, int y, Color color)
        {
            int cCount = Depth / 8;
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32)
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24)
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            {
                Pixels[i] = color.B;
            }

        }

        public void UnlockBits()
        {
            Marshal.Copy(Pixels, 0, iptr, Pixels.Length);
            source.UnlockBits(bitmapdata);
        }
    }
}
