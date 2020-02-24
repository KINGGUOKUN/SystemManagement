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
	public class SysDept : BaseEntity<long>
	{
		[StringLength(255)]
		[Column("FullName")]
		public string FullName { get; set; }

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
