using Softtube_TestCode_MVC.Models;

namespace Softtube_TestCode_MVC.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel.ProductItem>> GetAllProducts();

        public Task<IEnumerable<ProductViewModel.ProductItem>> GetAllSearchedProducts(string searchQuery);
    }
}
