using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysNoticeDto
	{
		public string Content { get; set; }

		public long? CreateBy { get; set; }

		public DateTime? CreateTime { get; set; }

		public long ID { get; set; }

		public long? ModifyBy { get; set; }

		public DateTime? ModifyTime { get; set; }

		public string Title { get; set; }

		public int? Type { get; set; }
	}
}
