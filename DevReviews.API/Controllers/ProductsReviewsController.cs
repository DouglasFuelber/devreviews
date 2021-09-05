using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevReviews.API.DTOs;
using DevReviews.API.Entities;
using DevReviews.API.Persistence;
using DevReviews.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/product/{productId}/reviews")]
    public class ProductsReviewsController : ControllerBase
    {
        private readonly DevReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsReviewsController(DevReviewsDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(int productId)
        {
            List<ProductReview> productReviews = _dbContext.ProductReviews.Where(r => r.ProductId == productId).ToList();

            List<ProductReviewViewModel> productReviewsViewModels = _mapper.Map<List<ProductReviewViewModel>>(productReviews);

            return Ok(productReviewsViewModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, int productId)
        {
            ProductReview productReview = _dbContext.ProductReviews.SingleOrDefault(p => p.Id == id && p.ProductId == productId);

            if (productReview == null)
                return NotFound();

            ProductReviewDetailsViewModel productReviewDetailsViewModel = _mapper.Map<ProductReviewDetailsViewModel>(productReview);

            return Ok(productReviewDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Post(int productId, ProductReviewPostDTO review)
        {
            ProductReview productReview = new ProductReview(review.Author, review.Rating, review.Comment, productId);

            _dbContext.ProductReviews.Add(productReview);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId }, review);
        }
    }
}