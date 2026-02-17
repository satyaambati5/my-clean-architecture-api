using FluentValidation;
using Microsoft.Extensions.Logging;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Common.Exceptions;
using MyProject.Common.Models;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductDto> _validator;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IUnitOfWork unitOfWork,
            IValidator<ProductDto> validator,
            ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                
                var productDtos = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();

                return Result.Success(productDtos, $"Retrieved {productDtos.Count} products");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return Result.Failure<IEnumerable<ProductDto>>("Failed to retrieve products");
            }
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            
            if (product == null)
            {
                throw new NotFoundException("Product", id);
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return Result.Success(productDto);
        }

        public async Task<Result<ProductDto>> CreateProductAsync(ProductDto productDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(productDto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    throw new ValidationException(errors);
                }

                await _unitOfWork.BeginTransactionAsync();

                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitTransactionAsync();

                productDto.Id = product.Id;

                _logger.LogInformation("Product created successfully with ID: {ProductId}", product.Id);

                return Result.Success(productDto, "Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<Result<ProductDto>> UpdateProductAsync(int id, ProductDto productDto)
        {
            try
            {
                var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    throw new NotFoundException("Product", id);
                }

                await _unitOfWork.BeginTransactionAsync();

                existingProduct.Name = productDto.Name;
                existingProduct.Price = productDto.Price;
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Products.UpdateAsync(existingProduct);
                await _unitOfWork.CommitTransactionAsync();

                productDto.Id = existingProduct.Id;

                return Result.Success(productDto, "Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<Result> DeleteProductAsync(int id)
        {
            try
            {
                var exists = await _unitOfWork.Products.ExistsAsync(id);
                if (!exists)
                {
                    throw new NotFoundException("Product", id);
                }

                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Products.DeleteAsync(id);
                await _unitOfWork.CommitTransactionAsync();

                return Result.Success("Product deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}