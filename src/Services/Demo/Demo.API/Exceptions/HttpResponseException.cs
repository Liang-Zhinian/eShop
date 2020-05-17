// Author: 	sprite
// On:		2020/5/15
using System;
using System.Net;

namespace Demo.API.Exceptions
{
    #region snippet_HttpResponseException
    public class HttpResponseException : Exception
    {

        public HttpResponseException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.Status = (int)statusCode;
            this.Value = message;
        }

        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
    #endregion
}
