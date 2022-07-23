namespace DW.Company.Entities.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
