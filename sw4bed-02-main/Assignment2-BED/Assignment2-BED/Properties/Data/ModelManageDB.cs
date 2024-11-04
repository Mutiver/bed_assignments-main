using Microsoft.EntityFrameworkCore;
using Assignment2_BED.Models;

namespace Assignment2_BED.Data
{
    public class ModelManageDB : DbContext
    {
        // private const string DbName = "BED2";
        // private const string ConnectionString = $"Data Source=localhost;Initial Catalog={DbName};User ID=sa;Password=<passw0rd1;Connect Timeout=10;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public ModelManageDB(DbContextOptions<ModelManageDB> options)   : base(options){ }

        public DbSet<Expense> expense { get; set; } = default!;

        public DbSet<Job> job { get; set; } = default!;

        public DbSet<Model> model { get; set; } = default!;
        
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseSqlServer(ConnectionString);
        //
    }
}
