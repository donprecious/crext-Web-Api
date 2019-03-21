using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CrExtApiCore.Models;
using CrExtApiCore.Repositories;
using AutoMapper;
using Entities;

namespace CrExtApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Review")]
    [EnableCors("AllowOrigin")]
    public class CustomerReviewController : Controller
    {
        private IReviewNotification _reviewNotification;
        private IReview _review;
        private IReviewAction _reviewAction;
        private IReviewKind _reviewKind;
        private IOrganisationAsync _organisation;


        public CustomerReviewController(IReviewNotification reviewNotification,
            IReview review, IReviewAction reviewAction, IReviewKind reviewKind, IOrganisationAsync organisation)
        {
            _review = review;
            _reviewNotification = reviewNotification;
            _reviewAction = reviewAction;
            _reviewKind = reviewKind;
            _organisation = organisation;
        }

        [HttpGet("GetReview/{id}", Name = "GetReview")]
        public async Task<IActionResult> GetReview(int id)
        {
            if (!await _reviewNotification.Find(id)) return NotFound("Review not Found");
            var review = await _reviewNotification.Get(id);
            return Ok(review);
        }

        [HttpGet("GetCustomerReview/{id}")]
        public async Task<IActionResult> GetCustomerReview(string id)
        {

            var review = await _reviewNotification.GetCustomerReview(id);
            return Ok(review);
        }

        [HttpGet("GetReviewKind")]
        public async Task<IActionResult> GetReviewKind()
        {
            var review = await _reviewKind.List();
            return Ok(review);
        }


        [HttpGet("GetReviewAction")]
        public async Task<IActionResult> GetReviewAction()
        {
            var review = await _reviewAction.List();
            return Ok(review);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateReviewAndNotitficationDto reviewAndNotificationDto)
        {
            if (reviewAndNotificationDto == null) return NotFound();

            if (ModelState.IsValid)
            {
                var reviewCreateMapped = new CreateReviewDto
                {
                    Comment = reviewAndNotificationDto.review.Comment,
                    CustomerId = reviewAndNotificationDto.review.CustomerId,
                    TeamMemberId = reviewAndNotificationDto.review.TeamMemberId,
                    Status = Helpers.ReviewStatus.UNREAD.ToString()

            };

                var reviewNotificationCreateMapped = new CreateReviewNotificationsDto
                {
                    ReviewActionId = reviewAndNotificationDto.reviewNotification.ReviewActionId,
                    ReviewKindId = reviewAndNotificationDto.reviewNotification.ReviewKindId,
                    StartDate = reviewAndNotificationDto.reviewNotification.StartDate,
                    EndDate = reviewAndNotificationDto.reviewNotification.EndDate,
                    DateAdded = DateTime.UtcNow
                };

                //var reviewMapped = Mapper.Map<CreateReviewDto>(reviewAndNotificationDto);
                //var notificationMapped = Mapper.Map<CreateReviewNotificationsDto>(reviewAndNotificationDto);

                // var targetReview = Mapper.Map<Reviews>(reviewMapped);
                var targetReview = Mapper.Map<Reviews>(reviewCreateMapped);

                await _review.Create(targetReview);
                //
                if (!await _review.Save())
                {
                    return StatusCode(500, "Server Error, Something went wrong with our server");
                }
                var createdReview = Mapper.Map<Reviews>(targetReview);

                //notificationMapped.ReviewId = createdReview.Id;
                reviewNotificationCreateMapped.ReviewId = createdReview.Id;
                // var targetNotification = Mapper.Map<ReviewNotifications>(notificationMapped);
                var targetNotification = Mapper.Map<ReviewNotifications>(reviewNotificationCreateMapped);

                
                
                await _reviewNotification.Create(targetNotification);

                if (!await _review.Save())
                {
                    return StatusCode(500, "Server Error, Something went wrong with our server");
                }

                var createdNotification = Mapper.Map<ReviewNotifications>(targetNotification);
                return CreatedAtRoute("GetReview", new { id = createdNotification.Id }, createdNotification);
                //return CreatedA("/GetReview/"+  createdNotification.Id, createdNotification);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetFeedBackReviews/{orgId}", Name = "GetFeedBackReviews")]
        public async Task<IActionResult> GetFeedBackReviews(int orgId)
        {
            if (await _organisation.Find(orgId))
            {
                var reviews = await _reviewNotification.List();
                var list = reviews.Where(a => a.Review.TeamMember.Projects.OrganisationId == orgId).Where(a => a.ReviewKind.Name == "Feedback").ToList();
                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }

        [HttpGet("GetAllFeedBackReviews", Name = "GetAllFeedBackReviews")]
        public async Task<IActionResult> GetAllFeedBackReviews()
        {

            var reviews = await _reviewNotification.List();
            var list = reviews.Where(a => a.ReviewKind.Name == "Feedback").ToList();
            return Ok(list);

        }

        [HttpGet("GetQueryReviews/{orgId}", Name = "GetQueryReviews")]
        public async Task<IActionResult> GetQueryReviews(int orgId)
        {
            if (await _organisation.Find(orgId))
            {
                var reviews = await _reviewNotification.List();
                var list = reviews.Where(a => a.Review.TeamMember.Projects.OrganisationId == orgId).Where(a => a.ReviewKind.Name == "Query").ToList();
                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }


        [HttpGet("GetAll", Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewNotification.List();
            return Ok(reviews);
        }

        [HttpGet("GetOrganisationReview/{id}", Name = "GetAllOrganisation")]
        public async Task<IActionResult> GetAllOrganisation(int id)
        {
            if (!await _organisation.Find(id)) return NotFound("Organisation Not Found");
            var reviews = await _reviewNotification.OrganisationReviewList(id);
            // return Ok(reviews);

            return Ok(reviews.OrderByDescending(a=>a.DateAdded).Take(20));
        }


        [HttpGet("GetOrganisationReviewCount/{id}", Name = "GetAllOrganisationCount")]
        public async Task<IActionResult> GetAllOrganisationCount(int id)
        {
            if (!await _organisation.Find(id)) return NotFound("Organisation Not Found");
            var reviews = await _reviewNotification.OrganisationReviewList(id);
            // return Ok(reviews);

            return Ok(reviews.Count());
        }

        [HttpGet("GetAll/{typeName}/{actionName}")]
        public async Task<IActionResult> GetAll(string typeName, string actionName)
        {

            var reviews = await _reviewNotification.List();
            var list = reviews.Where(a => a.ReviewKind.Name == typeName && a.ReviewAction.Name == actionName).ToList();
            //  return Ok( list);
            return Ok(list);

        }

        [HttpGet("GetAllQueryReviews", Name = "GetAllQueryReviews")]
        public async Task<IActionResult> GetAllQueryReviews()
        {

            var reviews = await _reviewNotification.List();
            var list = reviews.Where(a => a.ReviewKind.Name == "Query").ToList();
            return Ok(list);
        }

        [HttpGet("GetOrganisationQuery/{orgId}" )]
        public async Task<IActionResult> GetAllQueryReviews(int orgId)
        {

            var reviews = await _reviewNotification.GetOrganisationQueries(orgId);
            
            return Ok(reviews);
        }

        [HttpGet("GetAllReviewByAction/{actionName}")]
        public async Task<IActionResult> GetAllQueryReviewsByActions(string actionName)
        {

            var reviews = await _reviewNotification.List();
            var list = reviews.Where(a => a.ReviewAction.Name == actionName).ToList();
            return Ok(list);
        }



        [HttpGet("GetOrgReviewByAction/{orgId}/{actionName}")]
        public async Task<IActionResult> GetOrgQueryReviewsByActions(int orgId, string actionName)
        {
            if (await _organisation.Find(orgId))
            {
                var reviews = await _reviewNotification.OrganisationReviewList(orgId);
                var list = reviews.Where(a => a.Review.TeamMember.Projects.Organisation.Id == orgId).Where(a => a.ReviewAction.Name == actionName).ToList();
                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }
        }

        [HttpGet("GetOrgReminders/{orgId}/{isToday}")]
        public async Task<IActionResult> GetOrgReminders(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {
                var remider = Helpers.ReviewActions.SET_REMINDER.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);
                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList(); ;
                }

                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }

        }

        [HttpGet("GetOrgRemindersCount/{orgId}/{isToday}")]
        public async Task<IActionResult> GetOrgRemindersCount(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {
                var remider = Helpers.ReviewActions.SET_REMINDER.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);
                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList(); ;
                }

                return Ok(list.Count());
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }


        [HttpGet("GetDuePayments/{orgId}/{isToday}")]
        public async Task<IActionResult> GetDuePayments(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {

                var remider = Helpers.ReviewActions.ADD_PAYMENT.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);

                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList(); ;
                }
                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }

        [HttpGet("GetDuePaymentsCount/{orgId}/{isToday}")]
        public async Task<IActionResult> GetDuePaymentsCount(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {

                var remider = Helpers.ReviewActions.ADD_PAYMENT.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);

                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate == DateTime.Now)
                        .ToList(); ;
                }
                return Ok(list.Count());
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }


        [HttpGet("GetReschedule/{orgId}/{isToday}")]
        public async Task<IActionResult> GetReschedule(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {
                var remider = Helpers.ReviewActions.RESCHEDULE.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);

                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList(); ;
                }
                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }

        [HttpGet("GetRescheduleCount/{orgId}/{isToday}")]
        public async Task<IActionResult> GetRescheduleCount(int orgId, bool isToday)
        {
            if (await _organisation.Find(orgId))
            {
                var remider = Helpers.ReviewActions.RESCHEDULE.ToString().Replace('_', ' ');
                var list = await _reviewNotification.OrganisationReviewList(orgId);

                list = list.Where(a => a.ReviewAction.Name == remider).ToList();
                if (isToday)
                {
                    list = list
                        .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .ToList(); ;
                }
                return Ok(list.Count());
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }


        [HttpGet("GetQueryForUpdate/{orgId}")]
        public async Task<IActionResult> GetQueryForUpdate(int orgId)
        {
            if (await _organisation.Find(orgId))
            {
                var remider = Helpers.ReviewKinds.Query.ToString().Replace('_', ' ');
                var list = await _reviewNotification.List();

                list = list
                   .Where(a => a.Review.TeamMember.Projects.Organisation.Id == orgId)
                   .Where(a => a.ReviewKind.Name == remider).ToList();

                return Ok(list);
            }
            else
            {
                return NotFound("Organisation Not Found");
            }


        }

        [HttpGet("GetProjectReview/{id}")]
        public async Task<IActionResult> GetUnreadProjectReview(int id)
        {

            var list = await _reviewNotification.ProjectReviewList(id);
           
            return Ok(list);
        }

        [HttpGet("GetProjectRescheduleQuery/{projectId}/{reviewActionId}/{status}/{isToday}")]
        public async Task<IActionResult> GetProjectRescheduleQuery(int projectId, int reviewActionId, string status, bool isToday)
        {
            var list = await _reviewNotification.ProjectReviewQueryList(projectId, reviewActionId, status);
            if (isToday)
            {
                list = list
                    .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString());
            }
            return Ok(list.ToList());
        }

        [HttpGet("GetProjectTodayReview/{id}")]
        public async Task<IActionResult> GetProjectTodayReview(int id)
        {

            var list = await _reviewNotification.ProjectReviewList(id);
            list = list
                   .Where(a => a.StartDate.ToShortDateString() == DateTime.Now.ToShortDateString());
            return Ok(list);
        }

        [HttpGet("UpdateStatus/{id}")]
        public async Task<IActionResult> GetReviewAndUpdateStatus(int id)
        {
            var c = await _reviewNotification.UpdateStatus(id, Helpers.ReviewStatus.READ.ToString());

            if (c) {
                if (!await _reviewNotification.Save()) return BadRequest("Something went wrong");
                return Ok("Record Updated");
            }
            
            return BadRequest("Something nots right");
        }

        [HttpGet("GetReply/{id}", Name = "GetReply")]
        public async Task<IActionResult> GetReply(int id)
        {
            var r = await _reviewNotification.GetReply(id);
            if (r == null) return NotFound("Not Found");
            return Ok(r);
        }

        [HttpPost("CreateReply")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyDto createReplyDto)
        {
            if (createReplyDto == null) return NotFound();

            if (ModelState.IsValid)
            {
                
                //var reviewMapped = Mapper.Map<CreateReviewDto>(reviewAndNotificationDto);
                //var notificationMapped = Mapper.Map<CreateReviewNotificationsDto>(reviewAndNotificationDto);

                // var targetReview = Mapper.Map<Reviews>(reviewMapped);
                var targetReply = Mapper.Map<Replies>(createReplyDto);
                targetReply.status = Helpers.ReviewStatus.UNREAD.ToString();
                await _reviewNotification.CreateReply(targetReply);
                //
                if (!await _reviewNotification.Save())
                {
                    return StatusCode(500, "Server Error, Something went wrong with our server");
                }
                var createdReplies = Mapper.Map<Replies>(targetReply);

                return CreatedAtRoute("GetReply", new { id = createReplyDto.Id }, createdReplies);
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
    }
}
