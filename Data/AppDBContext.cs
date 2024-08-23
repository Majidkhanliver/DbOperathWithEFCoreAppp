using Microsoft.EntityFrameworkCore;
namespace DbOperathWithEFCoreAppp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType() { Id = 1, Title = "INR", Description = "Indian INR" },
                new CurrencyType() { Id = 2, Title = "Dollar", Description = "Dollar" },
                new CurrencyType() { Id = 3, Title = "Euro", Description = "Euro" },
                new CurrencyType() { Id = 4, Title = "Dinar", Description = "Dinar" }
                );
            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = 1, Title = "English", Description = "English" },
               new Language() { Id = 2, Title = "Urdu", Description = "Urdu" },
               new Language() { Id = 3, Title = "Hindi", Description = "Hindi" },
               new Language() { Id = 4, Title = "Kashmiri", Description = "Kashmiri" }
               );

        }
        //Book name in Database
        public DbSet<Book> Book { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
