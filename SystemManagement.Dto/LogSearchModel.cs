using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    /// <summary>
    /// 日志检索条件
    /// </summary>
    public class LogSearchModel : BaseSearchModel
    {
        /// <summary>
        /// 日志范围开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 日志范围结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 日志名称
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }
    }
}
