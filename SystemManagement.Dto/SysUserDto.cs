using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysUserDto : BaseDto<long>
	{
		/// <summary>
		/// 账户
		/// </summary>
		public string Account { get; set; }

		public string Avatar { get; set; }

		public DateTime? Birthday { get; set; }

		public long? DeptId { get; set; }

		public string DeptName { get; set; }

		/// <summary>
		/// email
		/// </summary>
		public string Email { get; set; }

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

		public string RoleName { get; set; }

		/// <summary>
		/// 密码盐
		/// </summary>
		public string Salt { get; set; }

		public int? Sex { get; set; }

		public int Status { get; set; }

		public int? Version { get; set; }
	}
}
