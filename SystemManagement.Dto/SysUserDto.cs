using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysUserDto
	{
		/// <summary>
		/// 账户
		/// </summary>
		public string Account { get; set; }

		public string Avatar { get; set; }

		public DateTime? Birthday { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public long? CreateBy { get; set; }

		/// <summary>
		/// 创建时间/注册时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		public long? DeptId { get; set; }

		/// <summary>
		/// email
		/// </summary>
		public string Email { get; set; }

		public long ID { get; set; }

		/// <summary>
		/// 最后更新人
		/// </summary>
		public long? ModifyBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? ModifyTime { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 角色id列表，以逗号分隔
		/// </summary>
		public string RoleId { get; set; }

		/// <summary>
		/// 密码盐
		/// </summary>
		public string Salt { get; set; }

		public int? Sex { get; set; }

		public bool? Status { get; set; }

		public int? Version { get; set; }
	}
}
