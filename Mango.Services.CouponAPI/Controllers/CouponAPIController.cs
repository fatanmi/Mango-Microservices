using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Implementation.Contract;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly ICouponUoW _CouponService;
        private readonly ResponseDto _response;
        private readonly ILogger<CouponAPIController> _logger;
        private readonly IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper, ICouponUoW couponUoW)
        {
            _response = new ResponseDto();
            _CouponService = couponUoW;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var objList = await _CouponService.Coupons.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetByID")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Coupon objList = await _CouponService.Coupons.GetAsync(expression: u => u.CouponID == id);

                //_response.Result = objList;
                _response.Result = _mapper.Map<CouponDto>(objList);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}", Name = "GetByCode")]
        public async Task<ResponseDto> Get(string code)
        {
            try
            {
                X.PagedList.IPagedList<Coupon> objList = await _CouponService.Coupons.GetAllAsync(expression: u => u.CouponCode.ToLower() == code);
                if (objList == null)
                {
                    _response.IsSuccess = false;
                }
                //_response.Result = objList;
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CreateCouponDto couponDto)
        {

            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _CouponService.Coupons.Insert(obj);
                _CouponService.Save();

                _response.Result = _mapper.Map<CouponDto>(obj);

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CreateCouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _CouponService.Coupons.UpdateAsync(obj);
                _CouponService.Save();
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                if (id < 0) _response.IsSuccess = false;
                Coupon CouponItem = await _CouponService.Coupons.GetAsync(q => q.CouponID == id);

                if (CouponItem != null && _response.IsSuccess)
                {
                    await _CouponService.Coupons.DeleteAsync(id);
                    _response.IsSuccess = true;
                    _response.Message = "Comment Deleted!";
                    return _response;
                }

                _response.IsSuccess = false;
                _response.Message = "Not found";
                return _response;

                //_response.Result = _mapper.Map<CouponDto>(CouponItem);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = ex.Message;
            }
            return _response;
        }



    }
}
