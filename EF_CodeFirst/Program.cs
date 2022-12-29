using EF_CodeFirst.Context;
using Microsoft.EntityFrameworkCore;

// 1.EF code first için Microsoft.EntityFrameworkCore.SqlServer ve Microsoft.EntityFrameworkCore.Tools u nugetten yükle
// 2.Öncelikle bir dbcontext(db), dbsetler(tablolar) ve kullanılacak connection string belirlenmesi için DbContext oluşturulmalı
// 3.Migration dosyalarını oluştur
//4. DB nin serverda oluşması için PM de update-database komutu veya MigrateAsync metoduylada migration dosyaları çalıştırılır, db oluşturulur. 
//MigrateAsync metodu ile oluşturmak çok mantıklı, çünkü uygulama ilk ayağa kalkarken db yi oluşturuyorsun

//********DETAY

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
