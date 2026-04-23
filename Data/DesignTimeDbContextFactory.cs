using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookstore.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookstoreDb>
{
    public BookstoreDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookstoreDb>();
        optionsBuilder.UseSqlite("Data Source=bookstore.db");
        return new BookstoreDb(optionsBuilder.Options);
    }
}