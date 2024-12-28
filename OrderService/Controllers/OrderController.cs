using EMall.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using ProductService.Data;
using System.Net.Http;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServiceDbContext _Context;
        public OrderController(OrderServiceDbContext Context, IHttpClientFactory HttpClientFactory)
        {
            _Context = Context;
            _HttpClientFactory = HttpClientFactory;
        }

        //Get: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _Context.Order.ToListAsync();
        }

        //Get: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Int32 ID)
        {
            Order? Order = await _Context.Order.FindAsync(ID);
            if(Order == null)
            {
                return NotFound();
            }
            return Order;
        }

        //Post: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order Order)
        {
            _Context.Order.Add(Order);
            await _Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { ID = Order.ID }, Order);
        }

        //Put: api/order/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Int32 ID, Order Order)
        {
            if (ID != Order.ID)
            {
                return BadRequest();
            }
            _Context.Entry(Order).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!_Context.Order.Any<Order>(e => e.ID == ID))
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

        //Delete: api/order/{id}
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteOrder(Int32 ID)
        {
            Order? Order = await _Context.Order.FindAsync(ID);
            if (Order == null) 
            { 
                return NotFound(); 
            }
            _Context.Order.Remove(Order);
            await _Context.SaveChangesAsync();
            return NoContent();
        }

        private readonly IHttpClientFactory _HttpClientFactory;

        [HttpGet("ProductService")]
        public async Task<IActionResult> CallProductService()
        {
            var client = _HttpClientFactory.CreateClient("ProductService");
            try
            {
                var response = await client.GetAsync("api/product/hello");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(new { message = "Response from Product Service", content });
                }

                return StatusCode((int)response.StatusCode, "Error calling Product Service");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Exception: {ex.Message}");
            }
        }
    }
}

