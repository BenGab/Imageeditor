using Imageditor.Contracts.Processing;
using System;
using System.Drawing;
using Imageditor.Contracts.Maybe;

namespace Imageeditor.Services.Processing
{
    public class ImageProcessing : IImageProcessing
    {
        public void AdjustImage<T>(Bitmap bitmap, IMaybe<T> value, Action<Bitmap, int, int, IMaybe<T>> converterFunction)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    converterFunction(bitmap, i, j, value);
                }
            }
        }
    }
}
