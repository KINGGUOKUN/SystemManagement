using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Common
{
    public enum ErrorCode
    {
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500,
        NotImplemented = 501,
    }
}
