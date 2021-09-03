using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.ViewModels;

namespace DevReviews.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailsViewModel>();

            CreateMap<ProductReview, ProductReviewViewModel>();
        }
    }
}