using Imageditor.Contracts.Lockbits;
using Imageditor.Contracts.Maybe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imageditor.Contracts.Processing
{
    public interface IImageProcessing
    {
        List<Task> AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value,
            Action<ILockBitmap, int, int, IMaybe<T>> converterFunction);
    }
}
