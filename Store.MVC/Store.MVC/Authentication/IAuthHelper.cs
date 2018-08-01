using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Store.Models.Entities;

namespace Store.MVC.Authentication
{
    public interface IAuthHelper
    {
        Customer GetCustomerInfo();
    }
}
