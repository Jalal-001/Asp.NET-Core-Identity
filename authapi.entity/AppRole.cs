using System;
using Microsoft.AspNetCore.Identity;

namespace authapi.entity
{
    public class AppRole:IdentityRole
    {
        public DateTime CreateDate { get; set; }
    }
}
