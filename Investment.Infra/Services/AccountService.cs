using Investment.Domain.Entities;
using Investment.Domain.Helpers;
using Investment.Infra.Context;
using Investment.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Services
{
    public interface IAccountService
    {
        Task<bool> Deposit(Operation operation);
        Task<bool> Withdraw(Operation operation);
    }
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Deposit(Operation operation)
        {
            bool result = false;
            try
            {

                // verfificar se usuario esta logado

                // verificar se o token é valido

                // Quantidade a ser depositada não poderá ser negativa ou igual a zero.
                bool isOpValid = await validateOperation(operation);

                if (isOpValid)
                {
                    var account = await _repository.GetByCustomerId(operation.CodCliente);
                    account.Balance += operation.Valor;
                    result = await _repository.UpdateBalance(account);
                }

            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return result;

        }

        public async Task<bool> Withdraw(Operation operation)
        {
            bool result = false;
            try
            {
                // verfificar se usuario esta logado

                // verificar se o token é valido

                // se o valor for maior que 0 e a diferenca entre o balance e o valor for igual ou maior que 0 retira valor do Balance do usuario

                // Quantidade a ser sacada não poderá ser maior que o saldo da conta; não pode ser negativa e não pode ser igual a zero
                bool isOpValid = await validateOperation(operation);
                Account account = await _repository.GetByCustomerId(operation.CodCliente);
                if (isOpValid && account.Balance >= operation.Valor)
                {
                    account.Balance -= operation.Valor;
                    result = await _repository.UpdateBalance(account);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return result;
        }

        private async Task<bool> validateOperation(Operation operation)
        {
            var account = await _repository.VerifyAccount(operation.CodCliente);
            return operation.Valor > 0 && account;
        }
    }
}
