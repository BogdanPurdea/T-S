using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.MVC.Data;
using WebStore.MVC.Models;

namespace WebStore.MVC.Helpers
{
    public class UserHelper : IUserHelper
    {
        protected readonly ApplicationDbContext Db;
        protected DbSet<ApplicationUser> Table;
        public ApplicationDbContext Context => Db;
        public UserHelper(DbContextOptions<ApplicationDbContext> options)
        {
            Db = new ApplicationDbContext(options);
            Table = Db.Set<ApplicationUser>();
        }
        public UserHelper()
        {
            Db = new ApplicationDbContext();
            Table = Db.Set<ApplicationUser>();
        }
        public IEnumerable<ApplicationUser> GetAll() => Table.OrderBy(x => x.UserName);
        public ApplicationUser FindByName(string name)
        {
            foreach (ApplicationUser user in GetAll())
                if (user.UserName == name)
                    return user;
            return null;
        }
    }
}
