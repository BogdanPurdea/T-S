using System;
using System.Collections.ObjectModel;
using Store.Models.Entities;
using Store.DAL.Repos;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Store.DAL.EF;
using Store.DAL.EF.Migrations;

namespace Store.DAL.Tests.RepoTests
{
    [Collection("Store.DAL")]
    public class CategoryRepoExceptionTests : IDisposable
    {
        private readonly CategoryRepo _repo;

        public CategoryRepoExceptionTests()
        {
            _repo = new CategoryRepo();
            CleanDatabase();
        }

        public void Dispose()
        {
            CleanDatabase();
            _repo.Dispose();
        }

        private void CleanDatabase()
        {
            _repo.Context.Database.ExecuteSqlCommand("Delete from Store.Categories");
            _repo.Context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"Store.Categories\", RESEED, -1);");
        }

        [Fact]
        public void ShouldNotDeleteACategoryFromSameContextWithConcurrencyIssue()
        {
            var category = new Category {CategoryName = "Foo"};
            _repo.Add(category);
            Assert.Equal(1, _repo.Count);
            var ex = Assert.Throws<Exception>(() => _repo.Delete(category.Id, null, false));
        }

        [Fact]
        public void ShouldNotDeleteOnConcurrencyIssue()
        {
            var category = new Category {CategoryName = "Foo"};
            _repo.Add(category);
            _repo.Context.Database.ExecuteSqlCommand("Update Store.Categories set CategoryName = 'Bar'");
            var ex = Assert.Throws<DbUpdateConcurrencyException>(() => _repo.Delete(category.Id, category.TimeStamp));
        }

        /*[Fact]
        public void ShouldThrowRetryExeptionWhenCantConnect()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            var connectionString =
                @"Server=DESKTOP-4JR0AR5\SQLEXPRESS;Database=Store;Trusted_Connection=True;MultipleActiveResultSets=true;";
            contextOptionsBuilder.UseSqlServer(connectionString, 
                o => o.EnableRetryOnFailure(2,new TimeSpan(0,0,0,0,100),new Collection<int>{ -2146232060 }));
            //contextOptionsBuilder.UseSqlServer(connectionString,
            //    o => o.ExecutionStrategy(c => new MyExecutionStrategy(c, 5, new TimeSpan(0, 0, 0, 0, 30))));
            var repo = new CategoryRepo(contextOptionsBuilder.Options);
            var category = new Category {CategoryName = "Foo"};
            var ex = Assert.Throws<RetryLimitExceededException>(()=> repo.Add(category));
        }*/

    }
}