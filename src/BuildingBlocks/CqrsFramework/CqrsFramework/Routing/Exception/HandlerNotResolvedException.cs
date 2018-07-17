using System;

namespace CqrsFramework.Routing.Exception
{
    public class HandlerNotResolvedException : ArgumentNullException
    {
        public HandlerNotResolvedException(string paramName)
            : base($"Type {paramName} was resolved to null from servicelocator")
        {
        }
    }
}
