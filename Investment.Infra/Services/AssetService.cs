using AutoMapper;
using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using Investment.Domain.Exceptions;
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
        Task<bool> Sell(AssetCreateDTO asset);
        Task<List<CustomerAssetReadDTO>> GetAssetsByCustomer(int customerId);
        Task<AssetReadDTO> GetAssetById(int id);
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
            await validateAsset(asset);

            // Quantidade de ativo a ser comprada não pode ser maior que a quantidade disponível na corretora
            // Compra de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
            // Compra de Ativo de ativo não pode ser feita no sábado nem domingo

            await validateBalance(asset);
            validateTimeOfCommerce();
               

            // Subtrair do volume do ativo e da conta do cliente
            bool assetBought = await _repository.BuyAsset(asset);

            return assetBought;

        }

        public async Task<bool> Sell(AssetCreateDTO asset)
        {

            // Quantidade de ativo a ser vendida não pode ser maior que a quantidade disponível na carteira
            // Venda de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
            // Venda de Ativo de ativo não pode ser feita no sábado nem domingo
            await validateAsset(asset, isSelling:true);
            await validateWallet(asset);
            validateTimeOfCommerce();
                
                

            // Adicionar da conta do cliente
            bool assetSold = await _repository.SellAsset(asset);

            return assetSold;
        }

        public async Task<List<CustomerAssetReadDTO>> GetAssetsByCustomer(int customerId)
        {
            await validatePropertyExists(customerId: customerId);

            List<UserAsset> assets = await _repository.GetAssetsByCustomer(customerId);
            var response = _mapper.Map<List<CustomerAssetReadDTO>>(assets);
            return response;
        }

        public async Task<AssetReadDTO> GetAssetById(int id)
        {
            await validatePropertyExists(assetId: id);

            var asset = await _repository.GetAssetById(id);
            var response = _mapper.Map<AssetReadDTO>(asset);
            return response;
        }

        private async Task validateAsset(AssetCreateDTO cmd, bool isSelling = false)
        {
            bool isValid = cmd.CodCliente > 0
                && cmd.CodAtivo > 0
                && cmd.QtdeAtivo > 0;

            bool customerNotHaveAsset = !await _repository.VerifyCustomerBoughtAsset(cmd.CodAtivo, cmd.CodCliente);

            if (!isValid) throw new InvalidPropertyException("dados inválidos");

            if (isSelling && customerNotHaveAsset) throw new NotFoundException("Cliente não possui esse ativo na carteira");

        }

        private async Task validatePropertyExists(int customerId = 0, int assetId = 0)
        {
            bool accountNotExists = ! await _accountRepository.VerifyAccount(customerId);
            bool assetNotExists = !await  _repository.VerifyAsset(assetId);

            if (accountNotExists && customerId > 0) throw new NotFoundException("Conta não encontrada");

            if (assetNotExists && assetId > 0) throw new NotFoundException("Ativo não encontrada");

        }
    
        private async Task validateBalance(AssetCreateDTO cmd)
        {
            Asset asset = await _repository.GetAssetById(cmd.CodAtivo);
            Account account = await _accountRepository.GetByCustomerId(cmd.CodCliente);

            if (account.Balance < asset.Price * cmd.QtdeAtivo) throw new InvalidPropertyException("Saldo insuficiente");

            if (asset.Volume < cmd.QtdeAtivo) throw new InvalidPropertyException("Ativo não disponível");
        }

        private async Task validateWallet(AssetCreateDTO cmd)
        {
            var assets = await _repository.GetAssetsByCustomer(cmd.CodCliente);
            assets = assets.Where(ua => ua.AssetId == cmd.CodAtivo && ua.UtcSoldAt == null).ToList();

            decimal assetsQty = assets.Sum(a => a.Quantity);

            if (assetsQty < cmd.QtdeAtivo) 
                throw new InvalidPropertyException("Cliente não possui quantidade de ativos suficiente"); 
        }

        private void validateTimeOfCommerce()
        {
            var timeOpenning = DateTime.Today.AddHours(13);
            var timeClosed = DateTime.Today.AddHours(20).AddMinutes(55);

            bool isValid = DateTime.UtcNow >= timeOpenning 
                && DateTime.UtcNow <= timeClosed 
                && !DateTime.Today.DayOfWeek.Equals(DayOfWeek.Saturday) 
                && !DateTime.Today.DayOfWeek.Equals(DayOfWeek.Sunday);

            if (!isValid) throw new InvalidPropertyException("Mercado fechado");
           
        }
    }
}
