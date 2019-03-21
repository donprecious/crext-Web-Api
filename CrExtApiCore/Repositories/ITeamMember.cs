using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using CrExtApiCore.Models;
namespace CrExtApiCore.Repositories
{
    public interface ITeamMember
    {
        Task Create(Entities.TeamMembers teamMembers);
        Task Delete(string userId);
        Task<bool> Find(int id);
        Task<IEnumerable<TeamMembers>> List();
        Task<TeamMembers> Get(int id);
        Task<int> GetTeamMemberId(string userId);
        Task<bool> FindTeamMember(string userid);
        Task<IEnumerable<TeamMembers>> GetOrganisationTeamMembers(int orgId);
        Task<TeamMembers> GetTeamMemberByPhone(string phone);
        Task<bool> Save();

    }

   
}
