using System;

namespace DW.Company.Entities.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message) { }
    }
}
