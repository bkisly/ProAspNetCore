namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            /*Product?[] products = Product.GetProducts()*/;
            List<string> output = new();
            await foreach (var length in MyAsyncMethods.GetPageLengthsAsync(output, "microsoft.com", "apress.com", "google.com"))
            {
                output.Add($"Page length: {length}");
            }

            return View(output);
        }
    }
}
