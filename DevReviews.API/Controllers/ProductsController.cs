using System.Collections.Generic;
using System.Linq;
using DevReviews.API.DTOs;
using DevReviews.API.Entities;
using DevReviews.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DevReviewsDbContext _dbContext;

        public ProductsController(DevReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _dbContext.Products.ToList();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(ProductPostDTO productDTO)
        {
            if (productDTO.Description.Length > 50)
                return BadRequest();

            Product product = new Product(productDTO.Title, productDTO.Description, productDTO.Price);

            _dbContext.Products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = 0 }, product);
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

            return NoContent();
        }
    }
}
