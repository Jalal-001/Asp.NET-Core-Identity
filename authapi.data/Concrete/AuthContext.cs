using System;
using Microsoft.EntityFrameworkCore;
using authapi.entity;

namespace authapi.data.Concrete
{
    public class AuthContext:DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options):base(options)
        {
            
        }
        public DbSet<AppUser>Users{get;set;}
    }
}
