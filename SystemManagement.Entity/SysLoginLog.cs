using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 登录日志
	/// </summary>
	[Table("SysLoginLog")]
	[Description("登录日志")]
	public class SysLoginLog
	{
		/// <summary>
		/// 创建时间
		/// </summary>
		[Description("创建时间")]
		[Column("CreateTime")]
		public DateTime? CreateTime { get; set; }

		[Key]
		[Column("ID")]
		public int ID { get; set; }

		[StringLength(255)]
		[Column("IP")]
		public string IP { get; set; }

		[StringLength(255)]
		[Column("LoginName")]
		public string LoginName { get; set; }

		[StringLength(255)]
		[Column("Message")]
		public string Message { get; set; }

		[StringLength(255)]
		[Column("Succeed")]
		public string Succeed { get; set; }

		[Column("UserId")]
		public int? UserId { get; set; }
	}
}
