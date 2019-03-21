 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using CrExtApiCore.Models;

namespace CrExtApiCore.Repositories
{
  public  interface IReviewNotification
    {
        Task Create(ReviewNotifications reviewNotificationDto);
        //Task Update(int projectId, string name, string description);

        Task Delete(int id);
        Task<bool> Find(int id);
        Task<ReviewNotifications> Get(int id);

        Task<IEnumerable<ReviewNotifications>> List();

        Task<IEnumerable<ReviewNotifications>> OrganisationReviewList(int orgid);
        Task<IEnumerable<ReviewNotifications>> ProjectReviewList(int projectid);
        Task<IEnumerable<ReviewNotifications>> ProjectReviewQueryList(int projectId, int reviewActionId, string status);

        Task<IEnumerable<ReviewNotifications>> GetCustomerReview(string id);
        //Task<bool> OrganisationHasProject(int organisationId);
        Task CreateReply(Replies Reply);
        Task<IEnumerable<Replies>> GetAllReply();
        Task<IEnumerable<Replies>> GetUserReply(string userId);
        Task<IEnumerable<Replies>> GetProjectReply(int projectId);
        Task<bool> UpdateStatus(int id, string status);

        Task<IEnumerable<Replies>> GetReply(int id);
        Task<IEnumerable<Replies>> GetOrganisationReplies(int orgId);

        Task<IEnumerable<ReviewNotifications>> GetOrganisationQueries(int orgId);
        //Task<IEnumerable<Projects>> OrganisationProjects(int organisationId);
        Task<bool> Save();
    }
}
