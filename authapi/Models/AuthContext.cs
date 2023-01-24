using System;
using authapi.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authapi.Models
{
    public class AuthContext:IdentityDbContext<AppUser>
    {
        public AuthContext(DbContextOptions<AuthContext> context):base(context)
        {
            
        }
    }
}
