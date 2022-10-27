using DemoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedCategory(builder);
            SeedBook(builder);

            SeedUser(builder);
            SeedRole(builder);
            SeedUserRole(builder);

        }
        private void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "History" },
                new Category { Id = 2, Name = "Entertainment" },
                new Category { Id = 3, Name = "Comics" }
                );
        }
        private void SeedBook(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Name = "The War of Jenkins' Ear",
                    Price = 42.59,
                    Quantity = 50,
                    CategoryId = 1,
                    Author = "Robert Gaudi",
                    Image = "https://img.thriftbooks.com/api/images/i/l/A8355E53B30072A52D98408A600E40C47BF77518.jpg"
                },
                new Book
                {
                    Id = 2,
                    Name = "African Kaiser: General Paul Von Lettow-Vorbeck and the Great War in Africa, 1914-1918",
                    Price = 28.69,
                    Quantity = 30,
                    CategoryId = 1,
                    Author = "Robert Gaudi",
                    Image = "https://img.thriftbooks.com/api/images/m/1b3c2d90b93acea06d9807e06ad6ff03a47a1076.jpg"
                },
                new Book
                {
                    Id = 3,
                    Name = "Diary of a Wimpy Kid",
                    Price = 13.99,
                    Quantity = 20,
                    CategoryId = 2,
                    Author = "Jeff Kinney",
                    Image = "https://img.thriftbooks.com/api/images/m/81690cdfa2eaab727c29d81b308efba1dec70a55.jpg"
                },
                new Book
                {
                    Id = 4,
                    Name = "The Last Straw",

                    Price = 22.99,
                    Quantity = 50,
                    CategoryId = 2,
                    Author = "Jeff Kinney",
                    Image = "https://img.thriftbooks.com/api/images/m/a38190325523b980a79a9388e76fd5da6e397f34.jpg"
                },
                new Book
                {
                    Id = 5,
                    Name = "Diary of a wimpy kid. rodrick rules",

                    Price = 13.99,
                    Quantity = 40,
                    CategoryId = 2,
                    Author = "Jeff Kinney",
                    Image = "https://img.thriftbooks.com/api/images/i/m/A41687F6F3D81F875262446FEFE9383C61A2A6C8.jpg"
                },
                new Book
                {
                    Id = 6,
                    Name = "Naruto, Vol. 3",

                    Price = 9.99,
                    Quantity = 20,
                    CategoryId = 3,
                    Author = "Masashi Kishimoto",
                    Image = "https://img.thriftbooks.com/api/images/m/456fbecc05164ed1e327ff5ee9922475d1e38df0.jpg"
                },
                new Book
                {
                    Id = 7,
                    Name = "Naruto, Vol. 2",

                    Price = 9.99,
                    Quantity = 10,
                    CategoryId = 3,
                    Author = "Masashi Kishimoto",
                    Image = "https://img.thriftbooks.com/api/images/m/1414da15882c6e496e490103cbfb8b5948be77f7.jpg"
                },
                new Book
                {
                    Id = 8,
                    Name = "Naruto, Vol. 1",

                    Price = 9.99,
                    Quantity = 30,
                    CategoryId = 3,
                    Author = "Masashi Kishimoto",
                    Image = "https://img.thriftbooks.com/api/images/m/5733979f44f82d7347b8d7718f996747462fe029.jpg"
                }
                );
            //throw new NotImplementedException();
        }

        private void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "3",
                    RoleId = "3"
                }
            );
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Customer",
                    NormalizedName = "Customer"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Owner",
                    NormalizedName = "Owner"
                }

            );
        }

        private void SeedUser(ModelBuilder builder)
        {
            //tạo tài khoản test cho admin & customer
            var admin = new IdentityUser
            {
                Id = "1",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com"
            };
            var customer = new IdentityUser
            {
                Id = "2",
                Email = "customer@gmail.com",
                UserName = "customer@gmail.com",
                NormalizedUserName = "customer@gmail.com"
            };
            var owner = new IdentityUser
            {
                Id = "3",
                Email = "owner@gmail.com",
                UserName = "owner@gmail.com",
                NormalizedUserName = "owner@gmail.com"
            };

            //khai báo thư viện để mã hóa mật khẩu cho user
            var hasher = new PasswordHasher<IdentityUser>();

            //set mật khẩu đã mã hóa cho từng user
            admin.PasswordHash = hasher.HashPassword(admin, "123456");
            customer.PasswordHash = hasher.HashPassword(customer, "123456");
            owner.PasswordHash = hasher.HashPassword(owner, "123456");

            //add 2 tài khoản test vào bảng User
            builder.Entity<IdentityUser>().HasData(admin, customer, owner);
        }


    }
}
