using Imageditor.Contracts.Processing;
using System;
using Imageditor.Contracts.Maybe;
using Imageditor.Contracts.Lockbits;
using System.Threading;
using System.Collections.Generic;

namespace Imageeditor.Services.Processing
{
    public class ImageProcessing : IImageProcessing
    {
        public void AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction)
        {
            int tilewidth = bitmap.Width / 4;
            int tileheight = bitmap.Height / 2;
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 4; i++)
            {
                int endwidth = (i + 1) * tilewidth;
                for (int j = 0; j < 2; j++)
                {
                    int endheight = (j + 1) * tileheight;
                    var obj = new BitmapTileData<T>(i * tilewidth, j * tileheight, endwidth, endheight, bitmap, value, converterFunction);
                    Thread th = new Thread(new ParameterizedThreadStart(AdjustFunction<T>));
                    threads.Add(th);
                    th.Start(obj);
                }
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private void AdjustFunction<T>(object obj)
        {
            BitmapTileData<T> tiledata = (BitmapTileData<T>)obj;

            for (int i = tiledata.Startwidht; i < tiledata.Endwidht; i++)
            {
                for (int j = tiledata.Startheight; j < tiledata.Endheight; j++)
                {
                    tiledata.Fn(tiledata.Bitmap, i, j, tiledata.Value);
                }
            }
        }
    }
}
