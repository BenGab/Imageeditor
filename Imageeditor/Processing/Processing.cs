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
    }
}
