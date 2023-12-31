using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineOrder.Web.Models.Dto;
using OnlineOrder.Web.Models.Dtos;
using OnlineOrder.Web.Services.IServices;

namespace OnlineOrder.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
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

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(productDto);
                if (response != null && response.IsSuccesful)
                {
                    TempData["success"] = "Product created succesfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return View(productDto);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            ResponseDto? response = await _productService.GetProductAsync(id);
            if (response != null && response.IsSuccesful)
            {
                ProductDto Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(Product);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            var response = await _productService.DeleteProductAsync(productDto.Id);

            if (response != null && response.IsSuccesful)
            {
                TempData["success"] = "Product deleted succesfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }
        public async Task<IActionResult> ProductEdit(int id)
        {
            ResponseDto? response = await _productService.GetProductAsync(id);
            if (response != null && response.IsSuccesful)
            {
                ProductDto Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(Product);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductsAsync(productDto);

                if (response != null && response.IsSuccesful)
                {
                    TempData["success"] = "Product updated succesfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return View(productDto);
        }
    }
}
