using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class MenuNode
    {
        #region Properties

        public long ID { get; set; }

        public long? ParentId { get; set; }

        public string Name { get; set; }

        public int Levels { get; set; }

        public bool IsMenu { get; set; }

        public string IsMenuName => this.IsMenu ? "是" : "否";

        public int Status { get; set; }

        public string StatusName => this.Status == 1 ? "启用" : "禁用";

        public int Num { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string Icon { get; set; }

        public string Code { get; set; }

        public string PCode { get; set; }

        public string Component { get; set; }

        public bool Hidden { get; set; }

        public List<MenuNode> Children { get; private set; } = new List<MenuNode>();

        #endregion
    }
}
