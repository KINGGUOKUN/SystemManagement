using System;
using System.Collections.Generic;
using System.Text;

namespace SystemManagement.Common
{
    /// <summary>
    /// JWT配置
    /// </summary>
    public class JWTConfig
    {
        /// <summary>
        /// 加密Key
        /// </summary>
        public string SymmetricSecurityKey { get; set; }

        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }
    }
}
