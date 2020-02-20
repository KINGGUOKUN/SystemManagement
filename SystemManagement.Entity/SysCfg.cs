using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 系统参数
	/// </summary>
	[Table("SysCfg")]
	[Description("系统参数")]
	public class SysCfg
	{
		/// <summary>
		/// 备注
		/// </summary>
		[Description("备注")]
		[StringLength(65535)]
		[Column("CfgDesc")]
		public string CfgDesc { get; set; }

		/// <summary>
		/// 参数名
		/// </summary>
		[Description("参数名")]
		[StringLength(256)]
		[Column("CfgName")]
		public string CfgName { get; set; }

		/// <summary>
		/// 参数值
		/// </summary>
		[Description("参数值")]
		[StringLength(512)]
		[Column("CfgValue")]
		public string CfgValue { get; set; }

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
	}
}
