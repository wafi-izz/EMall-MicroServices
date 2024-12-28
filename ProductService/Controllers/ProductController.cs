using EMall.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServiceDbContext _Context;
        public ProductController(ProductServiceDbContext Context)
        {
            _Context = Context;
        }

        //Get: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _Context.Product.ToListAsync();
        }

        //Get: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Int32 ID)
        {
            Product? Product = await _Context.Product.FindAsync(ID);
            if(Product == null)
            {
                return NotFound();
            }
            return Product;
        }

        //Post: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product Product)
        {
            _Context.Product.Add(Product);
            await _Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { ID = Product.ID }, Product);
        }

        //Put: api/product/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Int32 ID, Product Product)
        {
            if (ID != Product.ID)
            {
                return BadRequest();
            }
            _Context.Entry(Product).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!_Context.Product.Any<Product>(e => e.ID == ID))
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

        //Delete: api/product/{id}
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteProduct(Int32 ID)
        {
            Product? Product = await _Context.Product.FindAsync(ID);
            if (Product == null) 
            { 
                return NotFound(); 
            }
            _Context.Product.Remove(Product);
            await _Context.SaveChangesAsync();
            return NoContent();
        }

        //Get: api/product/hello
        [HttpGet("hello")]
        public IActionResult GetHelloMessage()
        {
            return Ok("Hello from Service 2!");
        }
    }
}

