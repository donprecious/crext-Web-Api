﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrExtApiCore.Models;
using Entities;
namespace CrExtApiCore.Repositories
{
  public  interface IOrganisationAsync
    {
        int id { get; set; }
        Task Create(OrganisationDto org);
        Task<IEnumerable<Organisations>>List();
        Task<Organisations> Get(int organisationId);
        Task<bool> Find(int organisationId);
        Task<Entities.Organisations> UserOrganisation(string userId);
        Task<IEnumerable<Entities.UserOrganisation>> GetUserOranisaton(string userId);
        Task<bool> UserBelongsToOrganisation(string id, int orgId);
        Task AddUser(CreateUserOrganisationDto userOrgDto);
        Task Delete(int packageId);
        Task<bool> Save();
    }
}
