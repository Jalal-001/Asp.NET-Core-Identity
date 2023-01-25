using System;
using Microsoft.EntityFrameworkCore;
using authapi.entity;

namespace authapi.data.Concrete
{
    public class AuthContext:DbContext
    {
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Server=AuthDb");
        }
        public DbSet<AppUser>Users{get;set;}
    }
}
