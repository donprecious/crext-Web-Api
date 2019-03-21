using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
namespace CrExtApiCore.Models
{
    public class CreateReplyDto
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public string Message { get; set; }

        public string Repliedby { get; set; }
        public string status { get; set; }

    }
}
