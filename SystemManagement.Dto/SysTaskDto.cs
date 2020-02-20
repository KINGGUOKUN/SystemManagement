using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
	public class SysTaskDto
	{
		/// <summary>
		/// 是否允许并发
		/// </summary>
		public bool Concurrent { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public long CreateBy { get; set; }

		/// <summary>
		/// 创建时间/注册时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 定时规则
		/// </summary>
		public string Cron { get; set; }

		/// <summary>
		/// 执行参数
		/// </summary>
		public string Data { get; set; }

		/// <summary>
		/// 是否禁用
		/// </summary>
		public bool Disabled { get; set; }

		/// <summary>
		/// 执行时间
		/// </summary>
		public DateTime? ExecAt { get; set; }

		/// <summary>
		/// 执行结果
		/// </summary>
		public string ExecResult { get; set; }

		public long ID { get; set; }

		/// <summary>
		/// 执行类
		/// </summary>
		public string JobClass { get; set; }

		/// <summary>
		/// 任务组名
		/// </summary>
		public string JobGroup { get; set; }

		/// <summary>
		/// 最后更新人
		/// </summary>
		public long? ModifyBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? ModifyTime { get; set; }

		/// <summary>
		/// 任务名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 任务说明
		/// </summary>
		public string Note { get; set; }
	}
}
