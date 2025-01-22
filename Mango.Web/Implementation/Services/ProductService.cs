using Mango.Web.Implementation.IService;
using Mango.Web.Models;
using Mango.Web.Utilities;

namespace Mango.Web.Implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService<ResponseDto> _baseService;

        public ProductService(IBaseService<ResponseDto> baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> CreateProduct(CreateProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/Product",
                Data = product
            });
        }

        public async Task<ResponseDto> DeleteProduct(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> GetProduct(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> GetProducts()
        {

            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/Product"
            });
        }

        public async Task<ResponseDto> UpdateProduct(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductAPIBase + "/api/Product" + product.ProductId,
                Data = product
            });
        }
    }
}
