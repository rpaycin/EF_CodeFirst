using EF_CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_CodeFirst.Context
{
    /// <summary>
    /// ECommerceDb nin code da karşılığı DbContext ten türelemeli=> DbContext = db
    /// </summary>
    public class ECommerceDbContext : DbContext
    {
        //tablolar=DbSet
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        //migration için kullanılıyor. migration dosyaları çalıştırılırken hangi cs i kullanabileceğini söylüyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "ECommerceDb";
            optionsBuilder.UseSqlServer($"Data Source=194.61.118.220; Initial Catalog= {dbName}; Persist Security Info=True;User ID= disusr;Password=Dis%022;TrustServerCertificate=True");

        }
    }
}
