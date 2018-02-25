using Imageditor.Contracts;
using System;
using Imageditor.Contracts.Maybe;
using System.Drawing;

namespace Imageeditor.Services
{
    public class AdjustProvider : IAdjustProvider
    {
        public Action<Bitmap, int, int, IMaybe<T>> CreateAdjustFunction<T>(AdjustType type)
        {
            switch (type)
            {
                case AdjustType.Brightness:
                    return (Action<Bitmap, int, int, IMaybe<T>>)(new Action<Bitmap, int, int, IMaybe<int>>(AdjustBrightness));
                case AdjustType.Contrast:
                    return (Action<Bitmap, int, int, IMaybe<T>>)(new Action<Bitmap, int, int, IMaybe<double>>(AdjustContrast));
                case AdjustType.GrayScale:
                    return (Action<Bitmap, int, int, IMaybe<T>>)(new Action<Bitmap, int, int, IMaybe<object>>(ConverToGray));
                case AdjustType.NegativeScale:
                    return (Action<Bitmap, int, int, IMaybe<T>>)(new Action<Bitmap, int, int, IMaybe<object>>(ConvertToNegative));
                default:
                    throw new NotSupportedException();
            }
        }

        private void AdjustBrightness(Bitmap bitmap, int i, int j, IMaybe<int> brightness)
        {
            Color c;
            c = bitmap.GetPixel(i, j);
            int cR = c.R + brightness.Value;
            int cG = c.G + brightness.Value;
            int cB = c.B + brightness.Value;

            if (cR < 0) cR = 1;
            if (cR > 255) cR = 255;

            if (cG < 0) cG = 1;
            if (cG > 255) cG = 255;

            if (cB < 0) cB = 1;
            if (cB > 255) cB = 255;

            bitmap.SetPixel(i, j,
            Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
        }

        private void AdjustContrast(Bitmap bitmap, int i, int j, IMaybe<double> contrast)
        {
            //if (contrast < -100) contrast = -100;
            //if (contrast > 100) contrast = 100;
            //contrast = (100.0 + contrast) / 100.0;
            //contrast *= contrast;
            Color c;

            c = bitmap.GetPixel(i, j);
            double pR = c.R / 255.0;
            pR -= 0.5;
            pR *= contrast.Value;
            pR += 0.5;
            pR *= 255;
            if (pR < 0) pR = 0;
            if (pR > 255) pR = 255;

            double pG = c.G / 255.0;
            pG -= 0.5;
            pG *= contrast.Value;
            pG += 0.5;
            pG *= 255;
            if (pG < 0) pG = 0;
            if (pG > 255) pG = 255;

            double pB = c.B / 255.0;
            pB -= 0.5;
            pB *= contrast.Value;
            pB += 0.5;
            pB *= 255;
            if (pB < 0) pB = 0;
            if (pB > 255) pB = 255;

            bitmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
        }

        private void ConverToGray(Bitmap bitmap, int i, int j, IMaybe<object> value)
        {
            Color colorPixel = bitmap.GetPixel(i, j);
            int grayScale = (int)((colorPixel.R * 0.3) + (colorPixel.G * 0.59) + (colorPixel.B * 0.11));
            bitmap.SetPixel(i, j, Color.FromArgb(grayScale, grayScale, grayScale));
        }

        public void ConvertToNegative(Bitmap bitmap, int i, int j, IMaybe<object> value)
        {
            Color colorPixel = bitmap.GetPixel(i, j);
            bitmap.SetPixel(i, j, Color.FromArgb(255 - colorPixel.R, 255 - colorPixel.G, 255 - colorPixel.G));
        }
    }
}
