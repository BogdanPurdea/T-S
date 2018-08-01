using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.MVC.Configuration
{
    public interface IWebServiceLocator
    {
        string ServiceAddress { get; }
    }
}
