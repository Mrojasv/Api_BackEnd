using Microsoft.EntityFrameworkCore;

namespace Api_BackEnd.Models
{
    public class InvoiceContent : DbContext
    {
        public InvoiceContent(DbContextOptions<InvoiceContent> options)
           : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(c => new { c.invoice_id ,c.product});
        }
    }
}
