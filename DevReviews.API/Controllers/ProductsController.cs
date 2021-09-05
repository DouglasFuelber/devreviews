using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.DTOs;
using DevReviews.API.Entities;
using DevReviews.API.Persistence.Repositories.Interfaces;
using DevReviews.API.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _productRepository.GetAllAsync();

            List<ProductViewModel> productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Product product = await _productRepository.GetDetailsByIdAsync(id);

            if (product == null)
                return NotFound();

            ProductDetailsViewModel productDetailsViewModel = _mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductPostDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = new Product(productDTO.Title, productDTO.Description, productDTO.Price);

            await _productRepository.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
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
