using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using Investment.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Repository
{
    public interface IAssetRepository
    {
        Task<Asset> GetAssetById(int id);
        Task<List<UserAsset>> GetAssetsByCustomer(int customerId);
        Task<bool> BuyAsset(AssetCreateDTO cmd);
        Task<bool> SellAsset(AssetCreateDTO cmd);
        Task<bool> VerifyAsset(int id);
        Task<bool> VerifyCustomerBoughtAsset(int assetId, int customerId);
    }

    public class AssetRepository : IAssetRepository
    {

        private readonly DataContext _context;

        public AssetRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Asset> GetAssetById(int id)
        {
            Asset asset = await _context.Assets.FirstAsync(ast => ast.AssetId == id);
            return asset;
        }

        public async Task<List<UserAsset>> GetAssetsByCustomer(int customerId)
        {

            List<UserAsset> assets = new();
            try
            {
                assets = await _context.UserAssets
                    .Include(ua=>ua.Asset)
                    .Where(ua => ua.UserId == customerId)
                    .ToListAsync();
          
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return assets;
        }

        public async Task<bool> BuyAsset(AssetCreateDTO cmd)
        {
            bool bought = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    UserAsset boughtAsset = new();
                    Asset asset = await _context.Assets.FirstAsync(ast => ast.AssetId == cmd.CodAtivo);
                    Account account = await _context.Accounts.Include(a=> a.User).FirstAsync(acc => acc.userId == cmd.CodCliente);
                    asset.Volume -= cmd.QtdeAtivo;
                    account.Balance -= cmd.QtdeAtivo * asset.Price;

                    _context.Accounts.Update(account);
                    _context.SaveChanges();
                    
                    _context.Assets.Update(asset);
                    _context.SaveChanges();

                    boughtAsset.UserId = cmd.CodCliente;
                    boughtAsset.User = account.User;
                    boughtAsset.AssetId = cmd.CodAtivo;
                    boughtAsset.Asset = asset;
                    boughtAsset.Quantity = cmd.QtdeAtivo;
                    boughtAsset.UtcBoughtAt = DateTime.UtcNow;
                    
                    _context.UserAssets.Add(boughtAsset);
                    _context.SaveChanges();
                    await transaction.CommitAsync();
                    bought = true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    transaction.Rollback();
                }
            }
            return bought;
        }

        public async Task<bool> SellAsset(AssetCreateDTO cmd)
        {
            bool sold = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    UserAsset soldAsset = await _context.UserAssets
                        .FirstAsync(ua=> ua.AssetId == cmd.CodAtivo && ua.UserId == cmd.CodCliente);
                    Asset asset = await _context.Assets.FirstAsync(ast => ast.AssetId == cmd.CodAtivo);
                    Account account = await _context.Accounts.Include(a => a.User).FirstAsync(acc => acc.userId == cmd.CodCliente);

                    account.Balance += cmd.QtdeAtivo * asset.Price;
                    _context.Accounts.Update(account);
                    _context.SaveChanges();

                    soldAsset.UtcSoldAt = DateTime.UtcNow;
                    _context.UserAssets.Update(soldAsset);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    sold = true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    transaction.Rollback();
                }

                return sold;

            }
            
        }

        public async Task<bool> VerifyAsset(int id)
        {
            bool exists = await _context.Assets.AnyAsync(acc => acc.AssetId == id);
            return exists;
        }

        public async Task<bool> VerifyCustomerBoughtAsset(int id, int customerId)
        {
            bool exists = await _context.UserAssets.AnyAsync(acc => acc.AssetId == id && acc.UserId == customerId && acc.UtcSoldAt == null);
            return exists;
        }
    }
}
