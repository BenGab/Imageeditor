using Imageditor.Contracts.Processing;
using System;
using Imageditor.Contracts.Maybe;
using Imageditor.Contracts.Lockbits;
using System.Threading.Tasks;

namespace Imageeditor.Services.Processing
{
    public class ImageProcessing : IImageProcessing
    {
        public void AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction)
        {
            Parallel.For(0, bitmap.Width, (i) =>
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    converterFunction(bitmap, i, j, value);
                }
            });
        }
    }
}
