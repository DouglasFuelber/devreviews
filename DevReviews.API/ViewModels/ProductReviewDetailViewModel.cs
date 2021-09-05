using System;

namespace DevReviews.API.ViewModels
{
    public class ProductReviewDetailsViewModel
    {
        public int Id { get; private set; }
        public string Author { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}