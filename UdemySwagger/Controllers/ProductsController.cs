using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemySwagger.Models;

namespace UdemySwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SwaggerDBContext _context;

        public ProductsController(SwaggerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This endpoint Returns all product
        /// </summary>
        /// <remarks>
        /// example:https://localhost:44381/api/products
        /// </remarks>
        /// <returns></returns>

        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        /// <summary>
        /// This endpoint Returns product from Id
        /// </summary>
        /// <returns></returns>
        /// <response code="404">Product is not found</response>
        /// <response code="200">Product is found</response>

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// This endpoint update product
        /// </summary>
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// This endpoint add product
        /// </summary>
        /// <remarks>
        /// example:product json:{"name": "book", "price":10,"category": "dedective"}
        /// </remarks>
        /// <returns></returns>

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Date = DateTime.Now;
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// This endpoint delete product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
