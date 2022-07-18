using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.Entities
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
    }
}
