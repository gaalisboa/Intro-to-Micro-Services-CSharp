using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts(string token);
        Task<ProductModel> GetProductById(long id, string token);
        Task<ProductModel> CreateProduct(ProductModel newProduct, string token);
        Task<ProductModel> UpdateProduct(ProductModel newProduct, string token);
        Task<bool> DeleteProduct(long id, string token);
    }
}