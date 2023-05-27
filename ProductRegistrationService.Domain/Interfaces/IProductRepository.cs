using ProductRegistrationService.Domain.Entities;

namespace ProductRegistrationService.Domain.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetProductCategoryAsync(int? id);
        Task<Product> CreateAsync(Category category);
        Task<Product> UpdateAsync(Category category);
        Task<Product> RemoveAsync(Category category);
    }
}