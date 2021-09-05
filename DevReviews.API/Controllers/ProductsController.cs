using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevReviews.API.DTOs;
using DevReviews.API.Entities;
using DevReviews.API.Persistence;
using DevReviews.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DevReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsController(DevReviewsDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _dbContext.Products.ToList();

            List<ProductViewModel> productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _dbContext.Products
                                                             .Include(p => p.Reviews)
                                                             .SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            ProductDetailsViewModel productDetailsViewModel = _mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Post(ProductPostDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = new Product(productDTO.Title, productDTO.Description, productDTO.Price);

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProductPutDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Update(productDTO.Description, productDTO.Price);

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
