using ENDASPNET_PROJECT.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENDASPNET_PROJECT.Models.Roles
{
        public class Role
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<User> Users { get; set; }
            public Role()
            {
                Users = new List<User>();
            }

        public static implicit operator string(Role v)
        {
            throw new NotImplementedException();
        }
    }
}
