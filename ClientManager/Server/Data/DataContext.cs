using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace ClientManager.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Costumer> Costumers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Costumer[] postsToSeed = new Costumer[6];

            for (int i = 1; i <= 6; i++)
            {
                bool isCompany;
                if (i % 2 == 0)
                {
                    isCompany = true;
                }
                else
                {
                    isCompany = false;
                }
                postsToSeed[i - 1] = new Costumer
                {
                    Id = i,
                    IsCompany = isCompany,
                    IdNum = i,
                    FirstName = $"First {i}",
                    LastName = $"Last {i}",
                    Phone = 1000+i,
                    Address = $"address: {i}",
                };
            }
            modelBuilder.Entity<Costumer>()
                        .HasIndex(b => b.IdNum)
                        .IsUnique();        

        modelBuilder.Entity<Costumer>().HasData(postsToSeed);
        }

        //dotnet ef migrations add Initial
        //dotnet ef database update

    }
}
