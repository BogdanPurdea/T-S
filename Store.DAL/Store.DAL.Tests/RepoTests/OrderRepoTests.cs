using System.Linq;
using Store.DAL.Initializers;
using Store.DAL.Repos;
using Xunit;
using System;

namespace Store.DAL.Tests.Repos
{
    [Collection("Store.DAL")]
    public class OrderRepoTests : IDisposable
    {
        private readonly OrderRepo _repo;

        public OrderRepoTests()
        {
            _repo = new OrderRepo(new OrderDetailRepo());
            StoreDataInitializer.ClearData(_repo.Context);
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Fact]
        public void ShouldGetAllOrders()
        {
            var orders = _repo.GetAll().ToList();
            Assert.Equal(1,orders.Count());
        }
        [Fact]
        public void ShouldUpdateAddressAndPhoneOfAnOrder()
        {
            int orderId = 0;
            _repo.UpdateAddressAndPhone(orderId, "billing address", "shipping address", "1234567890");
            var order = _repo.Find(orderId);
            Assert.Equal("billing address", order.BillingAddress);
            Assert.Equal("shipping address", order.ShippingAddress);
            Assert.Equal("1234567890", order.CustomerPhone);
        }
    }
}
