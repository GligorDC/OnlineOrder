using AutoMapper;
using OnlineOrder.Services.ProductApi.Data;
using OnlineOrder.Services.ProductApi.Models;
using OnlineOrder.Services.ProductApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OnlineOrder.Services.ProductApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly string ProductAddedMessage = "The Product was added succesfuly";
        private readonly string ProductDeletedMessage = "The Product was deleted succesfuly";
        private readonly string ProductUpdatedMessage = "The Product was updated succesfuly";
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> products = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);

                return _responseDto;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var Products = _db.Products.First(Product => Product.Id == id);
                _responseDto.Result = _mapper.Map<ProductDto>(Products);

                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
        }


        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody]ProductDto productDto)
        {
            try
            {
                Product Product =_mapper.Map<Product>(productDto);
                
                _db.Products.Add(Product);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(Product);
                _responseDto.Message = ProductAddedMessage;

                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                Product Product = _mapper.Map<Product>(productDto);

                _db.Products.Update(Product);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(Product);
                _responseDto.Message = ProductUpdatedMessage;

                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product Product = _db.Products.First(Product => Product.Id == id);
                _db.Products.Remove(Product);
                _db.SaveChanges();
                
                _responseDto.Message = ProductDeletedMessage;

                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccesful = false;
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
        }
    }
}
