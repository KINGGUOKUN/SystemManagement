using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 部门
	/// </summary>
	[Table("SysDept")]
	[Description("部门")]
	public class SysDept
	{
		/// <summary>
		/// 创建人
		/// </summary>
		[Description("创建人")]
		[Column("CreateBy")]
		public long? CreateBy { get; set; }

		/// <summary>
		/// 创建时间/注册时间
		/// </summary>
		[Description("创建时间/注册时间")]
		[Column("CreateTime")]
		public DateTime? CreateTime { get; set; }

		[StringLength(255)]
		[Column("FullName")]
		public string FullName { get; set; }

		[Key]
		[Column("ID")]
		public long ID { get; set; }

		/// <summary>
		/// 最后更新人
		/// </summary>
		[Description("最后更新人")]
		[Column("ModifyBy")]
		public long? ModifyBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		[Description("最后更新时间")]
		[Column("ModifyTime")]
		public DateTime? ModifyTime { get; set; }

		[Column("Num")]
		public int? Num { get; set; }

		[Column("Pid")]
		public long? Pid { get; set; }

		[StringLength(255)]
		[Column("Pids")]
		public string Pids { get; set; }

		[StringLength(255)]
		[Column("SimpleName")]
		public string SimpleName { get; set; }

		[StringLength(255)]
		[Column("Tips")]
		public string Tips { get; set; }

		[Column("Version")]
		public int? Version { get; set; }
	}
}
