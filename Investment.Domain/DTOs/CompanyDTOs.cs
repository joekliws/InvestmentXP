using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.DTOs
{
    public class CompanyReadDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public decimal Volume { get; set; }

        public decimal Price { get; set; }
    }
}
