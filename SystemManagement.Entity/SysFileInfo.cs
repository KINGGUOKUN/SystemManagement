using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 文件
	/// </summary>
	[Table("SysFileInfo")]
	[Description("文件")]
	public class SysFileInfo : BaseEntity<long>
	{
		[StringLength(255)]
		[Column("OriginalFileName")]
		public string OriginalFileName { get; set; }

		[StringLength(255)]
		[Column("RealFileName")]
		public string RealFileName { get; set; }
	}
}
