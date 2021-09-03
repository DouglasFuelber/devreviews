using System;

namespace DevReviews.API.Entities
{
    public class ProductReview
    {
        public ProductReview(string author, int rating, string comment, int productId)
        {
            Author = author;
            Rating = rating;
            Comment = comment;
            ProductId = productId;
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Author { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public int ProductId { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}