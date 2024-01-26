using System.Collections.Generic;
using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CouponAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
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
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponID == id);

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



    }
}
