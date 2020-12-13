using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        public DbSet<Books> Books { get; set; } // column name -- Books
        public DbSet<Language> Language { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     var connectString = "Server=127.0.0.1;port=3306;Database=BookStore_f;uid=root;pwd=Zhoujj@123!;Character Set=utf8;";
        //     optionsBuilder.UseMySql(connectString);
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}