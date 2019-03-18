using System;
using System.Net;
using System.Net.Http;

namespace MyProject.Exceptions
{
    public class RequestException : HttpRequestException
    {
        public HttpStatusCode HttpCode { get; }

        public RequestException(HttpStatusCode code) : this(code, null, null)
        {
            
        }

        public RequestException(HttpStatusCode code, string message) : this(code, message, null)
        {

        }

        public RequestException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
        {
            HttpCode = code;
        }
    }
}
