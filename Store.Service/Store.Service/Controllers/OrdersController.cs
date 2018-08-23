using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;

namespace Store.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class OrdersController : Controller
    {
        private IOrderRepo Repo { get; set; }
        public OrdersController(IOrderRepo repo)
        {
            Repo = repo;
        }
        public IActionResult GetOrderHistory(int customerId)
        {
            var orderWithTotals = Repo.GetOrderHistory(customerId);
            return orderWithTotals == null ? (IActionResult)NotFound()
            : new ObjectResult(orderWithTotals);
        }
        [HttpGet("{orderId}", Name = "GetOrderDetails")]
        public IActionResult GetOrderForCustomer(int customerId, int orderId)
        {
            var orderWithDetails = Repo.GetOneWithDetails(customerId, orderId);
            return orderWithDetails == null ? (IActionResult)NotFound()
            : new ObjectResult(orderWithDetails);
        }
        [HttpPut("{orderId}")] //Required even if method name starts with Put
        public IActionResult Update([FromBody] Order order)
        {
            if (order == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Update(order);
            return NoContent();
        }
    }
}