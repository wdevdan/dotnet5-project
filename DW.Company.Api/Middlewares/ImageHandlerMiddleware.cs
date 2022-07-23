using Microsoft.AspNetCore.Http;
using DW.Company.Contracts.Services;
using DW.Company.Entities.Value;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DW.Company.Api.Services
{
    public class ImageHandlerMiddleware
    {
        public ImageHandlerMiddleware(RequestDelegate next)
        {
            _ = next;
        }

        public async Task Invoke(HttpContext context, IImageHandlerService service)
        {
            var _buffer = service.GetImage(MakeParams(context.Request.Query));
            context.Response.Clear();
            context.Response.ContentType = "Image/png";
            await context.Response.Body.WriteAsync(_buffer, 0, _buffer.Length);
        }

        private ImageResizeParams MakeParams(IQueryCollection query)
        {
            ImageResizeCanvasParams _canvas = null;
            var _canvasFromQuery = query["canvas"].ToString();
            if (!string.IsNullOrEmpty(_canvasFromQuery) && new Regex("^[0-9]{1,}x[0-9]{1,}$").IsMatch(_canvasFromQuery))
            {
                _canvas = new ImageResizeCanvasParams
                {
                    Width = int.Parse(_canvasFromQuery.Split('x')[0]),
                    Height = int.Parse(_canvasFromQuery.Split('x')[1])
                };
            }

            var _widthFromQuery = query["w"].ToString();
            var _heightFromQuery = query["h"].ToString();
            var centerFromQuery = query["center"].ToString();
            return new ImageResizeParams
            {
                Image = query["src"].ToString(),
                Width = !string.IsNullOrEmpty(_widthFromQuery) && int.TryParse(_widthFromQuery, out int _width) ? _width : 0,
                Height = !string.IsNullOrEmpty(_heightFromQuery) && int.TryParse(_heightFromQuery, out int _height) ? _height : 0,
                Cache = query["cache"].ToString().Equals("true"),
                Canvas = _canvas,
                IsBase64 = query["base64"].ToString().Equals("true"),
                IsAdjustToCanvas = string.IsNullOrEmpty(centerFromQuery) || centerFromQuery.Equals("true"),
                ThrowsErrorWhenNotFound = query["throw_not_found"].ToString().Equals("true"),
            };
        }
    }
}

/*
if (string.IsNullOrEmpty(Cache) || !Cache.Equals("false"))
{
   context.Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
   context.Response.Cache.SetMaxAge(new TimeSpan(365, 0, 0, 0, 0));
   context.Response.Cache.SetCacheability(HttpCacheability.Public);
}

if (!string.IsNullOrEmpty(ImgBase64) && ImgBase64.Equals("S"))
{
   context.Response.Clear();
   context.Response.ContentType = "text/plain";
   context.Response.Write(string.Format("data:{0};base64, {1}", core.getContentType(IdImagem), Convert.ToBase64String(buffer)));
}
else
{
   context.Response.OutputStream.Write(buffer, 0, buffer.Length);
}
*/