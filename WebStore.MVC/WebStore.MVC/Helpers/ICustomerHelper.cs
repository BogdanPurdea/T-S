using Store.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.MVC.Helpers
{
    public interface ICustomerHelper
    {
        Customer GetCustomerInfo(string userId);
    }
}
