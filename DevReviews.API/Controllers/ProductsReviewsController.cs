using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.DTOs;
using DevReviews.API.Entities;
using DevReviews.API.Persistence.Repositories.Interfaces;
using DevReviews.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/product/{productId}/reviews")]
    public class ProductsReviewsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsReviewsController(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        /// <summary>Get product review by id</summary>
        /// <param name="id">Review id</param>
        /// <param name="productId">Product id</param>
        /// <returns>Product review details</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Product review not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, int productId)
        {
            ProductReview productReview = await _productRepository.GetReviewByIdAsync(id);

            if (productReview == null)
                return NotFound();

            ProductReviewDetailsViewModel productReviewDetailsViewModel = _mapper.Map<ProductReviewDetailsViewModel>(productReview);

            return Ok(productReviewDetailsViewModel);
        }

        /// <summary>Post product review</summary>
        /// <param name="productId">Product id</param>
        /// <param name="review">Review object</param>
        /// <returns>New product review</returns>
        /// <response code="201">Created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(int productId, ProductReviewPostDTO review)
        {
            ProductReview productReview = new ProductReview(review.Author, review.Rating, review.Comment, productId);

            await _productRepository.AddReviewAsync(productReview);

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId }, review);
        }
    }
}