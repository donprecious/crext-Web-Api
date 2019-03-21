using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrExtApiCore.Models
{
    public class CreateReviewAndNotitficationDto
    {
        public CreateReviewDto review { get; set; }
        public CreateReviewNotificationsDto reviewNotification { get; set; }
    }

    public class CreateNewReviewAndNotitficationDto
    {
        [Required]
        public int TeamMemberId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public string Comment { get; set; }
        public int ReviewKindId { get; set; }

        public int ReviewId { get; set; }

        public int ReviewActionId { get; set; }

        public DateTime StartDate { get; set; }

        //[Required]
        public DateTime EndDate { get; set; }

        public DateTime DateAdded { get; set; }

    }
}
