using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagement.Entity
{
	/// <summary>
	/// 菜单角色关系
	/// </summary>
	[Table("SysRelation")]
	[Description("菜单角色关系")]
	public class SysRelation
	{
		[Key]
		[Column("ID")]
		public long ID { get; set; }

		[Column("MenuId")]
		public long MenuId { get; set; }

		[Column("RoleId")]
		public long RoleId { get; set; }
	}
}
