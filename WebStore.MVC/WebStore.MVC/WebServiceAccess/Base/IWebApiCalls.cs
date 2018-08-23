using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models.Entities;
using Store.Models.ViewModels;
using Store.Models.ViewModels.Base;

namespace WebStore.MVC.WebServiceAccess.Base
{
    public interface IWebApiCalls
    {
        Task<IList<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<IList<ProductAndCategoryBase>> GetProductsForACategoryAsync(int categoryId);
        Task<IList<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<Customer> GetCustomerAsync(string userId);
        Task<IList<Order>> GetOrdersAsync(int customerId);
        Task<OrderWithDetailsAndProductInfo> GetOrderDetailsAsync(int customerId, int orderId);
        Task<string> UpdateOrderAddressAndPhone(int customerId, int orderId, string billingAddress, string shippingAddress, string phone);
        Task<ProductAndCategoryBase> GetOneProductAsync(int productId);
        Task<string> AddProductAsync(int categoryId, string description, string modelName, string modelNumber,
            string productImage, string productImageLarge, string productImageThumb, decimal unitCost, decimal currentPrice, int unitsInStock, bool isFeatured);
        Task<string> UpdateProductAsync(ProductAndCategoryBase item);
        Task<IList<ProductAndCategoryBase>> GetFeaturedProductsAsync();
        Task<IList<ProductAndCategoryBase>> SearchAsync(string searchTerm);
        Task<IList<CartRecordWithProductInfo>> GetCartAsync(int customerId);
        Task<CartRecordWithProductInfo> GetCartRecordAsync(int customerId, int productId);
        Task<string> AddToCartAsync(int customerId, int productId, int quantity);
        Task<string> UpdateCartItemAsync(ShoppingCartRecord item);
        Task RemoveCartItemAsync(int customerId, int shoppingCartRecordId, byte[] timeStamp);
        Task RemoveProductAsync(int productId, byte[] timeStamp);
        Task<int> PurchaseCartAsync(Customer customer);
    }
}
