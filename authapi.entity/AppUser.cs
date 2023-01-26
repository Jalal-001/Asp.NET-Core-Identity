using System;
using Microsoft.AspNetCore.Identity;
namespace authapi.entity
{
    public class AppUser:IdentityUser
    {
        public string? Country { get; set; }
    }
}
