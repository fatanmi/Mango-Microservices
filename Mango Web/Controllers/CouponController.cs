using Mango.Web.Models;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult>CouponIndex()
        {
            List<CouponDto> list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult>CouponCreate()
        {

            return View();
            //List<CouponDto> list = new();
            //CreateCouponDto createCoupons = new CreateCouponDto();

            //ResponseDto? response = await _couponService.CreateCouponAsync(createCoupons);
            //if(response != null && response.IsSuccess)
            //{
            //    list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            //}
            //return View(list);
        }
        [HttpPost]
        public async Task<IActionResult>CouponCreate( CreateCouponDto createCouponDto)
        {
            if (ModelState.IsValid)
            {

                ResponseDto? response = await _couponService.CreateCouponAsync(createCouponDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(createCouponDto);
        }
        //[HttpDelete]
        public async Task<IActionResult>CouponDelete( int couponId)
        {
            if (ModelState.IsValid && couponId > 0)
            {
                ResponseDto? response = await _couponService.DeleteCouponAsync(couponId);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View();
        }
        [HttpPut]
        public async Task<IActionResult>CouponPut(CreateCouponDto createCouponDto)
        {
            if (ModelState.IsValid)
            {

                ResponseDto? response = await _couponService.UpdateCouponAsync(createCouponDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(ModelState);
        }
    }
}
