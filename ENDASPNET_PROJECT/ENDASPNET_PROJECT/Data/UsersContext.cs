using ENDASPNET_PROJECT.Models.Comments;
using ENDASPNET_PROJECT.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ENDASPNET_PROJECT.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Joker",
                Role = "ADMIN"
            },
            new User
            {
                Id = 2,
                Name = "Superman",
                Role = "USER"
            });

            modelBuilder.Entity<Comment>().HasData(
           new Comment
           {
               commentId = 1,
               commentContent = "Perviy",
               commentAuthor = 1,
               commentTime = new DateTime(2019, 10, 4),
               compostID = 1
           },
           new Comment
           {
               commentId = 2,
               commentContent = "Vtoroy",
               commentAuthor = 1,
               commentTime = new DateTime(2019, 10, 4),
               compostID = 1
           });
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UsersContext>
        {
            public UsersContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<UsersContext>();

                var connectionString = configuration.GetConnectionString("Databasezzzzzzzzzzzzz");

                builder.UseSqlServer(connectionString);

                return new UsersContext(builder.Options);
            }
        }
    }
}