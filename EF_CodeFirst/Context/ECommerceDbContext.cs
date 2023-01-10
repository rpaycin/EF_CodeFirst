using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Customers)
                .HasForeignKey(c => c.DId);

            modelBuilder.HasDefaultSchema("Ayaz");//oluşturulcak tüm şemalar Ayaz şemasından default türecek

            //IEntityTypeConfiguration interface den türeyen class ları ilgili çalışan assembly içinde bulur, ayarları uygulamasına yardımcı olur
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    //department-customer arasında 1-N ilişki var. 1 departmentda 1 den fazla customer
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // navigation property
        public ICollection<Customer> Customers { get; set; }
    }

    //table anotation ı kullanmaya gerek yok tablo adını farklı vermek istersen kullanabilirsin
    [Table("Musteri")]
    public class Customer
    {
        // her bir entity de mutlaka bir primary key olmalı. primary key in adı Id ve CustomerId olabilir
        public int Id { get; set; }

        //column attribute kullanamana gerek yok, ama aşağıdaki özellikleri kullanabilirsin
        //requeired, db de not null yapar
        [Required]
        [MaxLength(10)]
        [Column("Adi", Order = 1, TypeName = "varchar(50)")]
        public string Name { get; set; }

        //notmapped kullnıırsam db de karşılığı olmaz, db ye kolon açmaz
        [NotMapped]
        public string Surname { get; set; }

        //Customer ve Department arasında 1-N ilişki var. 3 farklı şekilde Customer-Department arasında foreign key tanımlayabiliriz
        //1. Default convention=> yani İlişki kurulan classın adıyla başlayan ve sonuna Id ekleyerek veilen property özelliği. DepartmentId vererek bu işi çözüyoruz, EF kendiliğinden bunu foreignkey olduğunu biliyor
        //2. ForeignKey annotaion ı vererek. property adını istediğimiz gibi verebiliriz
        //3. ise fluent api ile => yani OnModelCreating
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        //en fazla 5 haneli. noktadan sonra 3 haneye kadar tutulur
        [Precision(5, 3)]
        public double Salary { get; set; }

        //
        [DefaultValue(1)]
        public int DId { get; set; }

        // Navigation Property. Customer-Department arasında 1-N ilişki var. İlişkisel tablolar arasındaki fiziksel erişim entity class lar üzerinde sağlayan propertylere Navigation Property denir
        public Department Department { get; set; }

    }


    ///*** modelbuilderı ayırma
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Description { get; set; }
    }

    /// <summary>
    /// order class ındaki db özelliklerini burda ayarlıyoruz
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            List<Order> seedOrders = GetSeedOrders();
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderDate)
                .HasDefaultValue("GETDATE()");

            builder.Property(o => o.Description).
                HasMaxLength(100);

            builder.HasData(GetSeedOrders());//seed data, veri tohumlama anlamına gelir. yani tablo yaratıldıktan sonra tabloya ilk dataları burda gönderiririz
        }

        private static List<Order> GetSeedOrders()
        {
            return new List<Order>
        {
            new Order{OrderId=1,Description="Sipariş 1",OrderDate=DateTime.Now},

            new Order{OrderId=2,Description="Sipariş 2",OrderDate=DateTime.Now}
        };
        }
    }
}
