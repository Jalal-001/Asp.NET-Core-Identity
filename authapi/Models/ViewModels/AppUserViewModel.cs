using System;
using System.ComponentModel.DataAnnotations;

namespace authapi.Models.ViewModels
{
    public class AppUserViewModel
    {
        [Required(ErrorMessage ="İstifadəçi adı boş ola bilməz!")]
        [StringLength(15,MinimumLength =3,ErrorMessage ="İstifadəçi adı 3 ilə 15 simvoldan ibarət olmalıdır!")]
        [Display(Name ="İstifadəçi adı")]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Email boş ola bilməz!")]
        [EmailAddress(ErrorMessage ="Format düzgün deyil!")]
        [Display(Name ="Email")]

        public string Email { get; set; }
        [Required(ErrorMessage ="Şifrə boş ola bilməz!")]
        [DataType(dataType:DataType.Password,ErrorMessage ="Xəta baş verdi!")]
        [Display(Name ="Şifrə")]
        public string Password { get; set; }
    }
}
