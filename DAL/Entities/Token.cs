using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class RefreshToken
    {
        /// <summary>
        /// gets or sets refresh token id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// gets or sets refresh token expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// gets or sets whether refresh token is revoked
        /// </summary>
        public bool Revoked { get; set; }

        /// <summary>
        /// navigation property to user
        /// </summary>
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
