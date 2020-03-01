using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class UserProfile
    {
        public string Account { get; set; }

        public string Avatar { get; set; }

        public DateTime? Birthday { get; set; }

        public long? DeptId { get; set; }

        public string Dept { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string RoleId { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public int? Sex { get; set; }
    }
}
