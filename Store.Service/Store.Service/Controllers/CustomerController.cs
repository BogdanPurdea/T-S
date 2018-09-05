using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Entities;
using Store.DAL.Repos.Interfaces;

namespace Store.Service.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepo Repo { get; set; }
        public CustomerController(ICustomerRepo repo)
        {
            Repo = repo;
        }

        [HttpGet]
        public IEnumerable<Customer> Get() => Repo.GetAll();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("ByUser/{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var item = Repo.FindByUserId(userId);
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Customer customer)
        {
            var model = ModelState;
            if (customer == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            if(Repo.FindByUserId(customer.UserId) == null)
                Repo.Add(customer);
            return NoContent();
        }
    }
}