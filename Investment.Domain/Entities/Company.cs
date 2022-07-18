using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "varchar(7)")]
        public string Code { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Volume { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal Price { get; set; }
    }
}
