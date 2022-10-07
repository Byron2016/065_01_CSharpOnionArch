using Microsoft.EntityFrameworkCore;
using SolutionApplication.Database.DbModels;

namespace SolutionApplication.Database.Context
{
    public  class ApplicationDBContext : DbContext
    {
        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=db_bancoOnion_00;User Id=sa;Password=123456;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
