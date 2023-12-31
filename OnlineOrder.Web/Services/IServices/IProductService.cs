using OnlineOrder.Web.Models.Dto;
using OnlineOrder.Web.Models.Dtos;

namespace OnlineOrder.Web.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int productId);
    }
}
