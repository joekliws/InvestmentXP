using Investment.Domain.Entities;
using Investment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.DTOs
{
    public class AccountReadDTO
    {
        public int AccountNumber { get; set; }

        public UserReadDTO User { get; set; } = new UserReadDTO();

        public decimal Balance { get; set; }
    }

    public class UserReadDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? PreferedName { get; set; }

        public RiskTolerance InvestorStyle { get; set; }

        public string Cpf { get; set; } = string.Empty;
    }

    public class AccountCreateDTO : UserReadDTO
    {
        public string Password { get; set; } = string.Empty;
    }

}
