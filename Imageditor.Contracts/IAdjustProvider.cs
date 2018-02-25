using Imageditor.Contracts.Maybe;
using System;
using System.Drawing;

namespace Imageditor.Contracts
{
    public interface IAdjustProvider
    {
        Action<Bitmap, int, int, IMaybe<T>> CreateAdjustFunction<T>(AdjustType type);
    }
}
