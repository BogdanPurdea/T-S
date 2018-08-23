using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Entities;
using Store.Models.ViewModels;
using WebStore.MVC.WebServiceAccess.Base;

namespace WebStore.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public OrdersController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int customerId)
        {
            ViewBag.Title = "Order History";
            ViewBag.Header = "Order History";
            IList<Order> orders = await _webApiCalls.GetOrdersAsync(customerId);
            if (orders == null) return NotFound();
            return View(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Details(int customerId, int orderId)
        {
            ViewBag.Title = "Order Details";
            ViewBag.Header = "Order Details";
            OrderWithDetailsAndProductInfo orderDetails =
             await _webApiCalls.GetOrderDetailsAsync(customerId, orderId);
            if (orderDetails == null) return NotFound();
            return View(orderDetails);
        }
        [HttpPost("{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int customerId, int id, string billingAddress, string shippingAddress, string phone)
        {
            try
            {
                await _webApiCalls.UpdateOrderAddressAndPhone(customerId, id, billingAddress, shippingAddress, phone);
                OrderWithDetailsAndProductInfo orderDetails =
                    await _webApiCalls.GetOrderDetailsAsync(customerId, id);
                return RedirectToAction(nameof(Details), new { customerId, id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred updating the address. Please reload the page and try again.");
                return RedirectToAction(nameof(Details), new { customerId, id });
            }
        }
    }
}
