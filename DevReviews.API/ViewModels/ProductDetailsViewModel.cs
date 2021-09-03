using System;
using System.Collections.Generic;
using DevReviews.API.Entities;

namespace DevReviews.API.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel(int id, string title, string description, decimal price, DateTime createdAt, List<ProductReviewViewModel> reviews)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            Reviews = reviews;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<ProductReviewViewModel> Reviews { get; private set; }
    }
}