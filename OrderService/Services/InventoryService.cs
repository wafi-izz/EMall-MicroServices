using OrderService.Data;
using OrderService.Models;
using Microsoft.Extensions.Http;
using ProductService.Services;
using System.Collections.Generic;

namespace OrderService.Services
{
    public class InventoryService
    {
        private readonly IProductService _ProductService;

        public InventoryService(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        public async Task<bool> CheckInventoryAsync(Order Order)
        {
            foreach (OrderItem OrderItem in Order.OrderItem)
            {
                Int32? ProductStock = await _ProductService.GetProductStockAsync(OrderItem.ProductID);
                if (ProductStock == null || ProductStock < OrderItem.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
