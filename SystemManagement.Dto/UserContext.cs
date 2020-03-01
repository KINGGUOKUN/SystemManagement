using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Dto
{
    public class UserContext
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public UserProfile Profile { get; set; }

        public List<string> Roles { get; private set; } = new List<string>();

        public List<string> Permissions { get; private set; } = new List<string>();
    }
}
