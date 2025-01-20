using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Wireless Mouse",
                Category = "Electronics",
                Description = "A comfortable and ergonomic wireless mouse.",
                ImageUrl = "https://example.com/images/wireless-mouse.jpg",
                Price = 25.99m
            });
            base.OnModelCreating(modelBuilder);
        }

        public static IEnumerable<Product> GetSampleProducts()
        {
            return new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "Wireless Mouse",
            Category = "Electronics",
            Description = "A comfortable and ergonomic wireless mouse.",
            ImageUrl = "https://example.com/images/wireless-mouse.jpg",
            Price = 25.99m
        },
        new Product
        {
            Id = 2,
            Name = "Bluetooth Headphones",
            Category = "Electronics",
            Description = "Noise-cancelling Bluetooth headphones with long battery life.",
            ImageUrl = "https://example.com/images/bluetooth-headphones.jpg",
            Price = 79.99m
        },
        new Product
        {
                Id = 3,
            Name = "Yoga Mat",
            Category = "Fitness",
            Description = "Eco-friendly yoga mat with non-slip surface.",
            ImageUrl = "https://example.com/images/yoga-mat.jpg",
            Price = 19.99m
        },
        new Product
        {
            Id = 4,
            Name = "Stainless Steel Water Bottle",
            Category = "Kitchenware",
            Description = "Insulated water bottle to keep drinks cold or hot.",
            ImageUrl = "https://example.com/images/water-bottle.jpg",
            Price = 14.99m
        },
        new Product
        {
            Id = 5,
            Name = "Running Shoes",
            Category = "Footwear",
            Description = "Lightweight and durable running shoes.",
            ImageUrl = "https://example.com/images/running-shoes.jpg",
            Price = 49.99m
        },
        new Product
        {
            Id = 6,
            Name = "LED Desk Lamp",
            Category = "Home & Office",
            Description = "Adjustable LED desk lamp with multiple brightness levels.",
            ImageUrl = "https://example.com/images/led-desk-lamp.jpg",
            Price = 29.99m
        },
        new Product
        {
            Id = 7,
            Name = "Gaming Keyboard",
            Category = "Electronics",
            Description = "Mechanical keyboard with RGB lighting.",
            ImageUrl = "https://example.com/images/gaming-keyboard.jpg",
            Price = 59.99m
        },
        new Product
        {
            Id = 8,
            Name = "Coffee Maker",
            Category = "Appliances",
            Description = "Programmable coffee maker with a timer and reusable filter.",
            ImageUrl = "https://example.com/images/coffee-maker.jpg",
            Price = 89.99m
        },
        new Product
        {
            Id = 9,
            Name = "Fitness Tracker",
            Category = "Fitness",
            Description = "Water-resistant fitness tracker with heart rate monitoring.",
            ImageUrl = "https://example.com/images/fitness-tracker.jpg",
            Price = 99.99m
        },
        new Product
        {
            Id = 10,
            Name = "Backpack",
            Category = "Accessories",
            Description = "Spacious and durable backpack with multiple compartments.",
            ImageUrl = "https://example.com/images/backpack.jpg",
            Price = 39.99m
        }
    };
        }

    }
}
