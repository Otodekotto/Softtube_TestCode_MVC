﻿using Softtube_TestCode_MVC.Models; // Import your model namespace
using System.Net.Http;
using Newtonsoft.Json; // Import Newtonsoft.Json for JSON deserialization
using static Softtube_TestCode_MVC.Models.ProductViewModel;

namespace Softtube_TestCode_MVC.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://cdn.softube.com/api/v1/");
        }

        public async Task<IEnumerable<ProductItem>> GetAllProducts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("products?pageSize=500");

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a ProductViewModel object
                var result = JsonConvert.DeserializeObject<ProductViewModel>(apiResponse);

                // Extract the required properties from each item
                IEnumerable<ProductItem> products = result.Result.Select(item => new ProductItem
                {
                    Name = item.Name,
                    Images = new Images { W240 = item.Images?.W240 }
                });

                return products;
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
        }

        public async Task<IEnumerable<ProductItem>> GetAllSearchedProducts(string searchQuery)
        {
            // Modify the API endpoint to include the search query parameter
            HttpResponseMessage response;

            if (searchQuery != null)
            {
                response = await _httpClient.GetAsync($"products?search={searchQuery}&pageSize=500");
            }
            else
            {
                response = await _httpClient.GetAsync("products?pageSize=500");
            }

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a ProductViewModel object
                var result = JsonConvert.DeserializeObject<ProductViewModel>(apiResponse);

                // Extract the required properties from each item and get the searched product
                IEnumerable<ProductItem> filteredProducts;

                if (searchQuery != null)
                {
                    filteredProducts = result.Result.Where(item => item.Name.ToLower().Contains(searchQuery.ToLower()));
                }
                else
                {
                    // If searchQuery is null, return all products without filtering
                    IEnumerable<ProductItem> products = result.Result.Select(item => new ProductItem
                    {
                        Name = item.Name,
                        Images = new Images { W240 = item.Images?.W240 }
                    });
                    return products;
                }

                return filteredProducts;
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
        }

    }
}
