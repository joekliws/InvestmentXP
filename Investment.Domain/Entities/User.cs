using Investment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }  = string.Empty;
        public string LastName { get; set; }  = string.Empty;
        public string? PreferedName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public RiskTolerance InvestorStyle { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public bool Inactive => RemovedAt.HasValue;

    }
}
