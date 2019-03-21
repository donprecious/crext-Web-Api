using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class Customers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

      
        [StringLength(50)]
        public string FirstName{ get; set; }

     
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string OtherName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Sex { get; set; }

        public string AccountName { get; set; }

        public string NUBAN_NUMBER { get; set; }
        public string OutstandingBalance { get; set; }
        public string BalanceIssued { get; set; }
        public string Interest{ get; set; }
        public string Recommendation { get; set; }

        public string CustomData { get; set; }

        public int TeamId { get; set; }
        public Teams Team { get; set; }

        public ICollection<Reviews> Reviews { get; set; }

    }
}
