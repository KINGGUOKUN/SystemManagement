using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	/// <summary>
	/// 操作日志
	/// </summary>
	public class SysOperationLogDto
	{
		/// <summary>
		/// 控制器类名
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public long ID { get; set; }

		/// <summary>
		/// 日志业务名称
		/// </summary>
		public string LogName { get; set; }

		/// <summary>
		/// 日志类型
		/// </summary>
		public string LogType { get; set; }

		/// <summary>
		/// 详细信息
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// 控制器方法
		/// </summary>
		public string Method { get; set; }

		/// <summary>
		/// 是否操作成功
		/// </summary>
		public string Succeed { get; set; }

		/// <summary>
		/// 操作用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 操作用户名
		/// </summary>
		public string UserName { get; set; }
	}
}
