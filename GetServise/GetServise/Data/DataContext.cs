using GetServise.Models;
using Microsoft.EntityFrameworkCore;

namespace GetServise.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Calendar> Calendars { get; set; }
    }
}
