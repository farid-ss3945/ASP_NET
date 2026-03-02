using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class InvoiceManagerDbContext:DbContext
    {
        public InvoiceManagerDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers=>Set<Customer>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(
                p =>
                {
                    p.HasKey(p => p.Id);
                    p.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                    p.Property(p => p.Address)
                    .HasMaxLength(100);
                    p.Property(p=>p.Email)
                    .IsRequired()
                    .HasMaxLength(50);
                    p.Property(p => p.PhoneNumber)
                    .HasMaxLength(50);
                    p.Property(p => p.CreatedAt).IsRequired();
                    p.Property(p => p.UpdatedAt);
                    p.Property(p => p.DeletedAt);
                }
                );
            modelBuilder.Entity<Invoice>(
                s =>
                {
                    s.HasKey(s => s.Id);
                    s.Property(s => s.CustomerId).IsRequired();
                    s.Property(s => s.StartDate).IsRequired();
                    s.Property(s => s.EndDate).IsRequired();
                    s.Property(s => s.TotalSum).IsRequired().HasPrecision(18, 2);
                    s.Property(s => s.Comment).HasMaxLength(500);
                    s.Property(s => s.Status).IsRequired();
                    s.Property(s => s.CreatedAt).IsRequired();
                    s.Property(s => s.UpdatedAt);
                    s.Property(s => s.DeletedAt);

                    s.HasOne(p => p.Customer)
                    .WithMany(s => s.Invoices)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
                }    
                );
            modelBuilder.Entity<InvoiceRow>(
                c => {                     c.HasKey(p => p.Id);
                    c.Property(c => c.InvoiceId).IsRequired();
                    c.Property(c => c.Service).IsRequired().HasMaxLength(200);
                    c.Property(c => c.Quantity).IsRequired().HasPrecision(18, 2);
                    c.Property(c => c.Rate).IsRequired().HasPrecision(18, 2);
                    c.Property(c => c.Sum).IsRequired().HasPrecision(18,2);
                    c.HasOne(c => c.Invoice)
                    .WithMany(s => s.Rows)
                    .HasForeignKey(c => c.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
                }
                );

        }
    }
}
