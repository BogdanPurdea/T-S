using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Store.MVC.WebServiceAccess.Base;
using Store.Models.Entities;

namespace Store.MVC.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IWebApiCalls _webApiCalls;
        public AuthHelper(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }
        public Customer GetCustomerInfo()
        {
            return _webApiCalls.GetCustomersAsync().Result.FirstOrDefault();
        }
    }
}
