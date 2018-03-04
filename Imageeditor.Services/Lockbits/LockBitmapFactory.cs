using Imageditor.Contracts.Lockbits;
using System.Drawing;

namespace Imageeditor.Services.Lockbits
{
    public class LockBitmapFactory : ILockBitmapFactory
    {
        public ILockBitmap CreateLockBitmap(Bitmap bitmap)
        {
            return new LockBitmap(bitmap);
        }
    }
}
