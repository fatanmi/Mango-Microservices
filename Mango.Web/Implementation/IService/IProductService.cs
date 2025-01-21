using Mango.Web.Models;

namespace Mango.Web.Implementation.IService
{
    public interface IProductService
    {
        Task<ResponseDto> GetProducts();
        Task<ResponseDto> GetProduct(int id);
        Task<ResponseDto> CreateProduct(CreateProductDto product);
        Task<ResponseDto> UpdateProduct(ProductDto productDto);
        Task<ResponseDto> DeleteProduct(int id);
    }
}
