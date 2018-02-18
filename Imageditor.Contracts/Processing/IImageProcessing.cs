using System.Drawing;

namespace Imageditor.Contracts.Processing
{
    public interface IImageProcessing
    {
        void ConverToGray(Bitmap bitmap);

        void ConvertToNegative(Bitmap bitmap);

        void AdjustBrightness(Bitmap bitmap, int brightness);

        void AdjustContrast(Bitmap bitmap, double contrast);
    }
}
