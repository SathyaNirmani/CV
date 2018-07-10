using SuperMarket.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private EntityContext context = new EntityContext();

        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<CustomerOrder> customerOrderRepository;
        private GenericRepository<Order> orderDetailRepository;
        private GenericRepository<UserProfile> userRepository;

        
        public GenericRepository<Customer> CustomerRepository
        { 
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<CustomerOrder> CustomerOrderRepository
        {
            get
            {
                if (this.customerOrderRepository == null)
                {
                    this.customerOrderRepository = new GenericRepository<CustomerOrder>(context);
                }
                return customerOrderRepository;
            }
        }

        public GenericRepository<Order> OrderDetailRepository
        {
            get
            {
                if (this.orderDetailRepository == null)
                {
                    this.orderDetailRepository = new GenericRepository<Order>(context);
                }
                return orderDetailRepository;
            }
        }

        public GenericRepository<UserProfile> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<UserProfile>(context);
                }
                return userRepository;
            }
        }


        public EntityContext DbContext
        {
            get { return context; }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
