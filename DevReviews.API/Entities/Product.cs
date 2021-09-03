using System;
using System.Collections.Generic;

namespace DevReviews.API.Entities
{
    public class Product
    {
        public Product(string title, string description, decimal price)
        {
            Title = title;
            Description = description;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            Reviews = new List<ProductReview>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<ProductReview> Reviews { get; private set; }

        public void AddReview(ProductReview review)
        {
            Reviews.Add(review);
        }

        public void Update(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
    }
}