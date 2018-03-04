using Imageditor.Contracts.Lockbits;
using Imageditor.Contracts.Maybe;
using System;

namespace Imageditor.Contracts.Processing
{
    public interface IImageProcessing
    {
        void AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction);
    }
}
