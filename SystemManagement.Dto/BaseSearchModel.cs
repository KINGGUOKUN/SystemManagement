using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    /// <summary>
    /// 查询条件基类
    /// </summary>
    public class BaseSearchModel
    {
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
    }
}
