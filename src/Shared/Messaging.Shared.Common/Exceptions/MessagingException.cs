using System;

namespace Messaging.Common.Exceptions
{
    public class HomeRunException : Exception
    {
        public string Code { get; }

        public HomeRunException()
        {
        }

        public HomeRunException(string code)
        {
            Code = code;
        }

        public HomeRunException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public HomeRunException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public HomeRunException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public HomeRunException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}