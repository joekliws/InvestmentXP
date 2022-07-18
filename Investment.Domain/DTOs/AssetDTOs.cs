using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Domain.DTOs
{
    public class AssetCreateDTO
    {
        public int codCliente { get; set; }
        public int codAtivo { get; set; }
        public int qtdeAtivo { get; set; }
    }

    public class AssetReadDTO
    {
        public int codAtivo { get; set; }
        public int qtdeAtivo { get; set; }
        public decimal Valor { get; set; }
    }

    public class CustomerAssetReadDTO : AssetReadDTO
    {
        public int codCliente { get; set; }
    }
}
