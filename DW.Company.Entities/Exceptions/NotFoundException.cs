﻿namespace DW.Company.Entities.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
