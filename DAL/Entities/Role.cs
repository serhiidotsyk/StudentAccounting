using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Role
    {
        /// <summary>
        /// Gets or sets role id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets role description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Navigation property to users
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
