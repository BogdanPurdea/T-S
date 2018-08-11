using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.DAL.EF;
using Store.DAL.Repos.Base;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;

namespace Store.DAL.Repos
{
    public class CustomerRepo : RepoBase<Customer>, ICustomerRepo
    {
        public CustomerRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public CustomerRepo() : base()
        {
        }
        public override IEnumerable<Customer> GetAll() => Table.OrderBy(x => x.FullName);
        public override IEnumerable<Customer> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.FullName), skip, take);
        public Customer FindByUserId(string userId)
        {
            foreach (Customer c in GetAll())
                if (c.UserId == userId)
                    return c;
            return null;
        }
    }
}
