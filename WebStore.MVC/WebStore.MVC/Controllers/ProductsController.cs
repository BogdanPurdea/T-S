using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Entities;
using Store.Models.ViewModels.Base;
using WebStore.MVC.Models;
using WebStore.MVC.WebServiceAccess.Base;

namespace WebStore.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebApiCalls _webApiCalls;
        public ProductsController(IWebApiCalls webApiCalls, UserManager<ApplicationUser> userManager)
        {
            _webApiCalls = webApiCalls;
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Featured));
        }
        public ActionResult Details(int id)
        {
            return RedirectToAction(
            nameof(CartController.AddToCart),
            nameof(CartController).Replace("Controller", ""),
            new { customerId = ViewBag.CustomerId, productId = id, cameFromProducts = true });
        }
        internal async Task<IActionResult> GetListOfProducts(int id = -1, bool featured = false, string searchString = "")
        {
            IList<ProductAndCategoryBase> prods;
            if (featured)
            {
                prods = await _webApiCalls.GetFeaturedProductsAsync();
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                prods = await _webApiCalls.SearchAsync(searchString);
            }
            else
            {
                prods = await _webApiCalls.GetProductsForACategoryAsync(id);
            }
            if (prods == null)
            {
                return NotFound();
            }
            return View("ProductList", prods);
        }
        [HttpGet]
        public async Task<IActionResult> Featured()
        {
            ViewBag.IsAdmin = false;
            var userId = ViewBag.UserId;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles[0] == "Admin")
                    ViewBag.IsAdmin = true;
            }
            ViewBag.Title = "Featured Products";
            ViewBag.Header = "Featured Products";
            ViewBag.ShowCategory = true;
            ViewBag.Featured = true;
            return await GetListOfProducts(featured: true);
        }
        [HttpGet]
        public async Task<IActionResult> ProductList(int id)
        {
            ViewBag.IsAdmin = false;
            var userId = ViewBag.UserId;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles[0] == "Admin")
                    ViewBag.IsAdmin = true;
            }
            var cat = await _webApiCalls.GetCategoryAsync(id);
            ViewBag.Title = cat?.CategoryName;
            ViewBag.Header = cat?.CategoryName;
            ViewBag.ShowCategory = false;
            ViewBag.Featured = false;
            return await GetListOfProducts(id);
        }
        [Route("[controller]/[action]")]
        [HttpPost("{searchString}")]
        public async Task<IActionResult> Search(string searchString)
        {
            ViewBag.Title = "Search Results";
            ViewBag.Header = "Search Results";
            ViewBag.ShowCategory = true;
            ViewBag.Featured = false;
            return await GetListOfProducts(searchString: searchString);
        }
        /*[HttpPost("{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id)
        {           
           await _webApiCalls.UpdateProductAsync(newItem);
           var updatedItem = await _webApiCalls.GetCartRecordAsync(customerId, item.ProductId);
           return PartialView(newViewModel);
        }*/
    }
}
