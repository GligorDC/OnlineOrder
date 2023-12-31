using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineOrder.Web.Models;
using OnlineOrder.Web.Models.Dto;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;
using System.Diagnostics;

namespace OnlineOrder.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;

		public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
			List<ProductDto>? products = new();
			ResponseDto? response = await _productService.GetAllProductsAsync();
			if (response != null && response.IsSuccesful)
			{
				products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(products);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            ProductDto? product = new();
            ResponseDto? response = await _productService.GetProductAsync(id);
            if (response != null && response.IsSuccesful)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
