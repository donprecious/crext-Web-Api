using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrExtApiCore.Repositories;
using Entities;
using CrExtApiCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CrExtApiCore.Repositories
{
    public class ReviewNotification : IReviewNotification
    {
        readonly CrExtContext _context;
        public ReviewNotification(CrExtContext context)
        {
            _context = context;
        }
   
        public async Task Create(ReviewNotifications review)
        {
            await _context.ReviewNotifications.AddAsync(review);
        }
        public async Task<bool> Find(int id)
        {
            
            if ((await _context.ReviewNotifications.FindAsync(id)) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<ReviewNotifications> Get(int id)
        {
           
            return await _context.ReviewNotifications.FindAsync(id);
        }
        public async Task Delete(int id)
        {
          await Task.Run(() => _context.ReviewNotifications.Remove(_context.ReviewNotifications.Find(id)));
        }

        public async Task<IEnumerable<ReviewNotifications>> List()
        {

            return await Task.Run(() => _context.ReviewNotifications
            .Include(a => a.ReviewAction)
            .Include(a => a.Review.TeamMember.Projects)
            .Include(a => a.Review.TeamMember.Projects.Organisation)
            
            .Include(a => a.Review)
            .Include(a => a.ReviewKind).ToList()
            );
        }

        public async Task<IEnumerable<ReviewNotifications>> OrganisationReviewList( int orgid)
        {
          
            return await _context.ReviewNotifications.Where(a => a.Review.TeamMember.Projects.Organisation.Id == orgid)
                .Include(a=>a.ReviewAction)
                .Include(a=>a.ReviewKind)
                .Include(a => a.Review)
                .Include(a => a.Review.Customer)
                .Include(a => a.Review.TeamMember)
                 .Include(a => a.Review.TeamMember.User)
                .Include(a => a.Review.TeamMember.Projects).ToListAsync();
        }

        public async Task<IEnumerable<ReviewNotifications>> ProjectReviewList(int projectid)
        {
            return await _context.ReviewNotifications.Where(a => a.Review.TeamMember.Projects.Id == projectid)
                
                .Include(a => a.Review)
                .Include(a => a.Review.Customer)
                .Include(a => a.Review.TeamMember)
                .Include(a => a.Review.TeamMember.User)
                .Include(a => a.Review.TeamMember.Projects).ToListAsync();
        }
        public async Task<IEnumerable<ReviewNotifications>> ProjectReviewQueryList(int projectId, int reviewActionId, string status)
        {
            return await _context.ReviewNotifications.Where(a => a.Review.TeamMember.Projects.Id == projectId && a.ReviewActionId == reviewActionId && a.Review.status == status)

                .Include(a => a.Review)
                
                .Include(a => a.Review.Customer)
                .Include(a => a.Review.TeamMember)
                .Include(a => a.Review.TeamMember.User)
                .Include(a => a.Review.TeamMember.Projects).ToListAsync();
        }
        public async Task<bool> Save()
        {
              return ((await _context.SaveChangesAsync()) >= 0);
        }

        public async Task<IEnumerable<ReviewNotifications>> GetCustomerReview(string id)
        {
            //return await Task.Run(() => _context.Reviews.Where(a => a.CustomerId == id)
            //.Include(a=>a.TeamMember)
            //.Include(a=>a.ReviewNotifications).ToList());
            var review = await Task.Run(() => _context.ReviewNotifications
                                               .Where(a => a.Review.CustomerId.ToString() == id)
                                               .Include(a => a.Review.TeamMember.User)
                                               .Include(a => a.Review)
                                               .Distinct()
                                               .ToList());
            return review;
        }

        public async Task CreateReply(Replies Reply)
        {
            await _context.Replies.AddAsync(Reply);
        }

        public async Task<IEnumerable<Replies>> GetReply(int id)
        {
            return await _context.Replies
                  .Include(a => a.Review)
                  .Include(a => a.Review.TeamMember)
                   .Include(a => a.Review.TeamMember.User)
                  .Include(a => a.Review.TeamMember.Projects)
                  .Include(a => a.Review.Customer).Where(a=>a.Id == id)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Replies>> GetAllReply()
        {
          return  await _context.Replies
                .Include(a=>a.Review)
                .Include(a=>a.Review.TeamMember)
                .Include(a => a.Review.TeamMember.User)
                .Include(a=>a.Review.TeamMember.Projects)
                .Include(a=>a.Review.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Replies>> GetOrganisationReplies(int orgId)
        {
            return await _context.Replies
                  .Where(a=>a.Review.TeamMember.Projects.OrganisationId == orgId)
                  .Include(a => a.Review)
                  .Include(a => a.Review.TeamMember)
                  .Include(a => a.Review.TeamMember.User)
                  .Include(a => a.Review.TeamMember.Projects)
                  .Include(a => a.Review.Customer)
                  .ToListAsync();
        }

        public async Task<IEnumerable<ReviewNotifications>> GetOrganisationQueries(int orgId)
        {
            return await _context.ReviewNotifications
                  .Where(a => a.Review.TeamMember.Projects.OrganisationId == orgId)
                  .Where(a=>a.ReviewKindId == 2)
                  .Include(a => a.Review)
                  .Include(a => a.Review.TeamMember.User)
                  .Include(a=>a.Review.Replies)
                  .Include(a => a.Review.Customer)
                  .ToListAsync();
        }
        public async Task<IEnumerable<Replies>> GetUserReply(string userId)
        {
            return await _context.Replies.Where(a=>a.Review.TeamMember.UserId == userId)
                  .Include(a => a.Review)
                  .Include(a => a.Review.TeamMember)
                   .Include(a => a.Review.TeamMember.User)
                  .Include(a => a.Review.TeamMember.Projects)
                  .Include(a => a.Review.Customer)
                  .ToListAsync();

        }

        public async Task<IEnumerable<Replies>> GetProjectReply(int projectId)
        {
            return await _context.Replies.Where(a => a.Review.TeamMember.ProjectId == projectId)
                  .Include(a => a.Review)
                  .Include(a => a.Review.TeamMember)
                  .Include(a=>a.Review.TeamMember.User)
                  .Include(a => a.Review.TeamMember.Projects)
                  .Include(a => a.Review.Customer)
                  .ToListAsync();

        }

        public async Task<bool> UpdateStatus(int id, string status)
        {
            var q = await _context.Reviews.FindAsync(id);
            if(q != null)
            {
                q.status = status;
                return true;
            }

            return false;
        }
        //Task<IEnumerable<Organisations>> IReviewNotification.OrganisationReviewList(int orgid)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task Update(int projectId, string name, string description)
        //{
        //    var project = await _context.Projects.FindAsync(projectId);
        //    if (project != null)
        //    {
        //        project.Description = description;
        //        project.Name = name;
        //        _context.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    }
        //}


    }
}
