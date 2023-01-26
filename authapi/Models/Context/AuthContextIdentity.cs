using System;
using authapi.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authapi.Models.Context
{
    public class AuthContextIdentity:IdentityDbContext<AppUser,AppRole,string>
    {
        public AuthContextIdentity(DbContextOptions<AuthContextIdentity>options):base(options)
        {
            
        }
    }
}
