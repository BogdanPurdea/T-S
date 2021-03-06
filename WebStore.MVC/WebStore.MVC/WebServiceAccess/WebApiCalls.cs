﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebStore.MVC.Configuration;
using Store.Models.Entities;
using Store.Models.ViewModels;
using Store.Models.ViewModels.Base;
using WebStore.MVC.WebServiceAccess.Base;

namespace WebStore.MVC.WebServiceAccess
{
    public class WebApiCalls : WebApiCallsBase, IWebApiCalls
    {
        public WebApiCalls(IWebServiceLocator settings) : base(settings)
        {
        }
        public async Task<IList<CartRecordWithProductInfo>> GetCartAsync(int customerId)
        {
            // http://localhost:44315/api/ShoppingCart/0
            return await GetItemListAsync<CartRecordWithProductInfo>(
                $"{CartBaseUri}{customerId}");
        }
        public async Task<CartRecordWithProductInfo> GetCartRecordAsync(int customerId,
            int productId)
        {
            // http://localhost:44315/api/ShoppingCart/0/0
            return await GetItemAsync<CartRecordWithProductInfo>(
                $"{CartBaseUri}{customerId}/{productId}");
        }
        public async Task<string> AddToCartAsync(int customerId, int productId,
            int quantity)
        {
            //http://localhost:44315/api/shoppingcart/{customerId} HTTPPost
            //Note: ProductId and Quantity in the body
            //http://localhost:44315/api/shoppingcart/0 {"ProductId":22,"Quantity":2}
            // Content-Type:application/json
            string uri = $"{CartBaseUri}{customerId}";
            string json = $"{{\"ProductId\":{productId},\"Quantity\":{quantity}}}";
            return await SubmitPostRequestAsync(uri, json);
        }
        public async Task<int> PurchaseCartAsync(Customer customer)
        {
            //Purchase: http://localhost:44315/api/shoppingcart/{customerId}/buy HTTPPost
            //Note: Customer in the body
            //{ "Id":1,"FullName":"Super Spy","EmailAddress":"spy@secrets.com"}
            // http://localhost:44315/api/shoppingcart/0/buy
            var json = JsonConvert.SerializeObject(customer);
            var uri = $"{CartBaseUri}{customer.Id}/buy";
            return int.Parse(await SubmitPostRequestAsync(uri, json));
        }
        public async Task<string> UpdateCartItemAsync(ShoppingCartRecord item)
        {
            // Change Cart Item(Qty): http://localhost:44315/api/shoppingcart/{customerId}/{id}HTTPPut
            // Note: Id, CustomerId, ProductId, TimeStamp, DateCreated, and Quantity in the body
            //{"Id":0,"CustomerId":0,"ProductId":32,"Quantity":2,
            // "TimeStamp":"AAAAAAAA86s=","DateCreated":"1/20/2016"}
            //http://localhost:44315/api/shoppingcart/0/AAAAAAAA86s=
            string uri = $"{CartBaseUri}{item.CustomerId}/{item.Id}";
            var json = JsonConvert.SerializeObject(item);
            return await SubmitPutRequestAsync(uri, json);
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            //http://localhost:44315/api/category/{id}
            return await GetItemAsync<Category>($"{CategoryBaseUri}{id}");
        }
        public async Task<IList<ProductAndCategoryBase>> GetProductsForACategoryAsync(int categoryId)
        {
            // http://localhost:44315/api/category/{categoryId}/products
            var uri = $"{CategoryBaseUri}{categoryId}/products";
            return await GetItemListAsync<ProductAndCategoryBase>(uri);
        }
        public async Task<IList<Customer>> GetCustomersAsync()
        {
            //Get All Customers: http://localhost:44315/api/customer
            return await GetItemListAsync<Customer>($"{CustomerBaseUri}");
        }
        public async Task<Customer> GetCustomerAsync(int id)
        {
            //Get One customer: http://localhost:44315/api/customer/{id}
            //http://localhost:44315/api/customer/1
            return await GetItemAsync<Customer>($"{CustomerBaseUri}{id}");
        }
        public async Task<Customer> GetCustomerAsync(string userId)
        {
            //Get One customer: http://localhost:44315/api/customer/{userId}
            //http://localhost:44315/api/customer/0b79191b-b587-405c-ae2a-e51a
            return await GetItemAsync<Customer>($"{CustomerBaseUri}/ByUser/{userId}");
        }
        public async Task<string> AddCustomerAsync(string fullName, string emailAddress, string userId)
        {
            string uri = $"{CustomerBaseUri}";
            string json = $"{{\"FullName\":\"{fullName}\",\"EmailAddress\":\"{emailAddress}\",\"UserId\":\"{userId}\"}}";
            return await SubmitPostRequestAsync(uri, json);
        }
        public async Task<IList<ProductAndCategoryBase>> GetFeaturedProductsAsync()
        {
            // http://localhost:44315/api/product/featured
            return await GetItemListAsync<ProductAndCategoryBase>($"{ProductBaseUri}featured");
        }
        public async Task<ProductAndCategoryBase> GetOneProductAsync(int productId)
        {
            // http://localhost:44315/api/product/{id}
            return await GetItemAsync<ProductAndCategoryBase>($"{ProductBaseUri}{productId}");
        }
        public async Task<string> AddProductAsync(int categoryId, string description, string modelName, string modelNumber, string productImage, string productImageLarge, string productImageThumb, decimal unitCost, decimal currentPrice, int unitsInStock, bool isFeatured)
        {
            string uri = $"{ProductBaseUri}";
            string json = $"{{\"CategoryId\":{categoryId},\"Description\":\"{description}\",\"ModelName\":\"{modelName}\"," +
                $"\"ModelNumber\":\"{modelNumber}\",\"ProductImage\":\"{productImage}\",\"ProductImageLarge\":\"{productImageLarge}\"," +
                $"\"ProductImageThumb\":\"{productImageThumb}\",\"UnitCost\":{unitCost},\"CurrentPrice\":{currentPrice}," +
                $"\"UnitsInStock\":{unitsInStock},\"IsFeatured\":\"{isFeatured}\"}}";
            return await SubmitPostRequestAsync(uri, json);
        }
        public async Task<string> UpdateProductAsync(ProductAndCategoryBase item)
        {
            string uri = $"{ProductBaseUri}/{item.Id}";
            var json = JsonConvert.SerializeObject(item);
            return await SubmitPutRequestAsync(uri, json);
        }
        public async Task<IList<Order>> GetOrdersAsync(int customerId)
        {
            //Get Order History: http://localhost:44315/api/orders/{customerId}
            return await GetItemListAsync<Order>($"{OrdersBaseUri}{customerId}");
        }
        public async Task<OrderWithDetailsAndProductInfo> GetOrderDetailsAsync(int customerId, int orderId)
        {
            //Get Order Details: http://localhost:44315/api/orders/{customerId}/{orderId}
            var url = $"{OrdersBaseUri}{customerId}/{orderId}";
            return await GetItemAsync<OrderWithDetailsAndProductInfo>(url);
        }
        public async Task<IList<ProductAndCategoryBase>> SearchAsync(string searchTerm)
        {
            var uri = $"{ServiceAddress}api/search/{searchTerm}";
            return await GetItemListAsync<ProductAndCategoryBase>(uri);
        }
        public async Task RemoveCartItemAsync(int customerId, int shoppingCartRecordId, byte[] timeStamp)
        {
            //Remove Cart Item:
            // http://localhost:44315/api/shoppingcart/{customerId}/{id}/{TimeStamp} HTTPDelete
            // http://localhost:44315/api/shoppingcart/0/0/AAAAAAAA1Uc=
            var timeStampString = JsonConvert.SerializeObject(timeStamp);
            var uri = $"{CartBaseUri}{customerId}/{shoppingCartRecordId}/{timeStampString}";
            await SubmitDeleteRequestAsync(uri);
        }
        public async Task RemoveProductAsync(int productId, byte[] timeStamp)
        {
            var timeStampString = JsonConvert.SerializeObject(timeStamp);
            var uri = $"{ProductBaseUri}/{productId}/{timeStampString}";
            await SubmitDeleteRequestAsync(uri);
        }
        public async Task<IList<Category>> GetCategoriesAsync()
        {
            //http://localhost:44315/api/category
            return await GetItemListAsync<Category>(CategoryBaseUri);
        }
        public async Task<string> UpdateOrderAddressAndPhone(int customerId, int orderId, string billingAddress, string shippingAddress, string phone)
        {
            //http://localhost:44315/api/orders/{customerId}/{orderId} HTTPPut
            //Note: BillingAddress, ShippingAddress and Phone in the body
            //http://localhost:44315/api/shoppingcart/0/1 {"BillingAddress":b_addr,"ShippingAddress":s_addr,"CustomerPhone":1234567890}
            // Content-Type:application/json
            string uri = $"{OrdersBaseUri}{customerId}/{orderId}";
            string json = $"{{\"BillingAddress\":\"{billingAddress}\",\"ShippingAddress\":\"{shippingAddress}\",\"CustomerPhone\":\"{phone}\"}}";
            return await SubmitPutRequestAsync(uri, json);
        }        
    }
}
