using api.Data;
using api.Entities;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Authorize()]
[Route("api/[controller]")]
public class ProductsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<IActionResult> ListAllProducts()
    {
        var products = await _context.Products
            .Select(product => new
            {
                product.Id,
                product.ItemNumber,
                product.ProductName
            }).ToListAsync();

        return Ok(new { success = true, data = products });
    }

    [HttpPost()]
    public async Task<IActionResult> AddProduct(ProductPostViewModel model)
    {
        var product = new Product
        {
            Id = Guid.NewGuid().ToString().Replace("-", ""),
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName
        };

        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { success = true, data = product });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
