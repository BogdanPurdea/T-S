using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models.ViewModels.Base;
using Store.DAL.Repos.Interfaces;
namespace Store.Service.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo Repo { get; set; }
        public ProductController(IProductRepo repo)
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
    }
}
