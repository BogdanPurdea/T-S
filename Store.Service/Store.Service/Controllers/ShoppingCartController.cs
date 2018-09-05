using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;
using Store.Models.ViewModels;

namespace Store.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepo Repo { get; set; }
        public ShoppingCartController(IShoppingCartRepo repo)
        {
            Repo = repo;
        }

        [HttpGet("{productId}")]
        public CartRecordWithProductInfo GetShoppingCartRecord(int customerId, int productId)
            => Repo.GetShoppingCartRecord(customerId, productId);

        [HttpGet(Name = "GetShoppingCart")]
        public IEnumerable<CartRecordWithProductInfo> GetShoppingCart(int customerId)
            => Repo.GetShoppingCartRecords(customerId);


        [HttpPost]
        public IActionResult Create(int customerId, [FromBody]ShoppingCartRecord item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            item.DateCreated = DateTime.Now;
            item.CustomerId = customerId;
            Repo.Add(item);
            //Location: http://localhost:61855/api/ShoppingCart/0 (201)
            return CreatedAtRoute("GetShoppingCart",
                new { controller = "ShoppingCart", customerId = customerId });
        }

        [HttpPut("{shoppingCartRecordId}")]
        public IActionResult Update(int customerId, int shoppingCartRecordId, [FromBody] ShoppingCartRecord item)
        {
            if (item == null || item.Id != shoppingCartRecordId || !ModelState.IsValid)
            {
                return BadRequest();
            }
            item.DateCreated = DateTime.Now;
            Repo.Update(item);
            //Location: http://localhost:61855/api/ShoppingCart/0 (201)
            return CreatedAtRoute("GetShoppingCart", new { customerId = customerId });
        }

        [HttpDelete("{shoppingCartRecordId}/{timeStamp}")]
        public IActionResult Delete(int customerId, int shoppingCartRecordId, string timeStamp)
        {
            if (!timeStamp.StartsWith("\""))
            {
                timeStamp = $"\"{timeStamp}\"";
            }
            var ts = JsonConvert.DeserializeObject<byte[]>(timeStamp);
            Repo.Delete(shoppingCartRecordId, ts);
            return NoContent();
        }

        [HttpPost("buy")]
        public IActionResult Purchase(int customerId, [FromBody] Customer customer)
        {

            if (customer == null || customer.Id != customerId || !ModelState.IsValid)
            {
                return BadRequest();
            }
            int orderId;
            orderId = Repo.Purchase(customerId);
            //Location: http://localhost:61855/api/Orders/0/1
            return CreatedAtRoute("GetOrderDetails", routeValues: 
                new { customerId = customerId, orderId = orderId }, value: orderId);
        }
    }
}
