using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class SysCfgDto
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

		/// <summary>
		/// 创建人
		/// </summary>
		public long? CreateBy { get; set; }

		/// <summary>
		/// 创建时间/注册时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		public long ID { get; set; }

		/// <summary>
		/// 最后更新人
		/// </summary>
		public long? ModifyBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? ModifyTime { get; set; }
	}
}
