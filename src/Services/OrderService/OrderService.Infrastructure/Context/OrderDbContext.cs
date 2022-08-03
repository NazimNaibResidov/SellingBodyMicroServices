using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.AggregateModel.OrderAggreage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Context
{
    public  class OrderDbContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer>  Buyers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
