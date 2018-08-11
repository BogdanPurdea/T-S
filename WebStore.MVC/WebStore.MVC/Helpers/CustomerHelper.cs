using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Store.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.MVC.Models;
using WebStore.MVC.WebServiceAccess.Base;

namespace WebStore.MVC.Helpers
{
    public class CustomerHelper : ICustomerHelper
    {
        private readonly IWebApiCalls _webApiCalls;
        public CustomerHelper(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }
        public Customer GetCustomerInfo(string userId)
        {
            return _webApiCalls.GetCustomerAsync(userId).Result;
        }
    }
}
