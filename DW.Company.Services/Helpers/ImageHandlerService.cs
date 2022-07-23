using DW.Company.Common;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace DW.Company.Services.Helpers
{

    public class ImageHandlerService : IImageHandlerService
    {
        private readonly IEnvironmentSettings _environmentSettings;

        public ImageHandlerService(IEnvironmentSettings environmentSettings)
        {
            _environmentSettings = environmentSettings;
        }

        public byte[] GetImage(ImageResizeParams resizeParams)
        {
            var _path = Path.Combine(Directory.GetCurrentDirectory(), _environmentSettings.IMAGESDIRECTORY, resizeParams.Image);

            if (string.IsNullOrEmpty(resizeParams.Image) || !File.Exists(_path))
            {
                if (resizeParams.ThrowsErrorWhenNotFound) throw new NotFoundException(ExceptionMessages.ERR0001);
                _path = Constants.IMAGENOTFOUND;
            }

            byte[] _buffer = ResizeImage(
                _path,
                resizeParams
            );
            return _buffer;
        }

        private bool IsBase64String(string value)
        {
            value = value.Trim();
            return value.Length % 4 == 0 && Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public byte[] ResizeImage(string path, ImageResizeParams resizeParams)
        {
            Bitmap _image = null;
            if (IsBase64String(path))
            {
                var _imageBytes = Convert.FromBase64String(path);
                using (var _ms = new MemoryStream(_imageBytes))
                    _image = new Bitmap(_ms);
            }
            else
            {
                _image = new Bitmap(path, false);
            }
            return ResizeImage(_image, resizeParams);
        }

        private int GetProportion(double value, double divider, double multiplier)
        {
            if (value > 0)
            {
                var _ratio = value / divider;
                return (int)(multiplier * _ratio);
            }
            return (int)multiplier;
        }

        private ImageResizeParams AdjustToCanvas(ImageResizeParams resizeParams, int originalHeight, int originalWidth)
        {
            if (resizeParams.IsAdjustToCanvas && resizeParams.Canvas != null)
            {
                if (resizeParams.Height == 0 && resizeParams.Width == 0)
                {
                    resizeParams.Width = originalWidth;
                    resizeParams.Height = originalHeight;
                }

                if (resizeParams.Height > resizeParams.Width)
                {
                    resizeParams.Height = resizeParams.Canvas.Height;
                    resizeParams.Width = GetProportion(resizeParams.Height, originalHeight, originalWidth);
                }
                else
                {
                    resizeParams.Width = resizeParams.Canvas.Width;
                    resizeParams.Height = GetProportion(resizeParams.Width, originalWidth, originalHeight);
                }

                while (true)
                {
                    var _widthDiff = resizeParams.Canvas.Width - resizeParams.Width;
                    var _heightDiff = resizeParams.Canvas.Height - resizeParams.Height;

                    if (_widthDiff >= 0 && _heightDiff >= 0)
                    {
                        break;
                    }
                    else if (_widthDiff < _heightDiff)
                    {
                        resizeParams.Width = resizeParams.Canvas.Width;
                        resizeParams.Height = GetProportion(resizeParams.Width, originalWidth, originalHeight);
                    }
                    else if (_heightDiff < _widthDiff)
                    {
                        resizeParams.Height = resizeParams.Canvas.Height;
                        resizeParams.Width = GetProportion(resizeParams.Height, originalHeight, originalWidth);
                    }
                }
            }
            return resizeParams;
        }

        private ImageResizeParams AdjustProportion(ImageResizeParams resizeParams, int originalHeight, int originalWidth)
        {
            if (resizeParams.Height > 0 || resizeParams.Width > 0)
            {
                if (resizeParams.Height > 0)
                {
                    resizeParams.Width = GetProportion(resizeParams.Height, originalHeight, originalWidth);
                }
                else if (resizeParams.Width > 0)
                {
                    resizeParams.Height = GetProportion(resizeParams.Width, originalWidth, originalHeight);
                }
            }
            return resizeParams;
        }

        public byte[] ResizeImage(Bitmap image, ImageResizeParams resizeParams)
        {
            var _originalHeight = image.Height;
            var _originalWidth = image.Width;

            resizeParams = AdjustProportion(resizeParams, _originalHeight, _originalWidth);
            resizeParams = AdjustToCanvas(resizeParams, _originalHeight, _originalWidth);

            if (resizeParams.Height == 0 || resizeParams.Width == 0)
            {
                resizeParams.Height = _originalHeight;
                resizeParams.Width = _originalWidth;
            }

            using (var _imageResized = new Bitmap(resizeParams.Width, resizeParams.Height))
            {
                _imageResized.SetResolution(96, 96);

                var _graphics = Graphics.FromImage(_imageResized);
                _graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                _graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                _graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                // TODO: Here we need a param to make the canvas transparent or white
                _graphics.Clear(false ? Color.Transparent : Color.White);

                // doing resize
                _graphics.DrawImage(
                    image,
                    new Rectangle(0, 0, resizeParams.Width, resizeParams.Height),
                    new Rectangle(0, 0, _originalWidth, _originalHeight),
                    GraphicsUnit.Pixel
                );

                if (resizeParams.Canvas != null)
                    return ApplyCanvasToResizedImage(_imageResized, resizeParams);

                using (var _outStream = new MemoryStream())
                {

                    _imageResized.Save(_outStream, ImageFormat.Png);
                    return _outStream.ToArray();
                }
            }
        }

        private byte[] ApplyCanvasToResizedImage(Bitmap image, ImageResizeParams resizeParams)
        {
            var _marginX = (resizeParams.Canvas.Width - image.Width) / 2;
            var _marginY = (resizeParams.Canvas.Height - image.Height) / 2;

            using (var _outStream = new MemoryStream())
            {
                using (var _imageWithCanvas = new Bitmap(resizeParams.Canvas.Width, resizeParams.Canvas.Height))
                {
                    var _graphics = Graphics.FromImage(_imageWithCanvas);
                    _graphics.Clear(Color.White);
                    _graphics.DrawImage(
                        image,
                        _marginX,
                        _marginY,
                        resizeParams.Width,
                        resizeParams.Height
                    );

                    _imageWithCanvas.Save(_outStream, ImageFormat.Png);
                    return _outStream.ToArray();
                }
            }
        }
    }
}
