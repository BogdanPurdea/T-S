using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models.ViewModels.Base;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;
using Newtonsoft.Json;

namespace Store.Service.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo Repo { get; set; }
        public ProductController(IProductRepo repo, ICategoryRepo categoryRepo)
        {
            Repo = repo;
        }
        [HttpGet]
        public IEnumerable<ProductAndCategoryBase> Get()
            => Repo.GetAllWithCategoryName().ToList();
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.GetOneWithCategoryName(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("featured")]
        public IEnumerable<ProductAndCategoryBase> GetFeatured()
            => Repo.GetFeaturedWithCategoryName().ToList();

        [HttpPost] //required even if method name starts with "Post"
        public IActionResult Create([FromBody]Product product)
        {
            if (product == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Repo.Add(product);
            return NoContent();
        }
        [HttpPut("{productId}")] //Required even if method name starts with Put
        public IActionResult Update(int productId, [FromBody] Product item)
        {
            if (item == null || item.Id != productId || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Update(item);
            return NoContent();
        }

        [HttpDelete("{productId}/{timeStamp}")] //Required even if method name starts with Delete
        public IActionResult Delete(int productId, string timeStamp)
        {
            if (!timeStamp.StartsWith("\""))
            {
                timeStamp = $"\"{timeStamp}\"";
            }
            var ts = JsonConvert.DeserializeObject<byte[]>(timeStamp);
            Repo.Delete(productId, ts);
            return NoContent();
        }
    }
}
