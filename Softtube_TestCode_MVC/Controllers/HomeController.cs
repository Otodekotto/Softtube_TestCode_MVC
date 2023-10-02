using Microsoft.AspNetCore.Mvc;
using Softtube_TestCode_MVC.Models;
using Softtube_TestCode_MVC.Services;
using System.Diagnostics;

namespace Softtube_TestCode_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger , IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<ProductViewModel.ProductItem> products = await _productService.GetAllProducts();

            return View(products);

        }
        [HttpGet("SearchProduct")]
        public async Task<IActionResult> SearchProduct(string searchQuery)
        {
            IEnumerable<ProductViewModel.ProductItem> products = await _productService.GetAllSearchedProducts(searchQuery);

            // Return a partial view with the updated product data
            return View("Search", products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}