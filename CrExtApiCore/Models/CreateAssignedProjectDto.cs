using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
namespace CrExtApiCore.Models
{
    public class CreateAssignedProjectDto
    {
        
        public int Id { get; set; }

        public int ProjectId { get; set; }      
      //  public Projects Project { get; set; }

        public string AssignedToUserId { get; set; }
      //  public Users AssignedToUser { get; set; }
        
        public string AssignedByUserId { get; set; }
     //   public Users AssignedbyUser { get; set; }



    }
}
