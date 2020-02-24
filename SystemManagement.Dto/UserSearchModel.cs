using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class UserSearchModel
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

        public string Name
        { 
            get;
            set;
        }

        public string Account
        {
            get;
            set;
        }
    }
}
