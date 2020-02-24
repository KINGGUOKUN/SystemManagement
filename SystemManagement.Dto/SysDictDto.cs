using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysDictDto : BaseDto<long>
	{
		public string Name { get; set; }

		public string Num { get; set; }

		public long? Pid { get; set; }

		public string Tips { get; set; }
	}
}
