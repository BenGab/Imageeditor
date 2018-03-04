using System.Drawing;

namespace Imageditor.Contracts.Lockbits
{
    public interface ILockBitmap
    {
        byte[] Pixels { get; set; }

        int Depth { get; }

        int Width { get; }

        int Height { get; }

        void LockBits();

        void UnlockBits();

        Color GetPixel(int x, int y);

        void SetPixel(int x, int y, Color color);
    }
}
