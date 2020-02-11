using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Common
{
    public class BusinessException : Exception
    {
        public BusinessException(int hResult, string message)
            : base(message)
        {
            base.HResult = hResult;
        }
    }
}
