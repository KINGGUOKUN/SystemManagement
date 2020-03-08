using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class LogSearchModel : BaseSearchModel
    {
        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string LogName { get; set; }

        public string LogType { get; set; }
    }
}
