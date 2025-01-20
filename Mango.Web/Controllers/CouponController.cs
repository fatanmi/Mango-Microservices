using Mango.Web.Implementation.IService;
using Mango.Web.Models;
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

        public async Task<IActionResult> CouponIndex()
        {
            IEnumerable<CouponDto> list = null;
            try
            {

                ResponseDto response = await _couponService.GetAllCouponsAsync();
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<IEnumerable<CouponDto>>(
                        Convert.ToString(response.Result)
                    );
                }
                else
                {
                    TempData["error"] = response?.Message ?? "Not Authenticated";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(list);
        }

        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CreateCouponDto createCouponDto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    ResponseDto response = await _couponService.CreateCouponAsync(createCouponDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Created";
                        return RedirectToAction(nameof(CouponIndex));
                    }
                    else
                    {
                        TempData["error"] = response.Message;
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            return View(createCouponDto);
        }

        public async Task<IActionResult> CouponDelete(int CouponID)
        {
            CouponDto model = new();
            try
            {

                ResponseDto response = await _couponService.GetCouponByIdAsync(CouponID);
                if (response != null && response.IsSuccess)
                {
                    model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            try
            {

                ResponseDto response = await _couponService.DeleteCouponAsync(couponDto.CouponID);
                //if (response != null && response.IsSuccess)
                if (response.IsSuccess)
                {
                    TempData["error"] = "Deleted";
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(couponDto);

        }

        [HttpPut]
        public async Task<IActionResult> CouponPut(CreateCouponDto createCouponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _couponService.UpdateCouponAsync(createCouponDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(ModelState);
        }
    }
}
