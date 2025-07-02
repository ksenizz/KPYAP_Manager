using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Models;

namespace UsersApp.Services
{
    public class RoleService
    {
        private readonly DB _context;

        public RoleService(DB context)
        {
            _context = context;
        }

        public int GetRoleIdByName(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            return role != null ? role.Id : 0;
        }
    }
}