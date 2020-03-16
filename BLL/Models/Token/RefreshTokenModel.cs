using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Token
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Revoked { get; set; }
        public int UserId { get; set; }
    }
}
