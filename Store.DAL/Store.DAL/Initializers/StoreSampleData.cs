using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Models.Entities;
using Store.DAL.EF;

namespace Store.DAL.Initializers
{
    public static class StoreSampleData
    {
        public static IEnumerable<Category> GetCategories() => new List<Category>
        {
            new Category {CategoryName = "Communications"},
            new Category {CategoryName = "Deception"},
            new Category {CategoryName = "Travel"},
        };
        public static IList<Product> GetProducts(IList<Category> categories)
        {
            var products = new List<Product>();
            foreach (var category in categories)
            {
                switch (category.CategoryName)
                {
                    case "Communications":
                        products.AddRange(new List<Product>
                        {
                        new Product
                        {
                        Category = category,
                        CategoryId = category.Id,
                        ProductImage = "product-image.png",
                        ProductImageLarge = "product-image-lg.png",
                        ProductImageThumb = "product-thumb.png",
                        ModelNumber = "RED1",
                        ModelName = "Communications Device",
                        UnitCost = 49.99M,
                        CurrentPrice = 49.99M,
                        Description = "Lorem ipsum",
                        UnitsInStock = 2,
                        IsFeatured = true
                        },
                        });
                        break;
                    default:
                        break;
                }
            }
            return products;
        }
        public static IEnumerable<Customer> GetAllCustomerRecords(StoreContext context)
        => new List<Customer>
        {
            new Customer() { EmailAddress = "customer@mail.com",Password = "Foo",
            FullName = "Customer 1",
            }
        };
        public static List<Order> GetOrders(Customer customer, StoreContext context)
        => new List<Order>
        {
            new Order()
            {
                Customer = customer,
                OrderDate = DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)),
                ShipDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                OrderDetails = GetOrderDetails(context)
            }
        };
        public static IList<OrderDetail> GetOrderDetails(Order order, StoreContext context)
        {
            var prod1 = context.Categories.Include(c => c.Products).FirstOrDefault()?
            .Products.FirstOrDefault();
            var prod2 = context.Categories.Skip(1).Include(c => c.Products).FirstOrDefault()?
            .Products.FirstOrDefault();
            var prod3 = context.Categories.Skip(2).Include(c => c.Products).FirstOrDefault()?
            .Products.FirstOrDefault();
            return new List<OrderDetail>
            {
                new OrderDetail() {Order = order, Product = prod1, Quantity = 3,
                UnitCost = prod1.CurrentPrice},
                new OrderDetail() {Order = order, Product = prod2, Quantity = 2,
                UnitCost = prod2.CurrentPrice},
                new OrderDetail() {Order = order, Product = prod3, Quantity = 5,
                UnitCost = prod3.CurrentPrice},
            };
        }
        public static List<OrderDetail> GetOrderDetails(
           StoreContext context)
        {
            var prod1 = context.Categories
                .Include(c => c.Products).FirstOrDefault()?
                .Products.Skip(3).FirstOrDefault();
            var prod2 = context.Categories.Skip(2)
                .Include(c => c.Products).FirstOrDefault()?
                .Products.Skip(2).FirstOrDefault();
            var prod3 = context.Categories.Skip(5)
                .Include(c => c.Products).FirstOrDefault()?
                .Products.Skip(1).FirstOrDefault();
            return new List<OrderDetail>
            {
                new OrderDetail() {Product = prod1, Quantity = 3, UnitCost = prod1.CurrentPrice},
                new OrderDetail() {Product = prod2, Quantity = 2, UnitCost = prod2.CurrentPrice},
                new OrderDetail() {Product = prod3, Quantity = 5, UnitCost = prod3.CurrentPrice},
            };
        }
        public static IList<ShoppingCartRecord> GetCart(Customer customer, StoreContext context)
        {
            var prod1 = context.Categories.Skip(1)
            .Include(c => c.Products).FirstOrDefault()?
            .Products.FirstOrDefault();
            return new List<ShoppingCartRecord>
            {
                new ShoppingCartRecord {
                Customer = customer, DateCreated = DateTime.Now,
                Product = prod1, Quantity = 1}
            };
        }
    }
}
