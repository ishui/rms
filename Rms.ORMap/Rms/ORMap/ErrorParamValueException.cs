namespace Rms.ORMap
{
    using System;

    public class ErrorParamValueException : ApplicationException
    {
        public ErrorParamValueException()
        {
        }

        public ErrorParamValueException(string errorMessage) : base(errorMessage)
        {
        }
    }
}

