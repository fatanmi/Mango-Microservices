using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Implementation.Contract;
using ProductAPI.Models;
using ProductAPI.Models.Dto;
using System.Collections.Immutable;

namespace ProductAPI.Implementation.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ProductDbContext productDbContext, IMapper mapper)
        {
            _context = productDbContext;
            _mapper = mapper;

        }
        public async Task<bool> CreateProduct(ProductDto productDto)
        {
            try
            {

                Product userProduct = _mapper.Map<Product>(productDto);
                await _context.Products.AddAsync(userProduct);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(q => q.Id == id);
            return product;

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }

        public void UpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }



    }
}
