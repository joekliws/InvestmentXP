using AutoMapper;
using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using Investment.Domain.Exceptions;
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
        Task<Operation> GetBalance(int custmerId);
        Task<AccountReadDTO> CreateAccount(AccountCreateDTO cmd);
    }
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _mapper = mapper;
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
               await validateOperation(operation);
                    var account = await _repository.GetByCustomerId(operation.CodCliente);
                    account.Balance += operation.Valor;
                    result = await _repository.UpdateBalance(account);
               
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return result;

        }

        public async Task<Operation> GetBalance(int custmerId)
        {
            Operation operation = new();
            var validUser = await _repository.VerifyAccount(custmerId);
            if (validUser)
                operation = await _repository.GetBalance(custmerId);
            return operation;
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
                await validateOperation(operation);
                Account account = await _repository.GetByCustomerId(operation.CodCliente);
                validateBalance(operation, account);

                account.Balance -= operation.Valor;
                result = await _repository.UpdateBalance(account);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return result;
        }

        private async Task validateOperation(Operation operation, bool isWithdraw = false)
        {
            bool accountExists = await _repository.VerifyAccount(operation.CodCliente);
            


            if (operation.Valor <= 0 || accountExists)
                throw new InvalidPropertyException("dados inválidos");
            
        }

        private void validateBalance(Operation operation, Account account)
        {

            if (operation.Valor > account.Balance)
                throw new InvalidPropertyException("Valor a ser retirado não pode ser maior do que da carteira");
            
        }

        public async Task<AccountReadDTO> CreateAccount(AccountCreateDTO cmd)
        {
            Account newAccount = await _repository.CreateAccount(cmd);
            if (newAccount == null) throw new InvalidPropertyException("Houve algum problema ao criar conta");
            AccountReadDTO response = _mapper.Map<AccountReadDTO>(newAccount);
            return response;

        }
    }
}
