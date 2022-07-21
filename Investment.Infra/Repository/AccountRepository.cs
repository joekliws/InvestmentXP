using Investment.Domain.Entities;
using Investment.Domain.Helpers;
using Investment.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Repository
{
    public interface IAccountRepository 
    {
        Task<Account> GetByCustomerId(int userId);
        Task<bool> UpdateBalance(Account account);
        Task<bool> VerifyAccount(int userId);
        Task<Operation> GetBalance(int customerId);
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Operation> GetBalance(int customerId)
        {
            Operation operation = new();
            Account account = await GetByCustomerId(customerId);
            operation.CodCliente = account.userId;
            operation.Valor = account.Balance;

            return operation;
        }

        public async Task<Account> GetByCustomerId(int userId)
        {
            Account account =  await _context.Accounts.FirstAsync(acc => acc.userId == userId);
            return account;
        }

        public async Task<bool> UpdateBalance(Account account)
        {
            _context.Update(account);
            int result = await _context.SaveChangesAsync();
            return result > 0;

        }

        public async Task<bool> VerifyAccount(int userId)
        {
            bool exists = await _context.Accounts.AnyAsync(acc => acc.userId == userId);
            return exists;
        }
    }
}
