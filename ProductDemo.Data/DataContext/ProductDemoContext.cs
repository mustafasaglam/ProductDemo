using ProductDemo.Data.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProductDemo.Data.DataContext
{
    public class ProductDemoContext:DbContext
    {
        public ProductDemoContext():base("ProductDemoDb")
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductFeature> ProductFeature { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }


        //modeller oluşurken çalışan bir metod evverride ediliyor.Ve model builder nesnesine s takısı coğulunu veritabanı tablolarını eklememesini diyoruz.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
