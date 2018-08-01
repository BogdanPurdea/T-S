using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebStore.MVC.WebServiceAccess.Base;

namespace WebStore.MVC.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly IWebApiCalls _webApiCalls;
        public Menu(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _webApiCalls.GetCategoriesAsync();
            if (categories == null)
            {
                return new ContentViewComponentResult("There was an error getting the categories");
            }
            return View("MenuView", categories);
        }
    }
}
