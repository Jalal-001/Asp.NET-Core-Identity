using System;
using Microsoft.AspNetCore.Identity;
namespace authapi.entity
{
    public class AppUser:IdentityUser
    {
         public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
