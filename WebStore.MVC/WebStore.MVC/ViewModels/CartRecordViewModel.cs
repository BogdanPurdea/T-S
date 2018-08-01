using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.MVC.ViewModels.Base;
using WebStore.MVC.Validations;

namespace WebStore.MVC.ViewModels
{
    public class CartRecordViewModel : CartViewModelBase
    {
        [MustNotBeGreaterThan(nameof(UnitsInStock))]
        public int Quantity { get; set; }
    }
}
