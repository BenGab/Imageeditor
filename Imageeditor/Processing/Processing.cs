using System.Drawing;

namespace Imageeditor.Processing
{
    public static class Processing
    {
        public static Bitmap ConverToGray(Bitmap bitmap)
        {
            var clone = bitmap.Clone(new Rectangle(0,0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);

            for (int i = 0; i< clone.Width; i++)
            {
                for (int j = 0; j< clone.Height; j++)
                {
                    Color colorPixel = clone.GetPixel(i, j);
                    int grayScale = (int)((colorPixel.R * 0.3) + (colorPixel.G * 0.59) + (colorPixel.B * 0.11));
                    clone.SetPixel(i, j, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }

            return clone;
        }

        public static Bitmap ConvertToNegative(Bitmap bitmap)
        {
            var clone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);

            for (int i = 0; i < clone.Width; i++)
            {
                for (int j = 0; j < clone.Height; j++)
                {
                    Color colorPixel = clone.GetPixel(i, j);
                    clone.SetPixel(i, j, Color.FromArgb(255 - colorPixel.R, 255 - colorPixel.G, 255 - colorPixel.G));
                }
            }

            return clone; 
        }

        public static Bitmap AdjustBrightness(Bitmap bitmap, int brightness)
        {
            Bitmap bmap = (bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat));
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j,
                    Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            return bmap;
        }

        public static Bitmap AdjustContrast(Bitmap bitmap, double contrast)
        {
            Bitmap bmap = (bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat));
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            return bmap;
        }
    }
}
