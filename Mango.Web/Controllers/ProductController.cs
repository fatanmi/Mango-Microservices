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
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseDto response = await _productService.CreateProduct(product);
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Success!";
                        return RedirectToAction(nameof(ProductIndex));
                    }
                    else
                    {
                        TempData["error"] = response?.Message ?? "Something went wrong";
                    }
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                ResponseDto response = await _productService.DeleteProduct(productId);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Success!";
                }
                else
                {
                    TempData["error"] = response?.Message ?? "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction(nameof(ProductIndex));
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
    }
}
