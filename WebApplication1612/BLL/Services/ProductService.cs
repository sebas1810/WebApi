using BLL.Repositories;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.Query().ToListAsync();
        }
        public async Task CreateAsync(Product product)
        {
            await _productRepository.InsertAsync(product);
        }

        public async Task DeleteAsync(int? input = 0)
        {
            if (input > 0)
            {
                var product = await _productRepository.GetAsync(input.Value);
                await _productRepository.DeleteAsync(product);
            }
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> Exists(int id)
        {
            return await _productRepository.Exists(id);
        }
    }
}
