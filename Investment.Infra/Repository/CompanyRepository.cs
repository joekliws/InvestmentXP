using Investment.Domain.DTOs;
using Investment.Domain.Entities;
using Investment.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Repository
{
    public interface ICompanyRepository 
    {
        Company getCompanyByAsset(Asset asset);
    }

    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;      
        }
        public  Company getCompanyByAsset(Asset asset)
        {
            return _context.Companies.First(c => c.CompanyId == asset.CompanyId);
        }
    }
}
