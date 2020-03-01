using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class RouterMenu
    {
        public long ID { get; set; }

        public long? ParentId { get; set; }

        public string Code { get; set; }

        public string PCode { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public string Name { get; set; }

        public int Num { get; set; }

        public bool Hidden { get; set; } = false;

        public MenuMeta Meta { get; set; }

        public List<RouterMenu> Children { get; private set; } = new List<RouterMenu>();
    }
}
