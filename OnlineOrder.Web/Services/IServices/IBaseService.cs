using OnlineOrder.Web.Models.Dtos;

namespace OnlineOrder.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
