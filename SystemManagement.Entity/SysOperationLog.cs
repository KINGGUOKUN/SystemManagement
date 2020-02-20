using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 操作日志
	/// </summary>
	[Table("SysOperationLog")]
	[Description("操作日志")]
	public class SysOperationLog
	{
		[StringLength(255)]
		[Column("ClassName")]
		public string ClassName { get; set; }

		[Column("CreateTime")]
		public DateTime? CreateTime { get; set; }

		[Key]
		[Column("ID")]
		public long ID { get; set; }

		[StringLength(255)]
		[Column("LogName")]
		public string LogName { get; set; }

		[StringLength(255)]
		[Column("LogType")]
		public string LogType { get; set; }

		/// <summary>
		/// 详细信息
		/// </summary>
		[Description("详细信息")]
		[StringLength(65535)]
		[Column("Message")]
		public string Message { get; set; }

		[StringLength(255)]
		[Column("Method")]
		public string Method { get; set; }

		[StringLength(255)]
		[Column("Succeed")]
		public string Succeed { get; set; }

		[Column("UserId")]
		public int? UserId { get; set; }
	}
}
