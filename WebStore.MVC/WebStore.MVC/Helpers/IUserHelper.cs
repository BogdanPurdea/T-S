using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.MVC.Models;

namespace WebStore.MVC.Helpers
{
    public interface IUserHelper
    {
        ApplicationUser FindByName(string name);
    }
}
