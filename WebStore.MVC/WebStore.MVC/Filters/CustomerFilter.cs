using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStore.MVC.Helpers;

namespace WebStore.MVC.Filters
{
    public class CustomerFilter : IActionFilter
    {

        private ICustomerHelper _customerHelper;
        private IUserHelper _userHelper;
        public CustomerFilter(ICustomerHelper customerHelper, IUserHelper userHelper)
        {
            _customerHelper = customerHelper;
            _userHelper = userHelper;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {

            var viewBag = ((Controller)context.Controller).ViewBag;
            //var customerHelper = (ICustomerHelper)context.HttpContext.RequestServices.GetService(typeof(ICustomerHelper));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                var user = _userHelper.FindByName(userName);
                var customer = _customerHelper.GetCustomerInfo(user.Id);
                viewBag.UserId = user.Id;
                viewBag.CustomerId = customer.Id;
                viewBag.CustomerName = customer.FullName;
            }
            else
            {
                viewBag.UserId = null;
                viewBag.CustomerId = 0;
                viewBag.CustomerName = "Anonymous";
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}