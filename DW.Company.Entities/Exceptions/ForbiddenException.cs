namespace DW.Company.Entities.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
