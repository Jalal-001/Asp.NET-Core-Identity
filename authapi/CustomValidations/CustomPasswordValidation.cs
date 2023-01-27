using System;
using authapi.entity;
using Microsoft.AspNetCore.Identity;

namespace authapi.CustomValidations
{
    public class CustomPasswordValidation : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            List<IdentityError>errors=new List<IdentityError>();
            if(password.Length<3)
                errors.Add(new IdentityError{Code="PasswordLength",Description="İstifadəçi adı 3 simvoldan az ola bilməz!"});
            if(password.ToLower().Contains(user.UserName.ToLower()))
                errors.Add(new IdentityError{Code="PasswordContainError",Description="Şifrə daxilində istifadəçi adı ola bilməz!"});
            
            if(!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
