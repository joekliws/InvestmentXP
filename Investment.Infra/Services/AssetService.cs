using AutoMapper;
using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using Investment.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Services
{
    public interface IAssetService 
    {
        Task<bool> Buy(AssetCreateDTO asset);
        //Task<bool> Sell(AssetCreateDTO asset);
        Task<List<CustomerAssetReadDTO>> GetAssetsByCustomer(int customerId);
        AssetReadDTO GetAssetById(int id);
    }
    public class AssetService : IAssetService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public AssetService(IAccountRepository accountRepository, IAssetRepository repository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Buy(AssetCreateDTO asset)
        {
            bool isValid, assetBought = false;
            
            // Quantidade de ativo a ser comprada não pode ser maior que a quantidade disponível na corretora
            // Compra de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
            // Compra de Ativo de ativo não pode ser feita no sábado nem domingo
            isValid = validateAsset(asset) && validateBalance(asset) && validateTimeOfCommerce();

            // Subtrair do volume do ativo e da conta do cliente
            if (isValid) assetBought = await _repository.BuyAsset(asset);

            return assetBought;

        }

        //public async Task<bool> Sell(AssetCreateDTO asset)
        //{
        //    // Quantidade de ativo a ser vendida não pode ser maior que a quantidade disponível na carteira
        //    // Venda de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
        //    // Venda de Ativo de ativo não pode ser feita no sábado nem domingo

        //    // Multiplicar a quantidade pelo preco do ativo
        //    // Subtrair do saldo do cliente
        //}

        public async Task<List<CustomerAssetReadDTO>> GetAssetsByCustomer(int customerId)
        {
            List<UserAsset> assets = await _repository.GetAssetsByCustomer(customerId);
            var response = _mapper.Map<List<CustomerAssetReadDTO>>(assets);
            return response;
        }

        public AssetReadDTO GetAssetById(int id)
        {
            var asset = _repository.GetAssetById(id).Result;
            var response = _mapper.Map<AssetReadDTO>(asset);
            return response;
        }

        private bool validateAsset(AssetCreateDTO cmd)
        {

            bool isValid = cmd.CodCliente > 0 
                && cmd.CodAtivo > 0 
                && cmd.QtdeAtivo > 0 
                && _accountRepository.VerifyAccount(cmd.CodCliente).Result
                && _repository.VerifyAsset(cmd.CodAtivo).Result;

            return isValid;

        }
    
        private bool validateBalance(AssetCreateDTO cmd)
        {
            Asset asset = _repository.GetAssetById(cmd.CodAtivo).Result;
            Account account = _accountRepository.GetByCustomerId(cmd.CodCliente).Result;
            bool hasValue = account.Balance > asset.Price * cmd.QtdeAtivo && asset.Volume > cmd.QtdeAtivo;
            return hasValue;
        }
    
        private bool validateTimeOfCommerce()
        {
            var timeOpenning = DateTime.Today.AddHours(13);
            var timeClosed = DateTime.Today.AddHours(20).AddMinutes(55);

            bool isValid = DateTime.UtcNow >= timeOpenning 
                && DateTime.UtcNow <= timeClosed 
                && !DateTime.Today.DayOfWeek.Equals(DayOfWeek.Saturday) 
                && !DateTime.Today.DayOfWeek.Equals(DayOfWeek.Sunday);

            return isValid;
           
        }
    }
}
