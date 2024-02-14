using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public CouponAPIController(AppDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }
        [HttpGet]
        public ResponseDTO Get() 
        {
            try 
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
            }
            catch (Exception e) 
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)

        {
            try
            {
                Coupon obj = _db.Coupons.First(u=> u.CouponId == id);

                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception e)   
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;

            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj == null) 
                {
                    _response.IsSuccess =   false;
                }

                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;

            }
            return _response;
        }
        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO couponDTO) 
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;

            }
            return _response;
        }

    }
}
