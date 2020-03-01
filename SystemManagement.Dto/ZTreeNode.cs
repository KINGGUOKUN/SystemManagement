using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class ZTreeNode<TKey, TData>
    {
        public TKey ID { get; set; }

        public TKey PID { get; set; }

        public string Name { get; set; }

        public bool Open { get; set; }

        public bool Checked { get; set; }

        public TData Data { get; set; }

        public static ZTreeNode<TKey, TData> CreateParent()
        {
            ZTreeNode<TKey, TData> node = new ZTreeNode<TKey, TData>
            {
                Checked = true,
                ID = default(TKey),
                Name = "顶级",
                Open = true,
                PID =default(TKey)
            };

            return node;
        }
    }
}
