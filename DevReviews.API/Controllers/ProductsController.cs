using System.Collections.Generic;
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
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        /// <summary>Get all products</summary>
        /// <returns>List of products</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _productRepository.GetAllAsync();

            List<ProductViewModel> productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModels);
        }

        /// <summary>Get product details by id</summary>
        /// <param name="id">Product id</param>
        /// <returns>Product details</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Product product = await _productRepository.GetDetailsByIdAsync(id);

            if (product == null)
                return NotFound();

            ProductDetailsViewModel productDetailsViewModel = _mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetailsViewModel);
        }

        /// <summary>Product post</summary>
        /// <param name="productDTO">Product model</param>
        /// <returns>Created product</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(ProductPostDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = new Product(productDTO.Title, productDTO.Description, productDTO.Price);

            await _productRepository.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>Product update</summary>
        /// <param name="id">Product id</param>
        /// <param name="productDTO">Product model</param>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, ProductPutDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            product.Update(productDTO.Description, productDTO.Price);

            await _productRepository.UpdateAsync(product);

            return NoContent();
        }
    }
}
