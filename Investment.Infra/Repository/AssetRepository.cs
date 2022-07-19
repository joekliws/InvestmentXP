using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Repository
{
    public interface IAssetRepository 
    {
        Task<Asset> GetAssetById(int id);
        Task<List<Asset>> GetAssetsByCustomer(int customerId);
        Task<bool> BuyAsset(AssetCreateDTO cmd);
        Task<bool> SellAsset(AssetCreateDTO cmd);
    }

    public class AssetRepository : IAssetRepository
    {
        public Task<Asset> GetAssetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Asset>> GetAssetsByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BuyAsset(AssetCreateDTO cmd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SellAsset(AssetCreateDTO cmd)
        {
            throw new NotImplementedException();
        }
    }
}
