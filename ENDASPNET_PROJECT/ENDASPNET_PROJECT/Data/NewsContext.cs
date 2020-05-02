using ENDASPNET_PROJECT.Models.Categories;
using ENDASPNET_PROJECT.Models.Comments;
using ENDASPNET_PROJECT.Models.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENDASPNET_PROJECT.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet <Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
            new Post
            {
                postId = -1,
                postTitle = "Sportland",
                postContent = "Kto to bezhit podezde",
                postAuthor = "Joker",
                postCategory = "Sport",
                postTime = new DateTime(2019, 10, 3)
            },
            new Post
            {
                postId = -2,
                postTitle = "Tak skazano U-la-la",
                postContent = "V podezde okolo doma prodayet beliyash, biznes vo vremya karantina",
                postAuthor = "Joker",
                postCategory = "Food",
                postTime = new DateTime(2019, 10, 3)
            });

            modelBuilder.Entity<Category>().HasData(
           new Category
           {
               categoryId = 1,
               categoryName = "Sport"
           },
           new Category
           {
               categoryId = 2,
               categoryName = "Food"
           });

           
        }
    }
}
