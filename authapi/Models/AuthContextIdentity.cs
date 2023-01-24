using System;
using authapi.data.Concrete;
using authapi.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authapi.Models
{
    public class AuthContextIdentity:IdentityDbContext<AppUser>
    {
        public AuthContextIdentity(DbContextOptions<AuthContext> dbContext):base(dbContext)
        {
            
        }
    }
}
