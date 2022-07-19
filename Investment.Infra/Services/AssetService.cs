using Investment.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Services
{
    public interface IAssetService 
    {
        void Buy(AssetCreateDTO asset);
        void Sell(AssetCreateDTO asset);
        Task<IEnumerable<CustomerAssetReadDTO>> GetAssetsByCustomer();
        AssetReadDTO GetAssetById(int id);
    }
    public class AssetService : IAssetService
    {
        public void Buy(AssetCreateDTO asset)
        {
            // Quantidade de ativo a ser comprada não pode ser maior que a quantidade disponível na corretora
            // Compra de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
            // Compra de Ativo de ativo não pode ser feita no sábado nem domingo
        }

        public void Sell(AssetCreateDTO asset)
        {
            // Quantidade de ativo a ser vendida não pode ser maior que a quantidade disponível na carteira
            // Venda de Ativo de ativo não pode ser feita fora dos horarios 1:00pm-8:55pm(UTC) 10:00 as 17:55(UTC-3)
            // Venda de Ativo de ativo não pode ser feita no sábado nem domingo
        }

        public Task<IEnumerable<CustomerAssetReadDTO>> GetAssetsByCustomer()
        {
            throw new NotImplementedException();
        }

        public AssetReadDTO GetAssetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
