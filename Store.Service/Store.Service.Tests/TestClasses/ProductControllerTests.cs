using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Store.Models.ViewModels;
using Store.Service.Tests.TestClasses.Base;
using Xunit;
using Store.Models.ViewModels.Base;
using System.Text;
using Store.Service.Tests.Helpers;

namespace Store.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public class ProductControllerTests : BaseTestClass
    {
        public ProductControllerTests()
        {
            RootAddress = "api/product";
        }

        [Fact]
        public async void ShouldGetAllProductsWithCategoryName()
        {
            //Get All Products With Category Name: http://localhost:61855/api/product
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ServiceAddress}{RootAddress}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryNames = JsonConvert.DeserializeObject<List<ProductAndCategoryBase>>(jsonResponse);
                Assert.Equal(41,productWithCategoryNames.Count);
                Assert.Equal("Protection",productWithCategoryNames[0].CategoryName);
            }
        }

        [Fact]
        public async void ShouldGetOneProductWithCategoryName()
        {
            //Get Featured Products With Category Name: http://localhost:61855/api/product/featured
            //Get One Product With Category Name: http://localhost:61855/api/product/2
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ServiceAddress}{RootAddress}/1");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryName = JsonConvert.DeserializeObject<ProductAndCategoryBase>(jsonResponse);
                Assert.Equal("Multi-Purpose Rubber Band", productWithCategoryName.ModelName);
                Assert.Equal("Munitions",productWithCategoryName.CategoryName);
            }
        }

        [Fact]
        public async void ShouldFailIfBadCustomerId()
        {
            //Get One Product with Category Name: http://localhost:61855/api/product/1
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ServiceAddress}{RootAddress}/100");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            }
        }
        [Fact]
        public async void ShouldAddProduct()
        {
            // Add product: http://localhost:61855/api/product HTTPPost
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var categoryId = 1;
                var description = "desc";
                var modelName = "name";
                var modelNumber = "number";
                var productImage = "product-image.png";
                var productImageLarge = "product-image-lg.png";
                var productImageThumb = "product-thumb.png";
                var unitCost = 10;
                var currentPrice = 10;
                var unitsInStock = 3;
                var isFeatured = true;
                var targetUrl = $"{ServiceAddress}{RootAddress}";
                var response = await client.PostAsync(targetUrl,
                    new StringContent("{" + $"\"CategoryId\":{categoryId},\"Description\":{description},\"ModelName\":{modelName},\"ModelNumber\":{modelNumber}" +
                        $",\"ProductImage\":{productImage},\"ProductImageLarge\":{productImageLarge},\"ProductImageThumb\":{productImageThumb}" +
                        $",\"UnitCost\":{unitCost},\"CurrentPrice\":{currentPrice},\"UnitsInStock\":{unitsInStock},\"IsFeatured\":{isFeatured}" + "}",
                        Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal(targetUrl.ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the product was added
            var products = await ProductTestHelpers.GetProducts(ServiceAddress, RootAddress);
            var product = products[products.Count - 1];
            Assert.Equal(1, product.CategoryId);
            Assert.Equal("desc", product.Description);
            Assert.Equal("name", product.ModelName);
            Assert.Equal("number", product.ModelNumber);
            Assert.Equal("product-image.png", product.ProductImage);
            Assert.Equal("product-image-lg.png", product.ProductImageLarge);
            Assert.Equal("product-thumb.png", product.ProductImageThumb);
            Assert.Equal(10, product.UnitCost);
            Assert.Equal(10, product.CurrentPrice);
            Assert.Equal(3, product.UnitsInStock);
            Assert.True(product.IsFeatured);
        }
    }
}
