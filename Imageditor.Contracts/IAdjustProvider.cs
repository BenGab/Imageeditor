using Imageditor.Contracts.Lockbits;
using Imageditor.Contracts.Maybe;
using System;

namespace Imageditor.Contracts
{
    public interface IAdjustProvider
    {
        Action<ILockBitmap, int, int, IMaybe<T>> CreateAdjustFunction<T>(AdjustType type);
    }
}
