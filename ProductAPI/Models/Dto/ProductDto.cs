﻿using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.Dto
{
    public class ProductDto : CreateProductDto
    {
        [Key]
        public int ProductId { get; set; }

    }
    public class CreateProductDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]

        [DataType(DataType.Text)]
        public string Category { get; set; }
        [Required]

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [Required]

        [Range(1, 100)]
        public int Quantity { get; set; } = 0;
    }

}
