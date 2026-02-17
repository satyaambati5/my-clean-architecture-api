using MyProject.Application.DTOs;
using MyProject.Common.Models;

namespace MyProject.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<Result<ProductDto>> GetProductByIdAsync(int id);
        Task<Result<ProductDto>> CreateProductAsync(ProductDto productDto);
        Task<Result<ProductDto>> UpdateProductAsync(int id, ProductDto productDto);
        Task<Result> DeleteProductAsync(int id);
    }
}