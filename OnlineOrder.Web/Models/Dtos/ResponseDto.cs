namespace OnlineOrder.Web.Models.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccesful { get; set; } = true;
        public string? Message { get; set; }
    }
}
