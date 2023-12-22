using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.GET,
                URL = ApiBase.Coupon + "/api/coupon"
            });
        }
        public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.GET,
                URL = ApiBase.Coupon + $"/api/coupon/GetByCode/{couponCode}"
            });
        }


        public async Task<ResponseDto?> GetCoupoAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.GET,
                URL = ApiBase.Coupon + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.POST,
                URL = ApiBase.Coupon + $"/api/coupon",
                Data = couponDto
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.PUT,
                URL = ApiBase.Coupon + $"/api/coupon",
                Data = couponDto
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.DELETE,
                URL = ApiBase.Coupon + $"/api/coupon/{couponId}",
            });
        }

    }
}
