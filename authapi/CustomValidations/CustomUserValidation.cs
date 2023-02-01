using System;
using authapi.entity;
using Microsoft.AspNetCore.Identity;

namespace authapi.CustomValidations
{
    public class CustomUserValidation : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError>errors=new List<IdentityError>();

            if(int.TryParse(user.UserName[0].ToString(),out int _))
                errors.Add(new IdentityError{Code="Exception",Description="Ad rəqəmlə başlaya bilməz!"});
            if(user.UserName.Length<3 && user.UserName.Length>25)
                errors.Add(new IdentityError{Code="Exception",Description="Ad 3 ilə 25 simvoldan ibarət olmalıdır!"});
            if(user.Email.Length>70)
                errors.Add(new IdentityError{Code="Exception",Description="Email 70 simvoldan artəq ola bilməz!"});

            if(!errors.Any())
               return Task.FromResult(IdentityResult.Success);
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
