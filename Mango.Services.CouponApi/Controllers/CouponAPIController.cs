using AutoMapper;
using OnlineOrder.Services.CouponApi.Data;
using OnlineOrder.Services.CouponApi.Models;
using OnlineOrder.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace OnlineOrder.Services.CouponApi.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : Controller
    {

        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly string CouponAddedMessage = "The coupon was added succesfuly";
        private readonly string CouponDeletedMessage = "The coupon was deleted succesfuly";
        private readonly string CouponUpdatedMessage = "The coupon was updated succesfuly";
        public CouponAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);

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
                var coupons = _db.Coupons.First(coupon => coupon.Id == id);
                _responseDto.Result = _mapper.Map<CouponDto>(coupons);

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
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                var coupons = _db.Coupons.FirstOrDefault(coupon => coupon.Code.ToLower() == code.ToLower());
                _responseDto.Result = _mapper.Map<CouponDto>(coupons);

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
        public ResponseDto Post([FromBody]CouponDto couponDto)
        {
            try
            {
                Coupon coupon =_mapper.Map<Coupon>(couponDto);
                
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
                _responseDto.Message = CouponAddedMessage;

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
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);

                _db.Coupons.Update(coupon);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
                _responseDto.Message = CouponUpdatedMessage;

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
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(coupon => coupon.Id == id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                
                _responseDto.Message = CouponDeletedMessage;

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
