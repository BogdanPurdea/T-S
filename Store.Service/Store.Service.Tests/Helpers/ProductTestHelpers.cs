using AutoMapper;
using Newtonsoft.Json;
using Store.Models.Entities;
using Store.Models.ViewModels;
using Store.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Store.Service.Tests.Helpers
{
    public static class ProductTestHelpers
    {
        static ProductTestHelpers()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ProductAndCategoryBase, Product>();
                });
        }
        public static async Task<List<ProductAndCategoryBase>> GetProducts(string serviceAddress, string rootAddress)
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{serviceAddress}{rootAddress}");
                //Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductAndCategoryBase>>(jsonResponse);
            }
        }
    }
}
