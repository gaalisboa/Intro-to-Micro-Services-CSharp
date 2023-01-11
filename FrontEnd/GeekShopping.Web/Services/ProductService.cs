using System.Net.Http.Headers;
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

        public async Task<IEnumerable<ProductModel>> GetAllProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{DefaultUrl}/getall");
            var products = await response.ReadContentAs<List<ProductModel>>();
            return products;
        }

        public async Task<ProductModel> GetProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{DefaultUrl}/getbyid/{id}");
            var product = await response.ReadContentAs<ProductModel>();
            return product;
        }

        public async Task<ProductModel> CreateProduct(ProductModel newProduct, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{DefaultUrl}/Create", newProduct);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            var product = await response.ReadContentAs<ProductModel>();
            return product;
        }

        public async Task<bool> DeleteProduct(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{DefaultUrl}/delete/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            var deleted = await response.ReadContentAs<bool>();
            return deleted;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel newProduct, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson($"{DefaultUrl}/update", newProduct);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling the API.");
            return newProduct;
        }
    }
}