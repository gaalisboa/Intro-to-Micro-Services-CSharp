using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.src.Models;

namespace GeekShopping.Web.src.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(long id);
        Task<ProductModel> CreateProduct(ProductModel newProduct);
        Task<ProductModel> UpdateProduct(ProductModel newProduct);
        Task<bool> DeleteProduct(long id);
    }
}