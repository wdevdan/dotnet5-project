
namespace DW.Company.Entities.Value
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Content { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public Response()
        {
            Success = true;
            StatusCode = 200;
        }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public Response()
        {
            Success = true;
            StatusCode = 200;
        }
    }
}
