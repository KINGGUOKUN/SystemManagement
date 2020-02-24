using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysDeptDto : BaseDto<long>
	{
		public string FullName { get; set; }

		public int? Num { get; set; }

		public long? Pid { get; set; }

		public string Pids { get; set; }

		public string SimpleName { get; set; }

		public string Tips { get; set; }

		public int? Version { get; set; }
	}
}
