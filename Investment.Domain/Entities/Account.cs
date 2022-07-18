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
        public int AccountId { get; set; }
        
        public string AccountNumber { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int userId { get; set; }
       
        public virtual User User { get; set; } = new User();

        public decimal Balance { get; set; }
        
        public List<Asset>? Assets { get; set; }
    }
}
