using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrExtApiCore.Models
{
    public class TeamMemberDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public int ProjectId { get; set; }
    }
}
