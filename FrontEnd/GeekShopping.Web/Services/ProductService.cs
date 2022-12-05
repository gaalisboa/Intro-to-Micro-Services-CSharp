using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string DefaultUrl = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(
                "client", "HttpClient wasn't given for the Service");
        }

        public async Task<ProductModel> CreateProduct(ProductModel newProduct)
        {
            var response = await _client.PostAsJson($"{DefaultUrl}/Create", newProduct);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<bool> DeleteProduct(long id)
        {
            var response = await _client.DeleteAsync($"{DefaultUrl}/delete/{id}");
            if (response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            return await response.ReadContentAs<bool>();
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _client.GetAsync($"{DefaultUrl}/getall");
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetProductById(long id)
        {
            var response = await _client.GetAsync($"{DefaultUrl}/getbyid/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> UpdateProduct(ProductModel newProduct)
        {
            var response = await _client.PutAsJson($"{DefaultUrl}/update", newProduct);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            return await response.ReadContentAs<ProductModel>();
        }
    }
}