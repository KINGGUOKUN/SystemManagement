using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysLoginLog
	{
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		public int ID { get; set; }

		public string IP { get; set; }

		public string LoginName { get; set; }

		public string Message { get; set; }

		public string Succeed { get; set; }

		public int? UserId { get; set; }
	}
}
