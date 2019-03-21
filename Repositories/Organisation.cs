using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrExtApiCore.Models;
using CrExtApiCore.Repositories;

using Entities;
using Microsoft.EntityFrameworkCore;

namespace CrExtApiCore.Repositories
{
    public class Organisation : IOrganisationAsync
    {
      private  CrExtContext _context;

        public int id { get; set; }

        public Organisation(CrExtContext context)
        {
            _context = context;
        }
        public async Task Create(OrganisationDto org)
        {

            await _context.Organisations.AddAsync(new Organisations
            {
                Name = org.Name,
                Description = org.Description,
                PackageId = org.PackageId,
                UserId = org.UserId
            });
        }

        public async Task Delete(int organisationId)
        {
            var org = await _context.Organisations.FindAsync(organisationId);
            _context.Organisations.Remove(org);

        }

        public async Task<IEnumerable<Organisations>> List()
        {
            return await Task.Run(() => _context.Organisations.Include(a=>a.User).Include(a=>a.Package).Include(a=>a.Projects).ToList());
        }

        public async Task<Organisations> Get(int organisationId)
        {
            return await _context.Organisations.Where( a=> a.Id==organisationId)
                .Include(a => a.User)
                .Include(a => a.Package)
                .Include(a => a.Projects)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            return ((await _context.SaveChangesAsync()) >= 0);
        }

        public async Task<Entities.Organisations> UserOrganisation(string userId)
        {
            var org = await Task.Run(() => _context.Organisations.Where(a => a.UserId == userId).SingleOrDefault());
            return org;
        }

        public async Task<bool> Find(int organisationId)
        {
            if ((await _context.Organisations.FindAsync(organisationId)) != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UserBelongsToOrganisation(string id, int orgId)
        {
            var find = await _context.UserOrganisations.Where(a => a.UserId == id && a.OrganisationId == orgId).FirstOrDefaultAsync();
            if (find != null)
            {
                return true;
            }
            return false;
        }

        public async Task AddUser(CreateUserOrganisationDto userOrgDto)
        {
          await _context.UserOrganisations.AddAsync(new Entities.UserOrganisation
            {
                OrganisationId = userOrgDto.OrganisationId,
                UserId = userOrgDto.UserId
          });
        }

        public async Task<IEnumerable<Entities.UserOrganisation>> GetUserOranisaton(string userId)
        {
          return await Task.Run( ()=>  _context.UserOrganisations.Where(a => a.UserId == userId).Include(a => a.User).Include(a => a.Organisations).ToList());
        }
    }
}
