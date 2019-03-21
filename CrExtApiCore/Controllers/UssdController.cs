using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrExtApiCore.Repositories;
using CrExtApiCore.Models;
using Entities;
using AutoMapper;

namespace CrExtApiCore.Controllers
{


    [Produces("application/json")]
    [Route("api/Ussd")]
    public class UssdController : Controller
    {
        private IOrganisationAsync _organisation;
        private ICustomer _customer;
        private IReviewNotification _reviewNotification;
        private ITeamMember _teamMember;
        private IReview _review;
        public UssdController(IOrganisationAsync organisation, ICustomer customer, ITeamMember teamMember, IReviewNotification reviewNotification, IReview review)
        {
            _organisation = organisation;
            _customer = customer;
            _teamMember = teamMember;
            _reviewNotification = reviewNotification;
            _review = review;
        }
        public async Task<IActionResult> CreateMessage([FromBody]ServerResponse ServerResponse)
        {
            // declare a complex type as input parameter
            HttpResponseMessage rs;
            string response;
            if (ServerResponse.text == null)
            {
                ServerResponse.text = "";
            }
            // loop through the server's text value to determine the next cause of action
            if (ServerResponse.text.Equals("", StringComparison.Ordinal))
            {
                // always include a 'CON' in your first statements
                response = "CON Welcome to Crext";
                response += "Enter organisation Id";

                var team = await _teamMember.GetTeamMemberByPhone(ServerResponse.phoneNumber);
                if(team == null)
                {
                    response = "END You were not Identified, Please continue with the phone number that was registered by the organisation";

                }

                //validate Organisation Id
                var orgId = Convert.ToInt32(ServerResponse.text);
                var hasOrg = await _organisation.Find(orgId);
                if (hasOrg)
                {
                    response += "Enter Customer Phone Number";

                    var findcus = await _customer.GetPhone(ServerResponse.text);
                    if(findcus != null)
                    {
                        response += "Enter Option \n 1 Add Payment \n 2 Report";
                        var opt = ServerResponse.text;
                        if(opt.Equals("1", StringComparison.Ordinal))
                        {
                            response += "Enter Amount";
                            var amount = ServerResponse.text;

                            //Update feed back with comment "Customer Paid Amount on Date"
                            var reviewCreateMapped = new CreateReviewDto
                            {
                                Comment = "Customer Paid " + amount + " on "+ DateTime.UtcNow,
                                CustomerId = findcus.Id.ToString(),
                                TeamMemberId = team.Id,
                                Status = Helpers.ReviewStatus.UNREAD.ToString()

                            };

                            var reviewNotificationCreateMapped = new CreateReviewNotificationsDto
                            {
                                ReviewActionId = 4,
                                ReviewKindId = 1,
                                StartDate = DateTime.UtcNow,
                                EndDate = DateTime.UtcNow,
                                DateAdded = DateTime.UtcNow
                            };

                            var targetReview = Mapper.Map<Reviews>(reviewCreateMapped);

                            await _review.Create(targetReview);
                            //
                            if (!await _review.Save())
                            {
                                return StatusCode(500, "END Server Error, Something went wrong with our server");
                            }
                            var createdReview = Mapper.Map<Reviews>(targetReview);

                            //notificationMapped.ReviewId = createdReview.Id;
                            reviewNotificationCreateMapped.ReviewId = createdReview.Id;
                            // var targetNotification = Mapper.Map<ReviewNotifications>(notificationMapped);
                            var targetNotification = Mapper.Map<ReviewNotifications>(reviewNotificationCreateMapped);



                            await _reviewNotification.Create(targetNotification);

                            if (!await _review.Save())
                            {
                                return StatusCode(500, "END Server Error, Something went wrong with our server");
                            }

                            var createdNotification = Mapper.Map<ReviewNotifications>(targetNotification);
                            response = "End Update Successful";
                            return CreatedAtRoute("GetReview", new { id = createdNotification.Id }, response);

                        }
                        else if (opt.Equals("2", StringComparison.Ordinal))
                        {

                        }
                       
                    }
                    else
                    {
                        //Wrong Phone Number
                    }
                }
                else
                {
                    //Wroung Organisation  Phone number
                }


            }
           
            else if (ServerResponse.text.Equals("1", StringComparison.Ordinal))
            {
                response = "END Your phone number is " + ServerResponse.phoneNumber;
                //the last response starts with an 'END' so that the server understands that it's the final response
            }
            else
            {
                response = "END invalid option";
            }
           // rs = Request.CreateResponse(HttpStatusCode.Created, response);
            var re = CreatedAtRoute("Get", response);
            // append your response to the HttpResponseMessage and set content type to text/plain, exactly what the server expects
          //  rs.Content = new StringContent(response, Encoding.UTF8, "text/plain");
            // finally return your response to the server
            return re;

        }

        public class ServerResponse
        {
            public string text { get; set; }
            public string phoneNumber { get; set; }
            public string sessionId { get; set; }
            public string serviceCode { get; set; }
        }
    }
}