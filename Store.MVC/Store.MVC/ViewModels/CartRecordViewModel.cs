using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.MVC.ViewModels.Base;
using Store.MVC.Validations;

namespace Store.MVC.ViewModels
{
    public class CartRecordViewModel : CartViewModelBase
    {
        [MustNotBeGreaterThan(nameof(UnitsInStock))]
        public int Quantity { get; set; }
    }
}
