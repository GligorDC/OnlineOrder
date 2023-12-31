using AutoMapper;
using OnlineOrder.Services.ProductApi.Models;
using OnlineOrder.Services.ProductApi.Models.Dto;

namespace OnlineOrder.Services.ProductApi.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });

            return mappingConfig;
        }
    }
}
