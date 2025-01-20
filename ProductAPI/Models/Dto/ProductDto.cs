using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.Dto
{
    public class ProductDto
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Category { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [DataType(DataType.Currency)]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }
    }

}
