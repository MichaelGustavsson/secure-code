using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult> ListAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(new { success = true, data = products });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return NotFound();
            
            return Ok(new { success = true, data = product });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return NotFound();

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
