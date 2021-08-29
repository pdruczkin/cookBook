using System.Collections.Generic;
using cookBook.Entities.Api;
using cookBook.Entities.Users;

namespace cookBook.Seeders
{
    public static class RolesSeeder
    {
        public static List<Role> GetRoles()
        {
            var list = new List<Role>()
            {
              new Role(){Name = "User"},
              new Role(){Name = "Manager"},
              new Role(){Name = "Admin"}
            };
            return list;
        }
    }
}