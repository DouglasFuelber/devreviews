using DevReviews.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/product/{productId}/reviews")]
    public class ProductsReviewsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(int productId)
        {
            if (productId == 1)
                return Ok();

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, int productId)
        {
            if (id == 5 && productId == 1)
                return Ok();

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(int productId, ProductReviewPostDTO review)
        {
            return CreatedAtAction(nameof(GetById), new { id = 5, productId = 1 }, review);
        }
    }
}