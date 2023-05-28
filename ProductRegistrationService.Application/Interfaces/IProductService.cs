using ProductRegistrationService.Application.DTOs;

namespace ProductRegistrationService.Application.interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetCategories();
        Task<ProductDTO> GetById(int? id);
        Task Add(ProductDTO productDto);
        Task Update(ProductDTO productDto);
        Task Remove(int? id);
    }    
}