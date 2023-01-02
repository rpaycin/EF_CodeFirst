using EF_CodeFirst.Context;
using Microsoft.EntityFrameworkCore;

// 1.EF code first için Microsoft.EntityFrameworkCore.SqlServer ve Microsoft.EntityFrameworkCore.Tools u nugetten yükle
// 2.Öncelikle bir dbcontext(db), dbsetler(tablolar) ve kullanılacak connection string belirlenmesi için DbContext oluşturulmalı
// 3.Migration dosyalarını oluştur
//4. DB nin serverda oluşması için PM de update-database komutu veya MigrateAsync metoduylada migration dosyaları çalıştırılır, db oluşturulur. 
//MigrateAsync metodu ile oluşturmak çok mantıklı, çünkü uygulama ilk ayağa kalkarken db yi oluşturuyorsun

//********MİGRATİON DETAY

//migration oluşturmak için PM de => add-migration 'migration_adi' --outputdir klasor_adi
//add-migration 'ecommerce_mig_1' -outputdir migs
// her bir dbset teki değişimleri göre migration lar oluşturulmalı. migration dosyalarına göre db ve tablolar şekilleniyor
//her bir migration dosyasında up ve down metotları override edilir. up metodu ilgili migration dosyasındaki yenilikleri, down metotunda ise migration dosyası kullanılmayacaksa yenilikleri geri alınacak komutlar tutar
//Örneğin 20221229203853_ecommerce_mig_1 migration dosyasında Customer class oluşturmuştum. 20221229205020_ecommerce_mig_2 migration dosyasında ise Customer class ında Quanitty property nin adını değiştirmiştim ve surname property eklemiştim

//varolan migrationları listeleme => get-migration

//oluşturulan tüm  migration dosyalarına göre db yi oluşturma veya güncelleme => update-database
//belli bir migration dosyasına kadar migration dosyası çalıştırma => update-database ecommerce_mig_1

//package-manager dan db yi üretebildiğimiz gibi aşağıdaki komut gibi de db yi üretebiliriz
ECommerceDbContext context = new();
await context.Database.MigrateAsync();

//********CHANGE TRACKER
var customer = new Customer();
//ef nin tüm entityler statelerinin listesini döner =>  context.ChangeTracker.Entries().ToList();

//context.SaveChangesAsync(false); => acceptAllChangesOnSuccess parametresini false set edersen şu anlama gelir. db de başarılı ya da başarısız kaydetme işlemi olursa entityler üzerindeki tracking i kaybetme
//trackingi bitirmek için context.ChangeTracker.AcceptAllChanges();metodu çağrılır. SaveChangesAsync de true çağrılırsa otomatik AcceptAllChanges metodu çağrılır

// entity lerde değişiklik var mı diye kontrol eder => context.ChangeTracker.HasChanges();

//entity nin herhangi bir property sinin db deki orjinal değerini almak için =>  context.Entry(customer).OriginalValues.GetValue<string>(nameof(Customer.Name));

//entity nin db deki orjinal hali => var dbCustomer = await context.Entry(customer).GetDatabaseValuesAsync();


//eğer entityler üzerinde herhangi bir işlem (insert,update veya delete ) yapılmıayacksa MUTLAKA kullan => var customers=context.Customers.AsNoTracking().ToList();
//eğer entiyler üzerinde change track olmasını istiyorsan => context.Customers.AsTracking().ToList();