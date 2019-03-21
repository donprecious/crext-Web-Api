using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Reviews
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(1000)]
        public string Comment { get; set; }

        [ForeignKey("TeamMemberId")]
        public int TeamMemberId { get; set; }
        public TeamMembers TeamMember { get; set; }

       
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customers Customer { get; set; }


        public string status { get; set; }

        public ICollection<Replies> Replies { get; set; }

    }
}
