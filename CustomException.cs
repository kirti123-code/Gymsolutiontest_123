using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.Common
{
    class CustomException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }  // Optional if you want to map to HTTP status codes

        public CustomException(string message, string errorCode = "UNKNOWN_ERROR", int statusCode = 400)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
