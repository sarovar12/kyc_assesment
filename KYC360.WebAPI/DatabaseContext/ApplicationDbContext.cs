using KYC360.WebAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace KYC360.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Entity> Entity { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Name> Name { get; set; }
        public DbSet<Date> Date { get; set; }


    }
}
