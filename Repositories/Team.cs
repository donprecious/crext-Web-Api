using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrExtApiCore.Models;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace CrExtApiCore.Repositories
{
    public class Team : ITeam
    {
        private CrExtContext _context;

        public Team( CrExtContext context)
        {
            _context = context;
        }


        public async Task Create(CreateTeamDto team)
        {
          await  _context.Teams.AddAsync(new Teams()
            {
                Description = team.Description,
                Name = team.Name,
                ProjectId = team.ProjectId,
                OrganisationId =team.OrganisationId
            });
          
        }


        public async Task<IEnumerable<Teams>> List()
        {
            return await Task.Run(() => _context.Teams.ToList());
        }

        public async Task Delete(int teamId)
        {
          var team  =await _context.Teams.FindAsync(teamId);
                _context.Teams.Remove(team);
        }

        public async Task<bool> Find(int teamId)
        {
            if ((await _context.Teams.FindAsync(teamId)) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Teams> Get(int teamId)
        {
            return await _context.Teams.FindAsync(teamId);
        }

        public async Task<bool> Save()
        {
            return ((await _context.SaveChangesAsync()) >= 0);
        }

        public async Task<bool> TeamHasProject(int projectId, int teamId)
        {
            var hasProject = await Task.Run(()=> _context.Teams.Any(a => a.ProjectId == projectId && a.Id == teamId));
            return (hasProject) ? true : false;
        }
       // public async Task<bool> TeamBelongsToOrganisation(int teamId)
       // {
       ////   //  var hasProject = await Task.Run(() => _context.Teams.Any(a=>a.OrganisationId == );

       //     return (hasProject) ? true : false;
       // }

        public async Task<IEnumerable<Projects>> TeamProjects(int projectId)
        {
            // var r = _context.Projects.Select(b => b.Teams.Where(a => a.ProjectId == projectId).ToList());
            var p = await Task.Run(() => _context.Teams.Where(a => a.ProjectId == projectId)
                                        .Select(a => a.Project).ToList() );
            return p;
        }

        public async Task<IEnumerable<Teams>> OrganisationTeams(int organisationId)
        {
            //   var r = _context.Projects.Select(b => b.Teams.Where(a => a.ProjectId == organisationId).Include(a=>a).ToList());
            //var p = await Task.Run(() => _context.Teams.Where(a => a.OrganisationId == organisationId).Include(a=>a.t)
            //                            .ToList());
            var  r = await _context.Teams.Where(a => a.OrganisationId == organisationId)
                .Include(a => a.Project)
                .Include(a => a.TeamMembers)
                .ToListAsync();
            return r;
        }

        public Task Update(string name, string description, int projectId, int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
