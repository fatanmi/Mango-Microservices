using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    public class CouponAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
