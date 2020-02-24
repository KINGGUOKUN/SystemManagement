using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysRoleDto : BaseDto<long>
	{
		public long? DeptId { get; set; }

		public string Name { get; set; }

		public int? Num { get; set; }

		public long? Pid { get; set; }

		public string Tips { get; set; }

		public int? Version { get; set; }
	}
}
