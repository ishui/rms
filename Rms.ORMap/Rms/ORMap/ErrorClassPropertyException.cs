namespace Rms.ORMap
{
    using System;

    public class ErrorClassPropertyException : ApplicationException
    {
        public ErrorClassPropertyException()
        {
        }

        public ErrorClassPropertyException(string errorMessage) : base(errorMessage)
        {
        }
    }
}

