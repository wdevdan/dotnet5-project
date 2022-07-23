namespace DW.Company.Entities.Value
{
    public class ImageResizeCanvasParams
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ImageResizeParams
    {
        public string Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Cache { get; set; }
        public ImageResizeCanvasParams Canvas { get; set; }
        public bool IsBase64 { get; set; }
        public bool IsAdjustToCanvas { get; set; }
        public bool ThrowsErrorWhenNotFound { get; set; }
    }
}
