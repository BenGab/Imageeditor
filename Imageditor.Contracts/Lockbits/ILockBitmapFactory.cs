using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageditor.Contracts.Lockbits
{
    public interface ILockBitmapFactory
    {
        ILockBitmap CreateLockBitmap(Bitmap bitmap);
    }
}
