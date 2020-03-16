using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.AppSettings
{
    public class AppSettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpiresMinutes { get; set; }
    }
}
