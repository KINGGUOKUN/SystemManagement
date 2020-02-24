using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Common
{
    public class PagedModel<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        public int Count { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PageCount { get; set; }
    }
}
