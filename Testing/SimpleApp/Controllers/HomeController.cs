using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models;

namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        public IProductsDataSource ProductsDataSource = new ProductsDataSource();

        public IActionResult Index()
        {
            return View(ProductsDataSource.Products);
        }
    }
}
