using Microsoft.EntityFrameworkCore;

namespace EF_CodeFirst.Context
{
    /// <summary>
    /// ECommerceDb nin code da karşılığı DbContext ten türelemeli=> DbContext = db nin kendisi
    /// </summary>
    public class ECommerceDbContext : DbContext
    {
        //DbSet = db deki tablolar. contextte olan tablolar dbset olarak prop set ediliyor
        public DbSet<Department> Departments { get; set; }

        public DbSet<Customer> Customers { get; set; }

        //migration için kullanılıyor. migration dosyaları çalıştırılırken hangi cs i kullanabileceğini söylüyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "ECommerceDb";
            optionsBuilder.UseSqlServer($"Data Source=194.61.118.220; Initial Catalog= {dbName}; Persist Security Info=True;User ID= disusr;Password=Dis%022;TrustServerCertificate=True");

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);//entity lerdeki change tracker i kapatır. hiç bir nesne takip edilmez. defaultı tracking 

        }
    }

    public class Customer
    {
        // her bir entity de mutlaka bir primary key olmalı. primary key in adı Id ve CustomerId olabilir
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        //Foreign Key. DepartmentId nin foreignkey olduğunu EF kendisi biliyor(default convention)
        public int DepartmentId { get; set; }

        // Navigation Property. Customer-Department arasında 1-N ilişki var. İlişkisel tablolar arasındaki fiziksel erişim entity class lar üzerinde sağlayan propertylere Navigation Property denir
        public Department Department { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // navigation property
        public ICollection<Customer> Customers { get; set; }
    }
}
