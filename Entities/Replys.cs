using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class Replies
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ReviewId { get; set; }
        
        [ForeignKey("ReviewId")]
        public Reviews Review { get; set; }

        public string Message { get; set; }

        public string Repliedby { get; set; }

        [ForeignKey("Repliedby")]
        public Users Users { get; set; }
        
        public string status { get; set; }     

    }
}
