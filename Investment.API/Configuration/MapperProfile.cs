using Investment.Domain.DTOs;
using Investment.Domain.Entities;

namespace Investment.API.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Asset, AssetReadDTO>().
                ForMember(dest => dest.CodAtivo, opt => opt.MapFrom(src => src.AssetId))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.QtdeAtivo, opt => opt.MapFrom(src => src.Volume));

            CreateMap<UserAsset, CustomerAssetReadDTO>()
                .ForPath(dest => dest.CodAtivo, opt => opt.MapFrom(src => src.AssetId))
                .ForPath(dest => dest.CodCliente, opt => opt.MapFrom(src => src.UserId))
                .ForPath(dest => dest.QtdeAtivo, opt => opt.MapFrom(src => src.Quantity))
                .ForPath(dest => dest.Valor, opt => opt.MapFrom(src => src.Asset.Price * src.Quantity));
        }
    }
}
