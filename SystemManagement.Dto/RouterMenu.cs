using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    /// <summary>
    /// 路由菜单
    /// </summary>
    public class RouterMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 菜编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父菜单编码
        /// </summary>
        public string PCode { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 菜单对应视图组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; } = false;

        /// <summary>
        /// 菜单元数据
        /// </summary>
        public MenuMeta Meta { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<RouterMenu> Children { get; private set; } = new List<RouterMenu>();
    }
}
