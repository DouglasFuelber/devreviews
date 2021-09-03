using System.Collections.Generic;
using DevReviews.API.Entities;

namespace DevReviews.API.Repositories
{
    public class DevReviewsDbContext
    {
        public DevReviewsDbContext()
        {
            Products = new List<Product>();
        }

        public List<Product> Products { get; set; }
    }
}