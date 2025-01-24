using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Implementation.Contract;
using ProductAPI.Models.Dto;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly ResponseDto _responseDto;
        public ProductController(IProductRepository productRepository, IMapper mapper, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _responseDto = new ResponseDto();

        }
        [HttpGet]
        public async Task<ResponseDto> GetProducts()
        {
            try
            {
                _responseDto.Result = await _productRepository.GetProducts();
                return _responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> GetProduct(int id)
        {
            try
            {
                _responseDto.Result = await _productRepository.GetProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto> CreateProduct([FromBody] ProductDto product)
        {
            try
            {

                bool isCreated = await _productRepository.CreateProduct(product);
                if (!isCreated)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Failed to create product";
                    return _responseDto;
                }
                _responseDto.Message = "Product created successfully";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
            return _responseDto;
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto> UpdateProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Product not found";
                    return _responseDto;
                }
                ProductDto productDto = _mapper.Map<ProductDto>(product);
                _productRepository.UpdateProduct(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{ProductId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ResponseDto> DeleteProduct(int ProductId)
        {
            try
            {
                var product = await _productRepository.GetProduct(ProductId);
                if (product == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Product not found";
                    return _responseDto;
                }
                bool isDeleted = await _productRepository.DeleteProduct(product);

                _responseDto.Message = "Product deleted successfully";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
            return _responseDto;
        }

    }
}
