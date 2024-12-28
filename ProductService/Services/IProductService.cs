namespace ProductService.Services
{
    public interface IProductService
    {
        Task<Int32?> GetProductStockAsync(Int32 ProductID);
    }
}
