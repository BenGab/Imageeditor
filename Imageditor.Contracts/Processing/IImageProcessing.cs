using Imageditor.Contracts.Maybe;
using System;
using System.Drawing;

namespace Imageditor.Contracts.Processing
{
    public interface IImageProcessing
    {
        void AdjustImage<T>(Bitmap bitmap, IMaybe<T> value, Action<Bitmap, int, int, IMaybe<T>> converterFunction);
    }
}
