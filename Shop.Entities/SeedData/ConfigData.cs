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
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    ID = 1,
                    Username = "admin",
                    Password = "1",
                    Name = "Dương",
                    Address = "a",
                    Phone = "123",
                    Email = "abc",
                    CreatedDate = DateTime.Now,
                    BirthDay = DateTime.Now,
                    AccountType = 1,
                    Sex = 1,
                    Status = true,
                    IsActive = true,
                    IsDelete = false,
                }, new Account
                {
                    ID = 2,
                    Username = "user",
                    Password = "1",
                    Name = "Dương",
                    Address = "a",
                    Phone = "123",
                    Email = "zxxz",
                    CreatedDate = DateTime.Now,
                    BirthDay = DateTime.Now,
                    AccountType = 2,
                    Sex = 2,
                    Status = true,
                    IsActive = true,
                    IsDelete = false,
                });
        }
    }
}
