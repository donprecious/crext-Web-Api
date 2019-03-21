using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrExtApiCore.Repositories;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace CrExtApiCore.Repositories
{
    public class Role : IRoleAsync
    {
        private CrExtContext _context;
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;

        public Role(CrExtContext context,  UserManager<Users> userManager, RoleManager<Roles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task CreateAsync(string name, string description)
        {
           
            await _context.Roles.AddAsync(new Roles
            {
                Name = name,
                Description = description
            });
        }

        public async Task<bool> CreateRole(string name)
        {

            var role = new Roles
            { Name = name};
              var create = await  _roleManager.CreateAsync(role);
            if (create.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task Delete(string roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            _context.Remove(role);

        }

        public async Task<bool> Find(string roleid)
        {
            var role = await _context.Roles.FindAsync(roleid);
            if (role != null)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> FindByName(string roleName)
        {
            var role =  await Task.Run(()=> _context.Roles.Where(a => a.Name == roleName).SingleOrDefault());
            if (role != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Roles> GetRoleAsync(string roleid)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            
            return role;
        }

        public  async Task<Roles> GetRoleByName(string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            return role;
        }

        public async Task<IEnumerable<Roles>> List()
        {
            var roles = await Task.Run(()=> _context.Roles.ToList());
            return roles;
        }

      
          public async Task<bool> Save()
            {
                return ((await _context.SaveChangesAsync()) >= 0);
            }

        public async Task<IEnumerable<string>> UserRoles(string id)
        {
            var user =  await _context.Users.FindAsync(id);
          var roles =  await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IEnumerable<string>> UserRolesByEmail(string email)
        {
            var user = await Task.Run(()=> _context.Users.Where(a=>a.Email== email).SingleOrDefault());
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

       
    }
}
