using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using CrExtApiCore.Models;
namespace CrExtApiCore.Repositories
{
    public class TeamMember : ITeamMember
    {
        private CrExtContext _context;
        public TeamMember(CrExtContext context)
        {
            _context = context;
        }

        public async Task Create(Entities.TeamMembers teamMembers)
        {
          await  _context.TeamMembers.AddAsync( 
             teamMembers
          //    new TeamMembers
          //{
          //    Description = m.Description,
          //    ProjectId = m.ProjectId,
          //    TeamId = m.TeamId,
          //    UserId = m.UserId,

          //}
              );

        }

        public async Task<bool> Find(int id)
        {
           
            if ((await _context.TeamMembers.FindAsync(id)) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> FindTeamMember(string userid)
        {
            var find = await _context.TeamMembers.AnyAsync(a => a.UserId == userid);
            if (find )
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<TeamMembers>> List()
        {
            return await Task.Run(() => _context.TeamMembers.Include(a=>a.User).ToList());
        }

        public async Task<IEnumerable<TeamMembers>> GetOrganisationTeamMembers(int orgId)
        {
            var members = await _context.TeamMembers.Where(a => a.Projects.OrganisationId == orgId)
                            .Include(a=>a.User)
                            .Include(a=>a.Teams)
                            .Include(a=>a.Projects)
                            .OrderBy(a=>a.Teams.Name)
                            .ToListAsync();
            return members;
        }

        public async Task<TeamMembers> Get(int id)
        {
           
            return await _context.TeamMembers.FindAsync(id);
        }
        public async Task<TeamMembers> GetTeamMemberByPhone(string phone)
        {
            var team = _context.TeamMembers
                .Where(a => a.User.PhoneNumber == phone)
                .Include(a => a.User)
                .FirstOrDefaultAsync();
                

            return await team;
        }
        public Task Delete(string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Save()
        {
            return ((await _context.SaveChangesAsync()) >= 0);
        }

        public async Task<int> GetTeamMemberId(string userId)
        {
            var id = await Task.Run(()=> _context.TeamMembers.Where(a => a.UserId == userId).Select(a=>a.Id).SingleOrDefault());
            return id;
        }
    }
}
