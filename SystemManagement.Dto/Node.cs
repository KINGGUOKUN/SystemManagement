using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class Node<T>
    {
        public T ID { get; set; }

        public T PID { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }

        public List<Node<T>> Children { get; private set; } = new List<Node<T>>();
    }
}
