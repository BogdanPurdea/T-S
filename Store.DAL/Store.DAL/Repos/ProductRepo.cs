﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.DAL.EF;
using Store.DAL.Repos.Base;
using Store.DAL.Repos.Interfaces;
using Store.Models.Entities;
using Store.Models.ViewModels.Base;

namespace Store.DAL.Repos
{
    public class ProductRepo : RepoBase<Product>, IProductRepo
    {
        public ProductRepo(DbContextOptions<StoreContext> options) : base(options)
        {
            Table = Context.Products;
        }
        public ProductRepo() : base()
        {
            Table = Context.Products;
        }
        public override IEnumerable<Product> GetAll() => Table.OrderBy(x => x.ModelName);
        internal ProductAndCategoryBase GetRecord(Product p, Category c)
        => new ProductAndCategoryBase()
        {
            CategoryName = c.CategoryName,
            CategoryId = p.CategoryId,
            CurrentPrice = p.CurrentPrice,
            Description = p.Description,
            IsFeatured = p.IsFeatured,
            Id = p.Id,
            ModelName = p.ModelName,
            ModelNumber = p.ModelNumber,
            ProductImage = p.ProductImage,
            ProductImageLarge = p.ProductImageLarge,
            ProductImageThumb = p.ProductImageThumb,
            TimeStamp = p.TimeStamp,
            UnitCost = p.UnitCost,
            UnitsInStock = p.UnitsInStock
        };
        public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int id)
             => Table
                 .Where(p => p.CategoryId == id)
                 .Include(p => p.Category)
                 .Select(item => GetRecord(item, item.Category))
                 .OrderBy(x => x.ModelName);
        public IEnumerable<ProductAndCategoryBase> GetAllWithCategoryName()
            => Table
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.ModelName);
        public IEnumerable<ProductAndCategoryBase> GetFeaturedWithCategoryName()
            => Table
                .Where(p => p.IsFeatured)
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.ModelName);
        public ProductAndCategoryBase GetOneWithCategoryName(int id)
            => Table
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .SingleOrDefault();
        public IEnumerable<ProductAndCategoryBase> Search(string searchString)
            => Table
                .Where(p => p.Description.ToLower().Contains(searchString.ToLower())
                || p.ModelName.ToLower().Contains(searchString.ToLower()))
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.ModelName);
    }
}