using Microsoft.EntityFrameworkCore;

namespace ProductManager.Data;

// create class that inherit from DbContext , DbContext is a session to the database, its how we communicate with the database
class ApplicationDbContext : DbContext
{
    // create connectionstring
     static string connectionString = "Server=.;Database=ProductManager;Integrated Security=true;Encrypt=False";

    // create a connection using DbContext to the sql server using our connectionString
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    //DbSet is a set of entitys (Chart)
    public DbSet<Product> Product { get; set; }

}