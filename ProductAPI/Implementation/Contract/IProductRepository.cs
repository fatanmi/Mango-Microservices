using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace ProductAPI.Implementation.Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<bool> CreateProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(Product product);
    }
}
