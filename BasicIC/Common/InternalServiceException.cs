using System;

namespace BasicIC.Common
{
    [Serializable]
    public class InternalServiceException : Exception
    {
        public int errorCode { get; set; }

        public InternalServiceException() : base("") { }

        public InternalServiceException(string message, int errorCode = -1) : base(message)
        {
            this.errorCode = errorCode;
        }
    }
}