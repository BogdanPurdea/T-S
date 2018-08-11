using System.Linq;
using Store.DAL.Initializers;
using Store.DAL.Repos;
using Xunit;
using System;

namespace Store.DAL.Tests.Repos
{
    [Collection("Store.DAL")]
    public class OrderDetailRepoTests : IDisposable
    {
        private readonly OrderDetailRepo _repo;

        public OrderDetailRepoTests()
        {
            _repo = new OrderDetailRepo();
            StoreDataInitializer.ClearData(_repo.Context);
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Fact]
        public void ShouldGetAllOrderDetails()
        {
            var orders = _repo.GetAll().ToList();
            Assert.Equal(_repo.Count,orders.Count());
        }

        [Fact]
        public void ShouldGetLineItemTotal()
        {
            var orderDetails = _repo.GetAll().ToList();
            var orderDetail = orderDetails[0];
            Assert.Equal(1799.9700M, orderDetail.LineItemTotal);
        }

    }
}
