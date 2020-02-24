using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysNoticeDto : BaseDto<long>
	{
		public string Content { get; set; }

		public string Title { get; set; }

		public int? Type { get; set; }
	}
}
