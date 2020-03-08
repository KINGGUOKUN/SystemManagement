using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysOperationLogDto
	{
		public string ClassName { get; set; }

		public DateTime? CreateTime { get; set; }

		public long ID { get; set; }

		public string LogName { get; set; }

		public string LogType { get; set; }

		/// <summary>
		/// 详细信息
		/// </summary>
		public string Message { get; set; }

		public string Method { get; set; }

		public string Succeed { get; set; }

		public long UserId { get; set; }

		public string UserName { get; set; }
	}
}
