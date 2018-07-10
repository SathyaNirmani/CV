using SuperMarket.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAL
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("Super_Market_Inexis")
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
