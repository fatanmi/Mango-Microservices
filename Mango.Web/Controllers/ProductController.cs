using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            IEnumerable<ProductDto> products = null;
            try
            {
                ResponseDto response = await _productService.GetProducts();
                if (response != null && response.IsSuccess)
                {
                    products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(
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

            return View(products);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
    }
}
