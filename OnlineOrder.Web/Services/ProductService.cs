using OnlineOrder.Web.Models.Dto;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using OnlineOrder.Web.Utility;

namespace OnlineOrder.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.POST,
                URL = ApiBase.Product + $"/api/product",
                Data = productDto
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.DELETE,
                URL = ApiBase.Product + $"/api/product/{productId}",
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.GET,
                URL = ApiBase.Product + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.GET,
                URL = ApiBase.Product + $"/api/product/{id}"
            });
        }

        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiRequestType.PUT,
                URL = ApiBase.Product + $"/api/product",
                Data = productDto
            });
        }
    }
}
