﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.DAL.EF;
using Store.DAL.Repos.Base;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;
using Store.Models.ViewModels;

namespace Store.DAL.Repos
{
    public class OrderRepo : RepoBase<Order>, IOrderRepo
    {
        private readonly IOrderDetailRepo _orderDetailRepo;
        public OrderRepo(DbContextOptions<StoreContext> options, IOrderDetailRepo
        orderDetailRepo)
        : base(options)
        {
            _orderDetailRepo = orderDetailRepo;
        }
        public OrderRepo(IOrderDetailRepo orderDetailRepo)
        {
            _orderDetailRepo = orderDetailRepo;
        }
        public override IEnumerable<Order> GetAll() => Table.OrderByDescending(x => x.OrderDate);
        public override IEnumerable<Order> GetRange(int skip, int take)
            => GetRange(Table.OrderByDescending(x => x.OrderDate), skip, take);
        public int UpdateAddressAndPhone(int orderId, string billingAddress, string shippingAddress, string phone, bool persist = true)
        {
            Order orderToUpdate = null;
            foreach (Order order in GetAll())
                if (order.Id == orderId)
                {
                    order.BillingAddress = billingAddress;
                    order.ShippingAddress = shippingAddress;
                    order.CustomerPhone = phone;
                    orderToUpdate = order;
                }
            return base.Update(orderToUpdate, persist);
        }
        public IEnumerable<Order> GetOrderHistory(int customerId)
            => Table
                .Where(x => x.CustomerId == customerId)
                .Select(x => new Order
                {
                    Id = x.Id,
                    TimeStamp = x.TimeStamp,
                    CustomerId = customerId,
                    OrderDate = x.OrderDate,
                    OrderTotal = x.OrderTotal,
                    ShipDate = x.ShipDate,
                });
        public OrderWithDetailsAndProductInfo GetOneWithDetails(int customerId, int orderId)
            => Table
                .Include(x => x.OrderDetails)
                .Where(x => x.CustomerId == customerId && x.Id == orderId)
                .Select(x => new OrderWithDetailsAndProductInfo
                {
                    Id = x.Id,
                    CustomerId = customerId,
                    OrderDate = x.OrderDate,
                    OrderTotal = x.OrderTotal,
                    ShipDate = x.ShipDate,
                    OrderDetails = _orderDetailRepo.GetSingleOrderWithDetails(orderId).ToList()
                })
                .FirstOrDefault();
    }
}
