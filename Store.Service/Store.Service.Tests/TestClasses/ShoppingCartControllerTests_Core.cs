using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Store.DAL.EF;
using Store.DAL.Initializers;
using Store.Models.Entities;
using Store.Models.ViewModels;
using Store.Service.Tests.TestClasses.Base;
using Xunit;

namespace Store.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public partial class ShoppingCartControllerTests : BaseTestClass
    {
        private int _customerId = 0;
        private int _productId = 32;
        public ShoppingCartControllerTests()
        {
            RootAddress = "api/shoppingcart";
            StoreDataInitializer.InitializeData(new StoreContext());
        }

        public override void Dispose()
        {
            StoreDataInitializer.InitializeData(new StoreContext());
        }


    }
}
