using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysFileInfoDto : BaseDto<long>
	{
		public string OriginalFileName { get; set; }

		public string RealFileName { get; set; }
	}
}
