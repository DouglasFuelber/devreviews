using System;

namespace DevReviews.API.ViewModels
{
    public class ProductReviewViewModel
    {
        public ProductReviewViewModel(int id, string author, int rating, string comment, int productId, DateTime createdAt)
        {
            Id = id;
            Author = author;
            Rating = rating;
            Comment = comment;
            ProductId = productId;
            CreatedAt = createdAt;
        }

        public int Id { get; private set; }
        public string Author { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public int ProductId { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}