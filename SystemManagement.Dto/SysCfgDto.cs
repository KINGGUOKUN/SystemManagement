using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	/// <summary>
	/// 系统配置
	/// </summary>
    public class SysCfgDto : BaseDto<long>
    {
		/// <summary>
		/// 备注
		/// </summary>
		public string CfgDesc { get; set; }

		/// <summary>
		/// 参数名
		/// </summary>
		public string CfgName { get; set; }

		/// <summary>
		/// 参数值
		/// </summary>
		public string CfgValue { get; set; }
	}
}
