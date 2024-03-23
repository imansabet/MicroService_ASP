using IdentityModel;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartservice;
        public HomeController(IProductService productService, ICartService cartservice)
        {
            _productService = productService;
            _cartservice = cartservice;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO>? list = new();
            ResponseDTO? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                //    json to .net object  // // final result type // // convert result to json string  //
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }
        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDTO? model = new();
            ResponseDTO? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                //    json to .net object  // // final result type // // convert result to json string  //
                model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDTO productDTO)
        {
            CartDTO cartDTO = new CartDTO()
            {
                CartHeader = new CartHeaderDTO
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value 
                }
            };
            CartDetialsDTO cartDetails = new CartDetialsDTO()
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId,
            };
            List<CartDetialsDTO> cartDetialsDTOs = new () { cartDetails };
            cartDTO.CartDetails = cartDetialsDTOs;


            ResponseDTO? response = await _cartservice.UpsertCartAsync(cartDTO);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Item Has been added to Shopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDTO);
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
