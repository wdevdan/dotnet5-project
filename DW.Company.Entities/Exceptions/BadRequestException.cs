namespace DW.Company.Entities.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message) { }
    }
}
