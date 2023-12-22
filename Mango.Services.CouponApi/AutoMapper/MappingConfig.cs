using AutoMapper;
using OnlineOrder.Services.CouponApi.Models;
using OnlineOrder.Services.CouponApi.Models.Dto;

namespace OnlineOrder.Services.CouponApi.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
