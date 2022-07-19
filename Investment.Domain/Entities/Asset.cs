using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.Entities
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime BoughtAt { get; set; }
    }
}
