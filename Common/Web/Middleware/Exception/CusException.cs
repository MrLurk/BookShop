using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Web.Middleware.Exception
{
    public class CusException : System.Exception
    {
        public CusException(string message) : base(message)
        {
        }

        public CusException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
