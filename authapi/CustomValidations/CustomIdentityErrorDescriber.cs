using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace authapi.CustomValidations
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "Təkrar ad", Description = "Ad artıq mövcuddur!" };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "Təkrar email", Description = "Email artıq mövcuddur!" };
        }

        public override IdentityError InvalidUserName(string? userName)
        {
            return new IdentityError { Code = "Keçərsiz ad", Description = "Yanlış ad!" };
        }

        public override IdentityError InvalidEmail(string? email)
        {
            return new IdentityError { Code = "Keçərsiz email", Description = "Email keçərsizdir!" };
        }
    }
}