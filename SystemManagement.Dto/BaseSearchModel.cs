using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class BaseSearchModel
    {
        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }
    }
}
