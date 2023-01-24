using System;
using Microsoft.EntityFrameworkCore;

namespace authapi.data.Concrete
{
    public class AuthContext:DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options):base(options)
        {
            
        }
    }
}
