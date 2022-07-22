global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Investment.Domain.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        public int AccountNumber { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
       
        public virtual User User { get; set; } = new User();

        [Column(TypeName = "decimal(15,2)")]
        public decimal Balance { get; set; } = 0;
        
    }
}
