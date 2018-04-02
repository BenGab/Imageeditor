using Imageditor.Contracts.Lockbits;
using Imageditor.Contracts.Maybe;
using System;

namespace Imageeditor.Services.Processing
{
    public class BitmapTileData<T>
    {
        public int Startwidht { get; private set; }
        public int Startheight { get; private set; }
        public int Endwidht { get; private set; }
        public int Endheight { get; private set; }
        public ILockBitmap Bitmap { get; private set; }
        public IMaybe<T> Value { get; private set; }
        public Action<ILockBitmap, int, int, IMaybe<T>> Fn { get; private set; }

        public BitmapTileData(int startwidth, int startHeight, int endwidth, int endheight, ILockBitmap bmp, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction)
        {
            Startwidht = startwidth;
            Startheight = startHeight;
            Endwidht = endwidth;
            Endheight = endheight;
            Bitmap = bmp;
            Value = value;
            Fn = converterFunction;
        }
    }
}
