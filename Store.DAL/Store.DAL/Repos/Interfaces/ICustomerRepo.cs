using System;
using System.Collections.Generic;
using System.Text;
using Store.DAL.Repos.Base;
using Store.Models.Entities;

namespace Store.DAL.Repos.Interfaces
{
    public interface ICustomerRepo : IRepo<Customer>
    {
        Customer FindByUserId(string userId);

    }
}
