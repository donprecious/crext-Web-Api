using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class AssignedProjects
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProjectId { get; set; }      
        public Projects Project { get; set; }

        public string AssignedToUserId { get; set; }
        public Users AssignedToUser { get; set; }
        
        public string AssignedByUserId { get; set; }
        public Users AssignedbyUser { get; set; }



    }
}
