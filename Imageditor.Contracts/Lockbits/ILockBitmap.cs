using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageditor.Contracts.Lockbits
{
    public interface ILockBitmap
    {
        void LockBits();

        void UnlockBits();

        Color GetPixel(int x, int y);

        void SetPixel(int x, int y, Color color);
    }
}
