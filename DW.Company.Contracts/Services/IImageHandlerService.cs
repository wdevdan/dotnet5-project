using DW.Company.Entities.Value;
using System.Drawing;

namespace DW.Company.Contracts.Services
{
    public interface IImageHandlerService
    {
        byte[] GetImage(ImageResizeParams query);
        byte[] ResizeImage(string path, ImageResizeParams resizeParams);
        byte[] ResizeImage(Bitmap image, ImageResizeParams resizeParams);

    }
}
