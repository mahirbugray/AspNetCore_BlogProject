using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.DataAccess.Identity;
using Wissen.Bright.BlogProject.App.Entity.Entities;

namespace Wissen.Bright.BlogProject.App.DataAccess.Contexts
{
    public class BlogDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Fluent API
            builder.Entity<Article>().Property("Title").IsRequired().HasMaxLength(200);
            builder.Entity<Article>().Property("Summary").IsRequired().HasMaxLength(500);

            builder.Entity<Category>().Property("Name").IsRequired().HasMaxLength(100);

            builder.Entity<Tag>().Property("Content").IsRequired().HasMaxLength(20);

            builder.Entity<Comment>().Property("Content").IsRequired().HasMaxLength(20);

            //Seed Data
            builder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "C#.Net Programming", Description = "C#.Net Introduction" },
                new Category() { Id = 2, Name = "Entity Framework Core", Description = "Entity Framework Core ile ORM teknolojileri" },
                new Category() { Id = 3, Name = "Asp.Net Core Mvc", Description = "Asp.Net Core Mvc ile Web Programlama" }
                );

            builder.Entity<Article>().HasData(
                 new Article() { Id = 1, Title = "C#.Net Introduction", Summary = "Visual Studio .Net ortamında C#.Net ile temel seviyeden (veri türleri, değişkenler, karar verme (if - else), döngüler, diziler) ileri düzeye (Nesneye dayalı programlama (OOP), collections, generic collections, Linq Sorguları) eğitim programı...", Content= "Visual Studio .Net ortamında C#.Net ile temel seviyeden (veri türleri, değişkenler, karar verme (if - else), döngüler, diziler) ileri düzeye (Nesneye dayalı programlama (OOP), collections, generic collections, Linq Sorguları) eğitim programı.", CategoryId = 1, UserId = 1, PictureUrl = "CSharp01.jpg" },
                  new Article() { Id = 2, Title = "Entity Framework Core ile ORM", Summary = "Visual Studio .Net ortamında Entity Framework Core ORM teknolojisini kullanarak veritabanı yönetiminin en popüler yollarını eğitim programı...", Content = "Visual Studio .Net ortamında Entity Framework Core ORM teknolojisini kullanarak veritabanı yönetiminin en popüler yollarını eğitim programı.", CategoryId = 2, UserId = 2, PictureUrl = "EFCore01.jpg" },
                  new Article() { Id = 3, Title = "Asp.Net Core MVC ile Web Programlama", Summary = "Visual Studio .Net ortamında Asp.Net Core MVC ile temel düzeyden ileri seviyeye web programlama eğitim programı...", Content= "Visual Studio .Net ortamında Asp.Net Core MVC ile temel düzeyden ileri seviyeye web programlama eğitim programı.", CategoryId = 3, UserId = 1, PictureUrl = "MVC01.jpg" }
                );




            base.OnModelCreating(builder);
        }
    }
}
