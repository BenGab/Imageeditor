using Imageditor.Contracts.Processing;
using System;
using Imageditor.Contracts.Maybe;
using Imageditor.Contracts.Lockbits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imageeditor.Services.Processing
{
    public class ImageProcessing : IImageProcessing
    {
        public List<Task> AdjustImage<T>(ILockBitmap bitmap, IMaybe<T> value, Action<ILockBitmap, int, int, IMaybe<T>> converterFunction)
        {
            int tilewidth = bitmap.Width / 4;
            int tileheight = bitmap.Height / 2;
            List<Task> threads = new List<Task>();
            for (int i = 0; i < 4; i++)
            {
                int endwidth = (i + 1) * tilewidth;
                for (int j = 0; j < 2; j++)
                {
                    int currentwidth = i;
                    int currentheight = j;
                    int endheight = (currentheight + 1) * tileheight;                   
                    Task task = Task.Factory.StartNew(() =>
                    {
                        var obj = new BitmapTileData<T>(currentwidth * tilewidth, currentheight * tileheight, endwidth, endheight, bitmap, value, converterFunction);
                        AdjustFunction(obj);
                    });
                    threads.Add(task);
                }
            }

            return threads;
        }

        private void AdjustFunction<T>(BitmapTileData<T> tiledata)
        {
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
