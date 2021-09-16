using System;

namespace Messaging.Common.Exceptions
{
    public class MessagingException : Exception
    {
        public string Code { get; }

        public MessagingException()
        {
        }

        public MessagingException(string code)
        {
            Code = code;
        }

        public MessagingException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public MessagingException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public MessagingException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public MessagingException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}