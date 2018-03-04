using Imageditor.Contracts.Processing;
using System;
using Imageditor.Contracts.Maybe;
using Imageditor.Contracts.Lockbits;

namespace Imageeditor.Services.Processing
{
    public class ImageProcessing : IImageProcessing
    {
        public void AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction)
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
