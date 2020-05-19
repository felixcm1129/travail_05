using BillingManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingManagement.UI.ViewModels
{
    public class BillingManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source = BillingManagement.db");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}