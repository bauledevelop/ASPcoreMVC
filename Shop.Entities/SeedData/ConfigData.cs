using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.SeedData
{
    public class ConfigData
    {
        public void ConfigDataAccount(ModelBuilder modelBuilder)
        {
            var passwordHash = new PasswordHasher<AppUser>();
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    ID = 1,
                    Username = "admin",
                    Password = "Admin@123",
                    Name = "Dương",
                    Address = "123 Phạm Văn Đồng",
                    Phone = "0862883237",
                    Email = "minhduong180702@gmail.com",
                    CreatedDate = DateTime.Now,
                    BirthDay = DateTime.Now,
                    AccountType = 1,
                    Sex = 1,
                    Status = true,
                    IsActive = true,
                    IsDelete = false,
                });
            modelBuilder.Entity<AppUser>().HasData(
               new AppUser
               {
                   Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                   UserName = "admin",
                   Address = "123 Phạm Văn Đồng",
                   PasswordHash = passwordHash.HashPassword(null, "Admin@123"),
                   Name = "Dương",
                   NormalizedUserName = "ADMIN",
                   PhoneNumber = "0862883237",
                   AccountType = 1,
                   Sex = 1,
                   BirthDay = DateTime.Now,
                   Email = "minhduong180702@gmail.com",
                   NormalizedEmail = "MINHDUONG180702@GMAIL.COM",
                   EmailConfirmed = true,
               }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb3",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb8",
                    Name = "User",
                    NormalizedName = "USER"
                }
                );
        }
    }
}
